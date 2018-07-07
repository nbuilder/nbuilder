namespace FizzWare.NBuilder.Implementation
{
    public interface IDeclarationQueue<T> : IQueue<IDeclaration<T>>
    {
        int ListCapacity { get;  }
        void Construct();
        void Prioritise();
        bool ContainsGlobalDeclaration();
    }
}