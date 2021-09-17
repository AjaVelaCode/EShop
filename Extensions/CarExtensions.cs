using EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Extensions
{
    public static class CarExtensions
    { 
        public static IQueryable<Car> QueryByName(this IQueryable<Car> query, string name) 
            => query.Where(car => car.Name == name);
    }
}
