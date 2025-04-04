namespace Management.Game.StateMachine
{
    public interface IStack<T>
    {
        int Count { get; }
        void Clear();
        bool Contains(T item);
        T Peek();
        T Pop();
        void Push(T item);
        T[] ToArray();
    }
}