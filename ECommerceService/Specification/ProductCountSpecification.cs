using ECommerce.Shared;
using ECommerceDomain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Specification
{
    public class ProductCountSpecification : BaseSpecification<Product, int>
    {

        public ProductCountSpecification(ProductQueryPram productPram) : base
            (
                ProductSpesificationHelper.GetProductCriteria(productPram)
            )
        {


        }

    }
}
