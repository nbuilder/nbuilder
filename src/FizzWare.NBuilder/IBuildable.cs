namespace FizzWare.NBuilder
{
    public interface IBuildable<T>
    {
        BuilderSettings BuilderSettings { get; set; }
        T Build();
        /// <summary>
        /// Builds the object and recursively builds all nested complex-type properties.
        /// Unlike <see cref="Build()"/>, this method ensures that all nested properties are also built and populated.
        /// </summary>
        /// <returns>The built object with nested properties populated.</returns>
        T BuildRecursive();

    }
}