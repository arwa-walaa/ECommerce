 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
       
        public string Name { get; set; } =null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        //FK => Fluent API
        public int BrandId { get; set; }

        public ProductBrand? ProductBrands { get; set; }

        public int TypeId { get; set; }

        public ProductType? ProductTypes{ get; set; }

    }
}
