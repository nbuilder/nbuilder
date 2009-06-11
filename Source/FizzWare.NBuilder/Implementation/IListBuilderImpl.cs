using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Implementation
{
    public interface IListBuilderImpl<T> : IListBuilder<T>
    {
        IObjectBuilder<T> CreateObjectBuilder();
        int Capacity { get; }
        IDeclarationQueue<T> Declarations { get; }
        IDeclaration<T> AddDeclaration(IDeclaration<T> declaration);
        IUniqueRandomGenerator ScopeUniqueRandomGenerator { get; }
    }
}