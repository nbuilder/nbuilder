namespace NBuilderCore.Implementation
{
    public interface IDeclarationQueue<T> : IQueue<IDeclaration<T>>
    {
        void Construct();
        void Prioritise();
        bool ContainsGlobalDeclaration();
    }
}