using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.Model;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;  // Quan trọng: import Moq.EntityFrameworkCore
using Xunit;

namespace InfraStructure.UnitTest
{
    public class ProductRepoTests
    {
        private Mock<Databasese> _mockContext;

        private List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Apple", Price = 10, CategoryId = 1 },
            new Product { ProductId = 2, Name = "Banana", Price = 20, CategoryId = 1 },
            new Product { ProductId = 3, Name = "Carrot", Price = 15, CategoryId = 2 },
            new Product { ProductId = 4, Name = "Date", Price = 25, CategoryId = 2 },
        };

        private void SetupMocks()
        {
            _mockContext = new Mock<Databasese>();

            // Dùng Moq.EntityFrameworkCore để mock DbSet<Product> dễ dàng
            _mockContext.Setup(c => c.Set<Product>()).ReturnsDbSet(_products);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_SearchTerm_FiltersCorrectly()
        {
            SetupMocks();
            var repo = new ProductRepo(_mockContext.Object);

            var filter = new ProductFilterParameters
            {
                SearchTerm = "a", // sẽ tìm "Apple", "Banana", "Carrot", "Date" (tất cả có chữ a)
                PageNumber = 1,
                PageSize = 10
            };

            var (result, totalCount) = await repo.GetFilteredProductsAsync(filter);

            Assert.Equal(4, totalCount);
            Assert.Contains(result, p => p.Name == "Apple");
            Assert.Contains(result, p => p.Name == "Banana");
            Assert.Contains(result, p => p.Name == "Carrot");
            Assert.Contains(result, p => p.Name == "Date");
        }

        [Fact]
        public async Task GetFilteredProductsAsync_MinPrice_FilterWorks()
        {
            SetupMocks();
            var repo = new ProductRepo(_mockContext.Object);

            var filter = new ProductFilterParameters
            {
                MinPrice = 15,
                PageNumber = 1,
                PageSize = 10
            };

            var (result, totalCount) = await repo.GetFilteredProductsAsync(filter);

            Assert.Equal(3, totalCount);
            Assert.DoesNotContain(result, p => p.Price < 15);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_Sorting_ByPrice_Descending()
        {
            SetupMocks();
            var repo = new ProductRepo(_mockContext.Object);

            var filter = new ProductFilterParameters
            {
                SortBy = "price",
                IsDescending = true,
                PageNumber = 1,
                PageSize = 10
            };

            var (result, totalCount) = await repo.GetFilteredProductsAsync(filter);

            Assert.Equal(4, totalCount);
            Assert.Equal(new double?[] { 25, 20, 15, 10 }, result.Select(p => p.Price).ToArray());
        }

        [Fact]
        public async Task GetFilteredProductsAsync_Paging_Works()
        {
            SetupMocks();
            var repo = new ProductRepo(_mockContext.Object);

            var filter = new ProductFilterParameters
            {
                PageNumber = 2,
                PageSize = 2
            };

            var (result, totalCount) = await repo.GetFilteredProductsAsync(filter);

            Assert.Equal(4, totalCount);
            Assert.Equal(2, result.Count);
            Assert.Equal(3, result[0].ProductId); // Sản phẩm thứ 3 trong danh sách
        }
    }
}
