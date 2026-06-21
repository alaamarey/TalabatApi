using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {

        // 1. Where Spec
        Expression<Func<T, bool>> Criteria { get; set; }


        // 2. Includes
        List<Expression<Func<T, object>>> Includes { get; set; }


        // 3. SortAsc
        Expression<Func<T, object>> OrderBy { get; set; }

        // 4. SortDesc
        Expression<Func<T, object>> OrderByDesc { get; set; }

        // 5. Pagination 
        int Skip { get; set; }

        int Take { get; set; }



    }
}
