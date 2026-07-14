using FirstWebApp.DTOs;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {

        //ProductService productService = new ProductService(); 
        //apply dependency inversion concept 

        private ProductService productService;
        public ProductController(ProductService _productService)//dependency injection
        {
            productService = _productService;
        }




        [HttpGet("GetAllProducts")]        
        public IActionResult GetAllProducts()
        {
            List<ProductOutputDTO> result = productService.GetAllProducts();

            if (result.Count > 0)
            {
                return Ok(result);
            }

            return NoContent(); //204 no data
        }


        //http://localhost:5153/product/GetProductById?id=3 //from query
        //[HttpGet("GetProductById")]

        //http://localhost:5153/product/GetProductById/3 //from route , required
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            ProductAllOutputDTO product = productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(); // 404 notfound
            }

            return Ok (product);   //200 succeeded
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Product product)
        {
            int productId = productService.Create(product);

            return Ok("product added successfully");  /// 200, product added successfully
            return Ok(new { ProductId = productId }); //200, ProductId=1
            return Created(); // 201  created / added
        }


        [HttpPost("AddDTO")]
        public IActionResult AddDTO([FromBody] ProductInputDTO product)
        {
            int productId = productService.Create(product);

            return Ok("product added successfully");  /// 200, product added successfully

        }

        //http://localhost:5153/product/UpdatePrice/3/iphone17?newPrice=200&&newNAme="iphone17"
        //http://localhost:5153/product/UpdatePrice/3?newPrice=200
        [HttpPut("UpdatePrice/{productId}")]
        public IActionResult UpdatePrice([FromRoute] int productId, [FromQuery] int newPrice)
        {
            bool updated = productService.UpdatePrice(productId, newPrice);

            if (!updated)
                return NotFound();

            return Ok("Updated successfully");
           // return NoContent();
        }

        [HttpDelete("Delete/{productId}")]
        public IActionResult Delete([FromRoute] int productId)
        {
            bool deleted = productService.Delete(productId);

            if (!deleted)
                return NotFound();

            return Ok("deleted successfully");
            //return NoContent();
        }
    }
}
