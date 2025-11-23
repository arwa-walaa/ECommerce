using ECommerce.Shared;
using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServiceApstarction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryPram productPram);

        Task<Result<ProductDTO>> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();





    }
}
