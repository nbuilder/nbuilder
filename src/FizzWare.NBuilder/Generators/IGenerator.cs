namespace FizzWare.NBuilder
{
    public interface IGenerator<out T>
    {
        T Generate();
    }
}