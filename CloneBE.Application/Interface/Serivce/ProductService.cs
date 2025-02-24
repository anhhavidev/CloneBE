using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;

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
        public async  Task<ProductDetail> CreateProduct(ProductDetail productdetail)
        {
            var product = mapper.Map<Product>(productdetail); // Map từ ProductDetail sang Product
             unitOfWork1.ProductRepo.Add(product); // Thêm vào database
            await unitOfWork1.SaveChangesAsync(); // Lưu thay đổi

            return mapper.Map<ProductDetail>(product); // Trả về ProductDetail sau khi thêm
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await unitOfWork1.ProductRepo.GetProductByID(id);
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

        public async Task<ProductDetail> GetProductByID(int id)
        {
             var tmp = await unitOfWork1.ProductRepo.GetProductByID(id);
            return mapper.Map<ProductDetail>(tmp);
        }

        public async Task<ProductDetail> UpdateProduct(ProductDetail productdetail)
        {
            var products = await unitOfWork1.ProductRepo.GetProductByID(productdetail.ProductId);
            if (products == null) return null;
            mapper.Map(productdetail, products); // productdetail sang products
            unitOfWork1.ProductRepo.Update(products);
            await unitOfWork1.SaveChangesAsync();
            return productdetail;
        }
    }
}
