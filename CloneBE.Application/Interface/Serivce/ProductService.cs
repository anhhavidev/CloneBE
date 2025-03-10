using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Domain.Model;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CloneBE.Application.Interface.Serivce
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork1 unitOfWork1;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork1 unitOfWork1,IMapper mapper) {
            this.unitOfWork1 = unitOfWork1;
            this.mapper = mapper;
        }
        public async  Task<ProductRequest> CreateProduct(ProductRequest productdetail)
        {
            var product = mapper.Map<Product>(productdetail); // Map từ ProductDetail sang Product
             unitOfWork1.ProductRepo.Add(product); // Thêm vào database
            await unitOfWork1.SaveChangesAsync(); // Lưu thay đổi

            return mapper.Map<ProductRequest>(product); // Trả về ProductDetail sau khi thêm
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await unitOfWork1.ProductRepo.GetById(id);
            if (product == null) return false; // Trả về false nếu không tìm thấy sản phẩm

            unitOfWork1.ProductRepo.Delete(product); // Đánh dấu xóa
            await unitOfWork1.SaveChangesAsync(); // Lưu thay đổi

            return true; // Trả về true nếu xóa thành công
        }

        public async Task<IEnumerable<ProductDTO>> GellALLProduct()
        {
            var products = await unitOfWork1.ProductRepo.GetAll();
            return  mapper.Map<IEnumerable<ProductDTO>>(products); // map từ tmp sang productdto
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductByCategory(int categoryId)
        {
            var products =  await unitOfWork1.ProductRepo.GetAllProductByCategpry(categoryId);
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductFilterResponse<ProductDTO>> GetFilteredProductsAsync(ProductFilterRequestDTO request)
        {
            var parameters = new ProductFilterParameters
            {
                SearchTerm = request.SearchTerm,
                Category = request.Category,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                SortBy = request.SortBy,
                IsDescending = request.IsDescending,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var (products, totalCount) = await unitOfWork1.ProductRepo.GetFilteredProductsAsync(parameters);

            return new ProductFilterResponse<ProductDTO>
            {
                Data = mapper.Map<List<ProductDTO>>(products),
                TotalCount = totalCount,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber
            };
        }

        public async Task<ProductRequest> GetProductByID(int id)
        {
             var tmp = await unitOfWork1.ProductRepo.GetById(id);
            if (tmp == null)
                throw new KeyNotFoundException("Sản phẩm không tồn tại."); // ❌ Lỗi được ném ra ở Service
            return mapper.Map<ProductRequest>(tmp);
        }

        public async Task<ProductRequest> UpdateProduct(ProductRequest productdetail)
        {
            var products = await unitOfWork1.ProductRepo.GetById(productdetail.ProductId);
            if (products == null) return null;
            mapper.Map(productdetail, products); // productdetail sang products
            unitOfWork1.ProductRepo.Update(products);
            await unitOfWork1.SaveChangesAsync();
            return productdetail;
        }
    }
}
