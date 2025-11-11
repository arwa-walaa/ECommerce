using ECommerceDomain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Specification
{
    public class ProductWithBrandAndTypeSpecification :BaseSpecification<Product,int>
    {
        public ProductWithBrandAndTypeSpecification():base(null) {
        
            AddInclude(p => p.ProductBrands);
            AddInclude(p => p.ProductTypes);
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id==id)
        {

            AddInclude(p => p.ProductBrands);
            AddInclude(p => p.ProductTypes);
        }
    }
}
