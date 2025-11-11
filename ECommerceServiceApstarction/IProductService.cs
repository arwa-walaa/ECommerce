using ECommerce.Shared;
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
        Task<IEnumerable<ProductDTO>> GetAllProductAsync(ProductQueryPram productPram);

        Task<ProductDTO> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();





    }
}
