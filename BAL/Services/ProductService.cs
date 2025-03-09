using BAL.Interfaces;
using DAL.Models;
using DAL.Interfaces;
using BAL.Constants;
using Common.Responses;

namespace BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _objProductRepository;
        private readonly IUniqueIdGenerator _idGenerator;
        private readonly IValidationUtility _validationUtility;
        public ProductService(IProductRepository objProductRepository, IUniqueIdGenerator idGenerator, IValidationUtility validationUtility)
        {
            _objProductRepository = objProductRepository;
            _idGenerator = idGenerator;
            _validationUtility = validationUtility;
        }
        public int GenerateUniqueId()
        {
            int uniqueId = 0;
            int attempts = 1000;
            bool uniqueIdGenerated = false;
            while (!uniqueIdGenerated && attempts < ApiConstant.IdGenerationMaxAttempts)
            {
                uniqueId = _idGenerator.GenerateUniqueId();
                Product product = _objProductRepository.GetProduct(uniqueId);
                if (product == null)
                    uniqueIdGenerated = true;
                attempts++;
            }
            if (attempts >= ApiConstant.IdGenerationMaxAttempts)
            {
                throw new InvalidOperationException(ApiMessages.API001);
            }
            return uniqueId;
        }
        public string ValidateProductDetails(Product product)
        {
            List<string> errors = new List<string>();
            if (product == null)
                errors.Add(ApiMessages.API009);
            else
            {
                errors.AddRange(_validationUtility.ValidProductName(product.ProductName));
                if (product.StockValue == null)
                    errors.Add(ApiMessages.API015);
                else
                    errors.AddRange(_validationUtility.ValidProductStock(product.StockValue));
            }   
            return errors.Any() ? string.Join(" ", errors) : string.Empty;
        }
        public ApiResponse<object> AddProduct(Product product)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            string errorMsg = ValidateProductDetails(product);
            if (!string.IsNullOrEmpty(errorMsg) && !string.IsNullOrWhiteSpace(errorMsg))
            {
                response.Message = errorMsg.Trim();
                return response;
            }   
            int generatedProductId = GenerateUniqueId();
            product.ProductId = generatedProductId;
            product.ProductName = product.ProductName.Trim();
            product.Description = product.Description != null ? product.Description.Trim() : null;
            bool productAdded = _objProductRepository.AddProduct(product);
            if (productAdded)
            {
                response.Message = ApiMessages.API004;
                response.IsSuccess = true;
            }
            else
                response.Message = ApiMessages.API005;
            return response;
        }
        public ApiResponse<List<Product>> GetProducts()
        {
            ApiResponse<List<Product>> response = new ApiResponse<List<Product>>();
            response.Data =  _objProductRepository.GetProducts();
            if (response.Data.Count == 0)
                response.Message = ApiMessages.API003;
            return response;
        }
        public ApiResponse<Product> GetProduct(int productId)
        {
            ApiResponse<Product> response = new ApiResponse<Product>();
            response.Data = _objProductRepository.GetProduct(productId);
            if (response.Data == null)
                response.Message = string.Format(ApiMessages.API006, productId);
            return response;
        }
        public ApiResponse<object> DeleteProduct(Product product)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = _objProductRepository.DeleteProduct(product);
            if (response.IsSuccess)
                response.Message = string.Format(ApiMessages.API007, product.ProductId);
            else
                response.Message = string.Format(ApiMessages.API008, product.ProductId);
            return response;
        }
        public string ValidateProductFieldsForUpdate(Product product)
        {
            List<string> errors = new List<string>();
            if (product == null)
                errors.Add(ApiMessages.API009);
            else
            {
                if (product.ProductName != null)
                    errors.AddRange(_validationUtility.ValidProductName(product.ProductName));
                if (product.StockValue != null)
                    errors.AddRange(_validationUtility.ValidProductStock(product.StockValue));
            }
            return errors.Any() ? string.Join(" ", errors) : string.Empty;
        }
        public ApiResponse<object> UpdateProduct(Product dbProduct, Product updatedProduct)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            if (updatedProduct == null)
            {
                response.Message = ApiMessages.API009;
                return response;
            }
            string errorMsg = ValidateProductFieldsForUpdate(updatedProduct);
            if(string.IsNullOrEmpty(errorMsg))
            {
                if (!string.IsNullOrEmpty(updatedProduct.ProductName) && !string.IsNullOrWhiteSpace(updatedProduct.ProductName))
                    dbProduct.ProductName = updatedProduct.ProductName;
                if (updatedProduct.StockValue != 0 && updatedProduct.StockValue > 0)
                    dbProduct.StockValue = updatedProduct.StockValue;
                if (!string.IsNullOrEmpty(updatedProduct.Description) && !string.IsNullOrWhiteSpace(updatedProduct.Description))
                    dbProduct.Description = updatedProduct.Description;
                bool productUpdated = _objProductRepository.UpdateProduct(dbProduct);
                if (productUpdated)
                {
                    response.IsSuccess = true;
                    response.Message = string.Format(ApiMessages.API013, dbProduct.ProductId);
                }
                else
                    response.Message = string.Format(ApiMessages.API014, dbProduct.ProductId);
            }
            else
            {
                response.Message = errorMsg;
            }
            return response;
        }
        public ApiResponse<object> DecrementStock(Product product, int decrementStockValue)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            if (decrementStockValue == 0 || decrementStockValue < 0)
                response.Message = ApiMessages.API012;
            else if (decrementStockValue > product.StockValue)
                response.Message = string.Format(ApiMessages.API016, product.ProductId);
            else
            {
                product.StockValue -= decrementStockValue;
                bool stockDecremented = _objProductRepository.UpdateProduct(product);
                if(stockDecremented)
                {
                    response.IsSuccess = true;
                    response.Message = string.Format(ApiMessages.API017, product.ProductId, decrementStockValue);
                }
                else
                    response.Message = string.Format(ApiMessages.API018, product.ProductId);
            }
            return response;
        }
        public ApiResponse<object> IncrementStock(Product product, int incrementStockValue)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            if (incrementStockValue == 0 || incrementStockValue < 0)
                response.Message = ApiMessages.API012;
            else
            {
                product.StockValue += incrementStockValue;
                bool stockIncremented = _objProductRepository.UpdateProduct(product);
                if (stockIncremented)
                {
                    response.IsSuccess = true;
                    response.Message = string.Format(ApiMessages.API019, product.ProductId, incrementStockValue);
                }
                else
                    response.Message = string.Format(ApiMessages.API020, product.ProductId);
            }
            return response;
        }
    }
}
