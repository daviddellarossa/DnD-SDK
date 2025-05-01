using System;
using MessageBroker;

namespace DeeDeeR.MessageBroker
{
    public class ExceptionMessageBrokerEventArgs : MessageBrokerEventArgs, IResettable
    {
        public Exception Exception { get; set; }
        public Exception InnerException { get; set; }

        // Reset custom state
        public void ResetState()
        {
            Exception = null;
            InnerException = null;
        }
    }
}