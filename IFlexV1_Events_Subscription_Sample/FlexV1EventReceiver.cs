using CookComputing.XmlRpc;
using System;
using System.Net;
using System.Threading;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public interface IFlexV1OnEvent
    {
        [XmlRpcMethod("FlexV1.OnEvent")]
        void OnEvent(DNAFusionEvent fusionEvent);
    }

    public interface IFlexV1OnAlarm
    {
        [XmlRpcMethod("FlexV1.OnAlarm")]
        void OnAlarm(DNAFusionAlarm alarm);
    }

    public delegate void DNAFusionEventHandler(DNAFusionEvent @event);
    public delegate void DNAFusionAlarmHandler(DNAFusionAlarm alarm);
    public delegate void EventReceiverException(object sender, Exception ex);

    public class EventReceiverService : XmlRpcListenerService, IFlexV1OnEvent, IFlexV1OnAlarm
    {
        #region ctor
        public EventReceiverService(DNAFusionEventHandler eventHandler, DNAFusionAlarmHandler alarmHandler)
        {
            this.eventHandler = eventHandler;
            this.alarmHandler = alarmHandler;
        }
        #endregion

        #region IFlexV1OnEvent Implementation
        private readonly DNAFusionEventHandler eventHandler;

        public void OnEvent(DNAFusionEvent @event)
        {
            if (eventHandler != null)
                eventHandler(@event);
        }
        #endregion

        #region IFlexV1OnAlarm Implementation
        private readonly DNAFusionAlarmHandler alarmHandler;

        public void OnAlarm(DNAFusionAlarm alarm)
        {
            if (alarmHandler != null)
                alarmHandler(alarm);
        }
        #endregion
    }

    public class FlexV1EventReceiver : IDisposable
    {
        #region ctor
        public FlexV1EventReceiver()
            : this(8899)
        {
        }

        public FlexV1EventReceiver(int listeningPort)
        {
            ListeningPort = listeningPort;
        }
        #endregion

        #region Events
        public event DNAFusionEventHandler OnEvent;
        public event DNAFusionAlarmHandler OnAlarm;
        public event EventReceiverException OnSubscribeException;
        public event EventReceiverException OnUnsubscribeException;
        #endregion

        #region Properties
        public HttpListener Listener
        {
            get;
            private set;
        }

        public int ListeningPort
        {
            get;
            private set;
        }

        public string ApiKey
        {
            get;
            set;
        }

        public string ServiceUrl
        {
            get;
            set;
        }

        public bool PortForward
        {
            get;
            set;
        }

        public SubscribeTo SubscribeTo
        {
            get;
            set;
        }

        private bool active;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (active && !value)
                    DoClose();
                else
                    if (!active && value)
                        DoOpen();

                active = value;
            }
        }
        #endregion

        private Thread listenerThread;

        private void DoOpen()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add(string.Format("http://*:{0}/", ListeningPort));
            Listener.Start();

            listenerThread = new Thread(Listen);
            listenerThread.Start();
        }

        private void DoClose()
        {
            if (Listener == null)
                return;

            Listener.Stop();

            listenerThread.Abort();
            listenerThread.Join();
            Listener = null;
        }

        private void Listen()
        {
            while ((this.Listener != null) && (this.Listener.IsListening))
            {
                try
                {
                    HttpListenerContext context = this.Listener.GetContext();
                    if (context != null)
                        new Thread(new Worker(new EventReceiverService(OnEvent, OnAlarm), context).ProcessRequest).Start();
                }
                catch (System.Net.HttpListenerException ex)
                {
#if DEBUG
                    Console.WriteLine(ex);
#endif
                }
            }
        }

        public void Subscribe()
        {
            var target = string.Format(@"http://{0}:{1}/", System.Environment.MachineName, ListeningPort);
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginSubscribe(ApiKey, target, SubscribeTo,
                lAsyncResult =>
                {
                    try
                    {
                        service.EndSubscribe(lAsyncResult);
                    }
                    catch (XmlRpcFaultException ex)
                    {
                        if (OnSubscribeException != null)
                            OnSubscribeException(this, ex);
                    }
                }, null);
        }

        public void Unsubscribe()
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginUnsubscribe(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        service.EndUnsubscribe(lAsyncResult);
                    }
                    catch (XmlRpcFaultException ex)
                    {
                        if (OnUnsubscribeException != null)
                            OnUnsubscribeException(this, ex);
                    }
                }, null);
        }

        #region Optional Open/Close replacements
        public void StartListening()
        {
            Active = true;
        }

        public void StopListening()
        {
            Active = false;
        }

        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public void Open()
        {
            Active = true;
        }

        public void Close()
        {
            Active = false;
        }

        #endregion

        #region Worker Class
        private class Worker
        {
            private readonly HttpListenerContext context;
            private readonly XmlRpcListenerService listenerService;

            public Worker(XmlRpcListenerService listenerService, HttpListenerContext context)
            {
                this.context = context;
                this.listenerService = listenerService;
            }

            public void ProcessRequest()
            {
                listenerService.ProcessRequest(context);
            }
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Active = false;
        }
        #endregion
    }
}
