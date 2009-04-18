using System;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    public static class CustomOperableExtensions
    {
        public static IOperable<Product> HaveWarehouseLocations(this IOperable<Product> operable)
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

        public static IOperable<Product> HaveLongTitles(this IOperable<Product> operable)
        {
            ((IDeclaration<Product>) operable).ObjectBuilder.With(x => x.Title = "");
            return operable;
        }

        public static IListBuilder<Product> WhereAllHaveLongTitles(this IListBuilder<Product> listBuilder)
        {
            var listBuilderImpl = (IListBuilderImpl<Product>) listBuilder;
            var declaration = new GlobalDeclaration<Product>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder());
            declaration.Have(x => x.Title = "");

            return declaration;
        }
    }

    public class Testit
    {
        public void Do()
        {
            Builder<Product>
                .CreateListOfSize(10)
                .WhereAll()
                    .HaveLongTitles()
                .Build();
        }
    }
}