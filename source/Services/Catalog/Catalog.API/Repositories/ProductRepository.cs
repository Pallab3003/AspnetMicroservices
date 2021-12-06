using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _CatalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _CatalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _CatalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _CatalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatagory(string catagory)
        {
            FilterDefinition<Product> Filter= Builders<Product>.Filter.Eq(p => p.Category, catagory);
            return await _CatalogContext.Products.Find(Filter).ToListAsync();
        }

        public async Task<Product> GetProduductById(string id)
        {
            return await _CatalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateProduct(Product product)
        {
             await _CatalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter =Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _CatalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult=await _CatalogContext.Products.ReplaceOneAsync(filter:p => p.Id==product.Id,replacement:product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
