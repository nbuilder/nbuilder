namespace FizzWare.NBuilder.Implementation
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T GetLastItem();
        int Count { get; }
    }
}