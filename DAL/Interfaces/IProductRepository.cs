using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        int SaveChanges();
        bool AddProduct(Product product);
        List<Product> GetProducts();
        Product GetProduct(int id);
        bool DeleteProduct(Product product);
        bool UpdateProduct(Product product);
    }
}
