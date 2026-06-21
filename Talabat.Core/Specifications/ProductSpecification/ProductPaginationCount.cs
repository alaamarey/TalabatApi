using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductPaginationCount : BaseSpecifications<Product>
    {
        public ProductPaginationCount(ProductParamsSpec productParams) : base(
            P =>
                  (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId) &&
                  (!productParams.CategoryId.HasValue || P.CategoryId == productParams.CategoryId) &&
                  (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search.ToLower()))
            )
        { }

    }
}
