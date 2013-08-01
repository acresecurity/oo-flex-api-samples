using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace OpenOptions.dnaFusion.Flex.V1.Cameras
{
    /// <summary>
    /// MJPEGSource - MJPEG stream support
    /// </summary>
    public class MJPEGSourceAsync
    {
        public MJPEGSourceAsync()
        {
            SeparateConnectionGroup = true;
        }

        private int framesReceived;
        private int bytesReceived;

        private const int bufSize = 512 * 1024;	// buffer size
        private const int readSize = 1024;		// portion size to read

        private delegate void TaskWorkerDelegate(AsyncContext context);
        private readonly AsyncContext asyncContext = new AsyncContext();

        // new frame event
        public event Action<MJPEGSourceAsync, Byte[]> NewFrame;

        // SeparateConnectioGroup property
        // indicates to open WebRequest in separate connection group
        public bool SeparateConnectionGroup
        {
            get;
            set;
        }

        public string VideoSource
        {
            get;
            set;
        }

        public int FramesReceived
        {
            get
            {
                var frames = framesReceived;
                framesReceived = 0;
                return frames;
            }
        }

        public int BytesReceived
        {
            get
            {
                var bytes = bytesReceived;
                bytesReceived = 0;
                return bytes;
            }
        }

        public bool Running
        {
            get
            {
                return asyncContext.IsBusy;
            }
        }

        public void Start()
        {
            try
            {
                if (!Running)
                {
                    framesReceived = 0;
                    bytesReceived = 0;

                    var worker = new TaskWorkerDelegate(TaskWorker);
                    worker.BeginInvoke(asyncContext, TaskCompletedCallback, null);
                    asyncContext.Reset();
                    asyncContext.IsBusy = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                throw ex;
            }
        }

        public void SignalToStop()
        {
            asyncContext.Cancel();
        }

        public void Stop()
        {
            try
            {
                if (this.Running)
                    asyncContext.Cancel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                throw ex;
            }
        }

        private void TaskCompletedCallback(IAsyncResult ar)
        {
            try
            {
                var worker = (TaskWorkerDelegate)((AsyncResult)ar).AsyncDelegate;
                worker.EndInvoke(ar);
                asyncContext.Reset();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                throw ex;
            }
        }

        public void TaskWorker(AsyncContext context)
        {
            try
            {
                var buffer = new byte[bufSize];

                while (!context.IsCancelling)
                {
                    HttpWebRequest req = null;
                    WebResponse resp = null;
                    Stream stream = null;
                    byte[] delimiter = null;

                    byte[] boundary = null;
                    int boundaryLen, delimiterLen = 0, delimiter2Len = 0;
                    int read, todo = 0, total = 0, pos = 0, align = 1;
                    int start = 0, stop = 0;

                    // align
                    //  1 = searching for image start
                    //  2 = searching for image end
                    try
                    {
                        // create request
                        req = (HttpWebRequest)WebRequest.Create(VideoSource);
                        req.UserAgent = "OpenOptions.dnaFusion.Flex.V1.Cameras";

                        // set connection group name
                        if (SeparateConnectionGroup)
                            req.ConnectionGroupName = GetHashCode().ToString();

                        // get response
                        resp = req.GetResponse();

                        // check content type
                        var ct = resp.ContentType;
                        if (ct.IndexOf("multipart/x-mixed-replace") == -1)
                        {
                            if (ct.IndexOf("image/png") == -1 || ct.IndexOf("image/jpeg") == -1)
                                return;
                            else
                                throw new ApplicationException("Invalid URL");
                        }

                        // get boundary
                        var encoding = new ASCIIEncoding();
                        boundary = encoding.GetBytes(ct.Substring(ct.IndexOf("boundary=", 0) + 9));
                        boundaryLen = boundary.Length;

                        // get response stream
                        stream = resp.GetResponseStream();

                        // loop
                        while (!context.IsCancelling)
                        {
                            // check total read
                            if (total > bufSize - readSize)
                                total = pos = todo = 0;

                            // read next portion from stream
                            if ((read = stream.Read(buffer, total, readSize)) == 0)
                                throw new ApplicationException();

                            total += read;
                            todo += read;

                            // increment received bytes counter
                            bytesReceived += read;

                            // does we know the delimiter ?
                            if (delimiter == null)
                            {
                                // find boundary
                                pos = ByteArrayUtils.Find(buffer, boundary, pos, todo);

                                if (pos == -1)
                                {
                                    // was not found
                                    todo = boundaryLen - 1;
                                    pos = total - todo;
                                    continue;
                                }

                                todo = total - pos;

                                if (todo < 2)
                                    continue;

                                // check new line delimiter type
                                if (buffer[pos + boundaryLen] == 10)
                                {
                                    delimiterLen = 2;
                                    delimiter = new byte[2] { 10, 10 };
                                    delimiter2Len = 1;
                                    //delimiter2 = new byte[1] { 10 };
                                }
                                else
                                {
                                    delimiterLen = 4;
                                    delimiter = new byte[4] { 13, 10, 13, 10 };
                                    delimiter2Len = 2;
                                    //delimiter2 = new byte[2] { 13, 10 };
                                }

                                pos += boundaryLen + delimiter2Len;
                                todo = total - pos;
                            }

                            // search for image
                            if (align == 1)
                            {
                                start = ByteArrayUtils.Find(buffer, delimiter, pos, todo);
                                if (start != -1)
                                {
                                    // found delimiter
                                    start += delimiterLen;
                                    pos = start;
                                    todo = total - pos;
                                    align = 2;
                                }
                                else
                                {
                                    // delimiter not found
                                    todo = delimiterLen - 1;
                                    pos = total - todo;
                                }
                            }

                            // search for image end
                            while ((align == 2) && (todo >= boundaryLen) && !context.IsCancelling)
                            {
                                stop = ByteArrayUtils.Find(buffer, boundary, pos, todo);
                                if (stop != -1)
                                {
                                    pos = stop;
                                    todo = total - pos;

                                    // increment frames counter
                                    framesReceived++;

                                    // image at stop
                                    if (NewFrame != null)
                                        NewFrame(this, ByteArrayUtils.Copy(buffer, start, stop - start));

                                    // shift array
                                    pos = stop + boundaryLen;
                                    todo = total - pos;
                                    Array.Copy(buffer, pos, buffer, 0, todo);

                                    total = todo;
                                    pos = 0;
                                    align = 1;
                                }
                                else
                                {
                                    // delimiter not found
                                    todo = boundaryLen - 1;
                                    pos = total - todo;
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                        // wait for a while before the next try
                        Thread.Sleep(250);
                    }
                    catch (ApplicationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                        // wait for a while before the next try
                        Thread.Sleep(250);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                    }
                    finally
                    {
                        // abort request
                        if (req != null)
                        {
                            req.Abort();
                            req = null;
                        }
                        // close response stream
                        if (stream != null)
                        {
                            stream.Close();
                            stream = null;
                        }
                        // close response
                        if (resp != null)
                        {
                            resp.Close();
                            resp = null;
                        }
                    }

                    // need to stop ?
                    if (context.IsCancelling)
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                throw ex;
            }
        }
    }
}
