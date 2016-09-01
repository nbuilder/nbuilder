namespace NBuilderCore.Generators
{
    public interface IGenerator<T>
    {
        T Generate();
    }
}