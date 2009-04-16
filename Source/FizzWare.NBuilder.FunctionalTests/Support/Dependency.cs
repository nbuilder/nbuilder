using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;

namespace FizzWare.NBuilder.FunctionalTests.Support
{
    /// <summary>
    /// Very simple IoC
    /// </summary>
    public static class Dependency
    {
        public static T Resolve<T>() where T : class
        {
            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository() as T;
            }

            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository() as T;
            }

            if (typeof(T) == typeof(IBasketRepository))
            {
                return new BasketRepository() as T;
            }

            if (typeof(T) == typeof(ITaxTypeRepository))
            {
                return new TaxTypeRepository() as T;
            }

            throw new ArgumentException("Type not found");
        }
    }
}