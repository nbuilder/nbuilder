using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class PersistenceExtensions
    {
        private static IDeclaration<T> GetDeclaration<T>(IOperable<T> operable)
        {
            var declaration = operable as IDeclaration<T>;

            if (declaration == null)
                throw new ArgumentException("Must be of type IDeclaration<T>");

            return declaration;
        }

        public static T Persist<T>(this ISingleObjectBuilder<T> singleObjectBuilder)
        {
            var obj = singleObjectBuilder.Build();

            IPersistenceService persistenceService = BuilderSetup.GetPersistenceService();
            persistenceService.Create(obj);

            return obj;
        }

        public static IList<T> Persist<T>(this IOperable<T> operable)
        {
            var declaration = GetDeclaration(operable);

            return Persist(declaration.ListBuilderImpl);
        }

        public static IList<T> Persist<T>(this IListBuilder<T> listBuilder)
        {
            var list = listBuilder.Build();

            var persistenceService = BuilderSetup.GetPersistenceService();
            persistenceService.Create(list);
            return list;
        }
    }
}
