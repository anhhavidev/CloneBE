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
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ProductResponse> CreateProduct(ProductRequest productRequest)
        {
            var product = mapper.Map<Product>(productRequest); // map từ ProductRequest sang Product

            // Xử lý ảnh
            if (productRequest.LinkImagesPath != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productRequest.LinkImagesPath.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productRequest.LinkImagesPath.CopyToAsync(stream);
                }

                product.LinkImagesPath = "/images/" + fileName;
            }

            unitOfWork1.ProductRepo.Add(product);
            await unitOfWork1.SaveChangesAsync();

            // map sang ProductResponse để trả về cho client
            return mapper.Map<ProductResponse>(product);
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

        public async Task<ProductResponse> GetProductByID(int id)
        {
             var tmp = await unitOfWork1.ProductRepo.GetById(id);
            if (tmp == null)
                throw new KeyNotFoundException("Sản phẩm không tồn tại."); // ❌ Lỗi được ném ra ở Service
            return mapper.Map<ProductResponse>(tmp);
        }


        public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest productDetail)
        {
            var product = await unitOfWork1.ProductRepo.GetById(productDetail.ProductId);
            if (product == null)
                return null;

            // Nếu có ảnh mới thì xử lý lưu ảnh
            if (productDetail.LinkImages != null && productDetail.LinkImages.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(productDetail.LinkImages.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadPath); // Đảm bảo thư mục tồn tại
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDetail.LinkImages.CopyToAsync(stream);
                }

                product.LinkImagesPath = "/images/" + uniqueFileName;
            }

            // Cập nhật các trường khác
            mapper.Map(productDetail, product);
            unitOfWork1.ProductRepo.Update(product);
            await unitOfWork1.SaveChangesAsync();

            return mapper.Map<ProductResponse>(product);
        }



    }
}
