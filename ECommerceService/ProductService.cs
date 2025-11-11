using AutoMapper;
using ECommerce.Shared.DTOS.ProductDtos;
using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.ProductModule;
using ECommerceService.Specification;
using ECommerceServiceApstarction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepo<ProductBrand,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);   

        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var Spec= new ProductWithBrandAndTypeSpecification();

            var Products = await _unitOfWork.GetRepo<Product, int>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepo<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification( id );
            var product = await _unitOfWork.GetRepo<Product,int>().GetByIdAsync(Spec);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
