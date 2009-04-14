namespace FizzWare.NBuilder
{
    public interface IForwardAndRearChainable<T> : IForwardChainable<T>, IRearChainable<T>
    {
    }
}