using ECommerce.Shared;
using ECommerceDomain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Specification
{
    public class ProductSpesificationHelper 
    {
        public static Expression<Func<Product,bool>> GetProductCriteria(ProductQueryPram productPram)
        {
            return
         P =>
         (!productPram.BrandId.HasValue || P.BrandId == productPram.BrandId.Value)
         && (!productPram.TypeId.HasValue || P.TypeId == productPram.TypeId.Value)
         && (string.IsNullOrEmpty(productPram.Serach) || P.Name.ToLower().Contains(productPram.Serach.ToLower()));
 
        }
    }
}
