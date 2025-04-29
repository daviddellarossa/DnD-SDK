using System;
using MessageBroker;

namespace DeeDeeR.MessageBroker
{
    public partial class Common
    {
        public static ExceptionMessageBrokerEventArgs CreateExceptionEventArgs(object sender, object target, Exception exception = null)
        {
            var errorEventArgs = MessageBrokerEventArgs.Pool<ExceptionMessageBrokerEventArgs>.Rent();
            errorEventArgs.Sender = sender;
            errorEventArgs.Target = target;
            errorEventArgs.Exception = exception;
            return errorEventArgs;
        }
        
        public static ExceptionMessageBrokerEventArgs CreateArgumentNullExceptionEventArgs(object sender, object target, string parameterName, Exception exception = null)
        {
            var errorEventArgs = MessageBrokerEventArgs.Pool<ExceptionMessageBrokerEventArgs>.Rent();
            errorEventArgs.Sender = sender;
            errorEventArgs.Target = target;
            errorEventArgs.Exception = new ArgumentNullException(parameterName, $"Parameter {parameterName} is required.");
            errorEventArgs.InnerException = exception;
            return errorEventArgs;
        }
    }
}