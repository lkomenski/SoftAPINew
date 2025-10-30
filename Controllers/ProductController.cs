using Microsoft.AspNetCore.Mvc;
using SoftAPINew.Models;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        new Product
        {
            ProductID = 1,
            CategoryId = 1,
            ProductCode = "GDN-0011",
            ProductName = "Garden Cart",
            Description = "15 gallon capacity rolling garden cart",
            ListPrice = 32.99M,
            DiscountPercent = 0M
        },
        new Product
        {
            ProductID = 2,
            CategoryId = 2,
            ProductCode = "TBX-0022",
            ProductName = "Tool Box",
            Description = "16 gallon capacity tool box with wheels",
            ListPrice = 24.99M,
            DiscountPercent = 0M
        }
    };
    
    // HTTP GET method to retrieve all products
    [HttpGet(Name = "GetProducts")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP GET method to retrieve a product by ID
    [HttpGet("{id}", Name = "GetProductById")]
    public IActionResult GetById(int id)
    {
        try
        {
            var product = products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP POST method to add a new product
    [HttpPost(Name = "CreateProduct")]
    public IActionResult Create([FromBody] Product newProduct)
    {
        try
        {
            if (newProduct == null)
                return BadRequest("Invalid product data.");

            newProduct.ProductID = products.Count + 1;
            products.Add(newProduct);
            return CreatedAtRoute("GetProductById", new { id = newProduct.ProductID }, newProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


    [HttpPut("{id}", Name = "UpdateProduct")]
    public IActionResult Update(int id, [FromBody] Product updatedProduct)
    {
        try
        {
            var product = products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
                return NotFound();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductID == id);
                
                if (product == null) return NotFound();
                bool removed = products.Remove(product);
                return NoContent();
        }


}