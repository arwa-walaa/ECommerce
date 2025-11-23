using AutoMapper;
using ECommerce.Shared;
using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.ProductDtos;
using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.ProductModule;
using ECommerceService.Exceptions;
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

        public async Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryPram productPram)
        {
            var Spec= new ProductWithBrandAndTypeSpecification(productPram);

            var Products = await _unitOfWork.GetRepo<Product, int>().GetAllAsync(Spec);
            var DataToReturn= _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfReturnedData = DataToReturn.Count();
            var CountSpec = new ProductWithBrandAndTypeSpecification(productPram);
            var countOfProducts = await _unitOfWork.GetRepo<Product, int>().CountAsync(CountSpec);

            return new PaginatedResult<ProductDTO>
            (
                productPram.PageIndex,
           
                CountOfReturnedData,

               countOfProducts,
                DataToReturn
            );
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepo<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<Result<ProductDTO>> GetProductByIdAsync(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification( id );
            var product = await _unitOfWork.GetRepo<Product,int>().GetByIdAsync(Spec);
            if (product is null)
            {
              
                return Error.NotFound(description: $"Product with id {id} not found."); 
            }
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
