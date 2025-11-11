using ECommerce.Shared;
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
        public ProductWithBrandAndTypeSpecification(ProductQueryPram productPram) : base
            (
                P =>
                    (!productPram.BrandId.HasValue || P.BrandId == productPram.BrandId.Value)
                    && (!productPram.TypeId.HasValue || P.TypeId == productPram.TypeId.Value)
                    && (string.IsNullOrEmpty(productPram.Serach) || P.Name.ToLower().Contains(productPram.Serach.ToLower()))
            )
        {
        
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
