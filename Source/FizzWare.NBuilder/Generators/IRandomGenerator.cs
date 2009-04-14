namespace FizzWare.NBuilder
{
    public interface IRandomGenerator<T>
    {
        T Generate(int lower, int upper);
    }
}