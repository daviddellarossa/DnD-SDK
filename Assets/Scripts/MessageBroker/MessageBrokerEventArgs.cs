using System;
using System.Collections.Concurrent;

namespace MessageBroker
{
    public class MessageBrokerEventArgs : System.EventArgs
    {
        public object Sender { get; set; }
        public object Target { get; set; }

        public MessageBrokerEventArgs()
        {
        }

        public MessageBrokerEventArgs(object sender, object target)
        {
            Sender = sender;
            Target = target;
        }

        /// <summary>
        /// Generic pool implementation for MessageBrokerEventArgs and derived classes.
        /// </summary>
        /// <typeparam name="T">Type of MessageBrokerEventArgs or derived class</typeparam>
        public static class Pool<T> where T : MessageBrokerEventArgs, new()
        {
            // Static pool for reusing instances, one pool per type.
            private static readonly ConcurrentBag<T> PoolBag = new ConcurrentBag<T>();

            /// <summary>
            /// Retrieves an instance from the pool or creates a new one.
            /// </summary>
            public static T Rent(object sender = null, object target = null)
            {
                if (PoolBag.TryTake(out var instance))
                {
                    instance.Sender = sender;
                    instance.Target = target;
                    return instance;
                }

                // Create a new instance if the pool is empty
                return new T { Sender = sender, Target = target };
            }

            /// <summary>
            /// Returns an instance back to the pool for reuse.
            /// </summary>
            public static void Return(T instance)
            {
                // Reset the state of the object before returning it to the pool
                instance.Sender = null;
                instance.Target = null;

                if (instance is IResettable resettable)
                {
                    resettable.ResetState();
                }

                PoolBag.Add(instance);
            }
        }
    }

    /// <summary>
    /// Interface for resetting state of pooled objects.
    /// Derived classes can implement this to clear custom state before being returned to the pool.
    /// </summary>
    public interface IResettable
    {
        void ResetState();
    }
}