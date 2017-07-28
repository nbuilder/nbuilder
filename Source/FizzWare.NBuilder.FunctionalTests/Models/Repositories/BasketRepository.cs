using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public class BasketRepository : BaseRepository<ShoppingBasket>, IBasketRepository
    {
        
    }
}