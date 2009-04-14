namespace FizzWare.NBuilder
{
    public interface IUniqueRandomGenerator<T> : IRandomGenerator<T>
    {
        void Reset();
    }
}