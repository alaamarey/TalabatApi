using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(ProductParamsSpec productParams) : base(
         P =>
                    (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId) &&
                    (!productParams.CategoryId.HasValue || P.CategoryId == productParams.CategoryId) &&
                    (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search.ToLower()))

            // 1 -  true & true  (NoBrandId / NoCategoryId)
            // 2 -  P.BrandId == brandId  && true  (BrandId / NoCategoryId)
            // 3 - true &&  P.CategoryId == categoryId  (NoBrandId / CategoryId)
            // 4 - P.BrandId == brandId  &&  P.CategoryId == categoryId (BrandId /CategoryId )
            )
        {
            AddIncludes();

            switch (productParams.Sort)
            {
                case "priceAsc":
                    OrderBy = P => P.Price;
                    break;
                case "priceDesc":
                    OrderByDesc = P => P.Price;
                    break;
                case "nameDesc":
                    OrderByDesc = P => P.Name;
                    break;
                default:
                    OrderBy = P => P.Name;
                    break;
            }

            //sort switch
            //{
            //    "priceAsc" => OrderBy = P => P.Price,
            //    "priceDesc" => OrderByDesc = P => P.Price,
            //    "nameDesc" => OrderByDesc = P => P.Name,
            //    _ => OrderBy = P => P.Name
            //};

            Skip = (productParams.PageIndex - 1) * productParams.PageSize;
            Take = productParams.PageSize;
        }


        public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);
        }

    }
}
