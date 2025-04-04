namespace GameManagement.StateMachine
{
    /// <summary>
    /// Bridge pattern is applied here to decouple the code from the actual implementation of the Stack class.
    /// This is to improve testability in dependent classes.
    /// This class, on the contrary, is not testable as the Stack class is not mockable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Stack<T> : IStack<T>
    {
        /// <summary>
        /// Instance of the Stack underneath. <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>
        protected System.Collections.Generic.Stack<T> _stack = new System.Collections.Generic.Stack<T>();

        /// <see cref="System.Collections.Generic.Stack{T}.Count"/>
        public int Count => _stack.Count;

        /// <see cref="System.Collections.Generic.Stack{T}.Clear"/>
        public void Clear() => _stack.Clear();

        /// <see cref="System.Collections.Generic.Stack{T}.Contains(T)"/>
        public bool Contains(T item) => _stack.Contains(item);

        /// <see cref="System.Collections.Generic.Stack{T}.Peek"/>
        public T Peek() => _stack.Peek();

        /// <see cref="System.Collections.Generic.Stack{T}.Pop"/>
        public T Pop() => _stack.Pop();

        /// <see cref="System.Collections.Generic.Stack{T}.Push(T)"/>
        public void Push(T item) => _stack.Push(item);

        /// <see cref="System.Collections.Generic.Stack{T}.ToArray"/>
        public T[] ToArray() => _stack.ToArray();
    }
}