namespace FizzWare.NBuilder
{
    public interface IForwardChainableOperable<T> : IOperable<T>, IListBuilder<T>
    {
    }

    public interface IForwardAndRearChainableOperable<T> : IForwardChainableOperable<T>, IRearChainableOperable<T>
    {
    }
}