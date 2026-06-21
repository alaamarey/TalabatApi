using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.repository.Data;

namespace Talabat.repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> entity, ISpecifications<TEntity> specs)
        {

            var query = entity;


            // 1. Criteria 
            if (specs.Criteria is not null)
                query = query.Where(specs.Criteria);
            // talabatDbcontext.Set<Product>().where(P => p.id == id);
       

            // 2. Sort
            if (specs.OrderBy is not null)
                query = query.OrderBy(specs.OrderBy);
            else if (specs.OrderByDesc is not null)
                query = query.OrderByDescending(specs.OrderByDesc);


            // 4. Include
            query = specs.Includes.Aggregate(query, (current, expression) => current.Include(expression));
            //string[] Names = { "alaa", "osama", "marey" };
            //string message = "hello";
            //message = Names.Aggregate( message , (acc , current ) => acc + current);





            // 3. Pagination 
            if (specs.Skip >= 0 && specs.Take > 1)
                query = query.Skip(specs.Skip).Take(specs.Take);




         


            return query;
        }


    }
}
