using Microsoft.AspNetCore.Mvc;
using BAL.Interfaces;
using DAL.Models;
using Common.Responses;
using System.Net;

namespace ProductAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _objProductService;
        public ProductController(IProductService objProductService)
        {
            _objProductService = objProductService;
        }

        /// <summary>
        /// This api will add product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("products")]
        public ActionResult<string> AddProduct(Product product)
        {
            ApiResponse<object> response = _objProductService.AddProduct(product);
            if (response.IsSuccess)
                return Ok(new { message = response.Message });
            return StatusCode((int)HttpStatusCode.BadRequest, new { message = response.Message });
        }

        /// <summary>
        /// This api will return all the prducts available in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        public ActionResult<List<Product>> GetProducts()
        {
            ApiResponse<List<Product>> response = _objProductService.GetProducts();
            if (response.Data.Count != 0)
                return Ok(response.Data);
            return Ok(new { message = response.Message });
        }

        /// <summary>
        /// This api will fetch and retrun the product with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("products/{id}")]
        public ActionResult<Product> GetPoduct(int id)
        {
            ApiResponse<Product> response = _objProductService.GetProduct(id);
            if(response.Data != null)
                return Ok(response.Data);
            return NotFound(new { message = response.Message });
        }

        /// <summary>
        /// This api will fetch and delete the product with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("products/{id}")]
        public ActionResult<string> DeletePoduct(int id)
        {
            ApiResponse<Product> productResponse = _objProductService.GetProduct(id);
            if (productResponse.Data != null)
            {
                ApiResponse<object> deleteResponse = _objProductService.DeleteProduct(productResponse.Data);
                if(deleteResponse.IsSuccess)
                    return Ok(new {message = deleteResponse.Message});
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = deleteResponse.Message });
            }
            return NotFound(new { message = productResponse.Message });
        }

        /// <summary>
        /// This api will update the existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("products/{id}")]
        public ActionResult<string> UpdateProduct(int id, Product product)
        {
            ApiResponse<Product> productResponse = _objProductService.GetProduct(id);
            if (productResponse.Data != null)
            {
                ApiResponse<object> updatedResponse = _objProductService.UpdateProduct(productResponse.Data, product);
                if (updatedResponse.IsSuccess)
                    return Ok(new { message = updatedResponse.Message });
                return StatusCode((int)HttpStatusCode.BadRequest, new { message = updatedResponse.Message });
            }
            return NotFound(new { message = productResponse.Message });
        }

        /// <summary>
        /// This api will decrease the stock value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("products/decrement-stock/{id}/{quantity}")]
        public ActionResult<string> DecrementStock(int id, int quantity)
        {
            ApiResponse<Product> productResponse = _objProductService.GetProduct(id);
            if (productResponse.Data != null)
            {
                ApiResponse<object> decrementResponse = _objProductService.DecrementStock(productResponse.Data, quantity);
                if (decrementResponse.IsSuccess)
                    return Ok(new { message = decrementResponse.Message });
                return StatusCode((int)HttpStatusCode.BadRequest, new { message = decrementResponse.Message });
            }
            return NotFound(new { message = productResponse.Message });
        }

        /// <summary>
        /// This api will increase the stock value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("products/add-to-stock/{id}/{quantity}")]
        public ActionResult<string> incrementStock(int id, int quantity)
        {
            ApiResponse<Product> productResponse = _objProductService.GetProduct(id);
            if (productResponse.Data != null)
            {
                ApiResponse<object> incrementResponse = _objProductService.IncrementStock(productResponse.Data, quantity);
                if (incrementResponse.IsSuccess)
                    return Ok(new { message = incrementResponse.Message });
                return StatusCode((int)HttpStatusCode.BadRequest, new { message = incrementResponse.Message });
            }
            return NotFound(new { message = productResponse.Message });
        }
    }
}
