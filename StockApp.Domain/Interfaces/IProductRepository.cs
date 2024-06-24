using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetById(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice);


        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
        Task BulkUpdateAsync(List<Product> products);
        Task<IEnumerable<Product>> GetAll(int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice);



    }
}
