namespace FizzWare.NBuilder
{
    public interface IBuildable<T>
    {
        BuilderSettings BuilderSettings { get; set; }
        T Build();
        T BuildRecursive();

    }
}