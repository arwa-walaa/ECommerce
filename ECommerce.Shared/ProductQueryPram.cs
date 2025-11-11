using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryPram
    {
      
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Serach { get; set; }

        public ProductSortingOptions? sort { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = (value <= 0) ? 1 : value; }
        }
        private const int PageCount = 5;
        private const int MaxPageSize = 10;

        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set {
                if (value > MaxPageSize)
                {
                    _pageSize = MaxPageSize;
                }
                else if(value <=0)
                {
                    _pageSize = PageCount;
                }
                else
                {
                    _pageSize = value;
                }


                }
        }



    }
}
