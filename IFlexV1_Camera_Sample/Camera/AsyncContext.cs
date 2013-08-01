using System;

namespace OpenOptions.dnaFusion.Flex.V1.Cameras
{
    public class AsyncContext
    {
        private readonly object _sync = new object();
        private bool _isCancelling = false;
        private bool _isBusy = false;

        public bool IsCancelling
        {
            get
            {
                lock (_sync)
                    return _isCancelling;
            }
        }

        public void Cancel()
        {
            try
            {
                lock (_sync)
                    _isCancelling = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("=============: " + ex.Message);
                throw ex;
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                    lock (_sync)
                        _isBusy = value;
            }
        }

        public void Reset()
        {
            lock (_sync)
            {
                _isBusy = false;
                _isCancelling = false;
            }
        }
    }
}
