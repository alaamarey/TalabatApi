using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductParamsSpec
    {
        private  int MaxPageSize = 10;
        int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public string? Search { get; set; }




    }
}
