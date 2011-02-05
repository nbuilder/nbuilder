using System;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    public static class CustomOperableExtensions
    {
        public static IOperable<Product> WithWarehouseLocations(this IOperable<Product> operable)
        {
            // First the IOperable object needs to be cast to IDeclaration
            var declaration = operable as IDeclaration<Product>;

            if (declaration == null)
                throw new ArgumentException("Must be of type IDeclaration<Product>");

            // Then you can access the declaration's object builder
            declaration.ObjectBuilder.With(x => x.Location = LocationGenerator.Generate());

            // Return the operable object
            return operable;
        }

        public static IOperable<Product> WithLongTitles(this IOperable<Product> operable)
        {
            ((IDeclaration<Product>)operable).ObjectBuilder.With(x => x.Title = "blahblahblahblahblahblahblahblahblahblahblahblahblahblah");
            return operable;
        }

        public static IListBuilder<Product> AllWithLongTitles(this IListBuilder<Product> listBuilder)
        {
            var listBuilderImpl = (IListBuilderImpl<Product>) listBuilder;
            var declaration = new GlobalDeclaration<Product>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder());
            declaration.With(x => x.Title = "blahblahblahblahblahblahblahblahblahblahblahblahblahblah");

            return declaration;
        }
    }

    public class Testit
    {
        public void Do()
        {
            Builder<Product>
                .CreateListOfSize(10)
                .All()
                    .WithLongTitles()
                .Build();
        }
    }
}