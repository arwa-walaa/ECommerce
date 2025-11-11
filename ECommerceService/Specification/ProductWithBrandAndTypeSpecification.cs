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
            //sort
            switch (productPram.sort)
            { 
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending( p => p.Price);
                    break;

                case ProductSortingOptions.NameAsc:
                        AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;

            }
            //pagination
            ApplyPagenation(productPram.PageSize, productPram.PageIndex);

        }

        public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id==id)
        {

            AddInclude(p => p.ProductBrands);
            AddInclude(p => p.ProductTypes);
        }
    }
}
