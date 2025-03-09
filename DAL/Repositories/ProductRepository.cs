using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ProductContext _productContext;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public int SaveChanges()
        {
            return _productContext.SaveChanges();
        }
        public bool AddProduct(Product product)
        {
            using (var transaction = _productContext.Database.BeginTransaction())
            {
                try
                {
                    _productContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products ON");
                    _productContext.Products.Add(product);
                    var rowsAffected = SaveChanges();
                    _productContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products OFF");
                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public List<Product> GetProducts()
        {
            try
            {
                return _productContext.Products.ToList();
            }
            catch
            {
                return new List<Product>();
            }
            
        }
        public Product GetProduct(int id)
        {
            try
            {
                return _productContext.Products.Find(id);
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteProduct(Product product)
        {
            try
            {
                _productContext.Products.Remove(product);
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateProduct(Product product)
        {
            try
            {
                _productContext.Products.Update(product);
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
