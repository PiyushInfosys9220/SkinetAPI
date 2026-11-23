using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext store) : IProductRepository
    {
        public void AddProduct(Product product)
        {
           store.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            store.Products.Remove(product);
        }
        

        public async Task<Product?> GetProductByIdAsync(int id)
        {
           return await store.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand,string? type,string? sort)
        {
            var query = store.Products.AsQueryable();   
              if(!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(p => p.Brand == brand);
            }
            if(!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(p => p.Type == type);
            }
           
                query = sort switch
                {
                    "priceAsc" => query.OrderBy(p => p.Price),
                    "priceDesc" => query.OrderByDescending(p => p.Price),
                    _ => query.OrderBy(p => p.Name)
                };
            
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetBrandAsync()
        {
            return await store.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await store.Products.Select(p => p.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return store.Products.Any(p => p.Id == id);
        
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await store.SaveChangesAsync() > 0;         
        }

        public void UpdateProduct(Product product)
        {
            store.Entry(product).State = EntityState.Modified;

        }
    }
}
