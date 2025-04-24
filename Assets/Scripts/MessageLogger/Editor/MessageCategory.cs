using System;

namespace MessageLogger.Editor
{
    internal abstract class MessageCategory: IDisposable
    {
        protected MessageLogger Logger;

        public void Initialize(MessageLogger logger)
        {
            this.Logger = logger;
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}