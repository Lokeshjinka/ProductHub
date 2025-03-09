using DAL.Models;
using Common.Responses;

namespace BAL.Interfaces
{
    public interface IProductService
    {
        int GenerateUniqueId();
        ApiResponse<object> AddProduct(Product product);
        ApiResponse<List<Product>> GetProducts();
        ApiResponse<Product> GetProduct(int productId);
        ApiResponse<object> DeleteProduct(Product product);
        ApiResponse<object> UpdateProduct(Product dbProduct, Product updatedProduct);
        ApiResponse<object> DecrementStock(Product product, int stockValue);
        ApiResponse<object> IncrementStock(Product product, int stockValue);
    }
}
