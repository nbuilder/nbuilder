namespace NBuilderCore.Implementation
{
    /// <summary>
    /// This is just a marker interface. Global declarations have special meaning 
    /// and are treated differently within NBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGlobalDeclaration<T> : IDeclaration<T>
    {
        
    }
}