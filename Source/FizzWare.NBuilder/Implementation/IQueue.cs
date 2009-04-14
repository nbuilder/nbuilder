namespace FizzWare.NBuilder.Implementation
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        int Count { get; }
    }
}