using Microsoft.AspNetCore.Mvc;
using SoftAPINew.Models;
using SoftAPINew.Infrastructure.Interfaces;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataRepository _repo;

    public ProductController(IDataRepositoryFactory factory)
    {
        _repo = factory.Create("MyGuitarShop");
    }


    // HTTP GET method to retrieve all products
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var rows = await _repo.GetDataAsync("GetAllProducts");
            var products = rows.Select(ConvertToProduct).ToList();
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP GET method to retrieve a product by ID
    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var parameters = new Dictionary<string, object?> { { "ProductID", id } };
            var data = await _repo.GetDataAsync("GetProductById", parameters);
            
            var productData = data.FirstOrDefault();
            if (productData == null)
                return NotFound();

            var product = new Product
            {
                ProductID = Convert.ToInt32(productData["ProductID"]),
                CategoryId = Convert.ToInt32(productData["CategoryID"]),
                ProductCode = productData["ProductCode"]?.ToString() ?? "",
                ProductName = productData["ProductName"]?.ToString() ?? "",
                Description = productData["Description"]?.ToString() ?? "",
                ListPrice = Convert.ToDecimal(productData["ListPrice"]),
                DiscountPercent = Convert.ToDecimal(productData["DiscountPercent"])
            };

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP POST method to add a new product
    [HttpPost(Name = "AddProduct")]
    public async Task<IActionResult> Create([FromBody] Product newProduct)
    {
        try
        {
            if (newProduct == null)
                return BadRequest("Invalid product data.");

            var parameters = new Dictionary<string, object?>
            {
                { "CategoryID", newProduct.CategoryId },
                { "ProductCode", newProduct.ProductCode },
                { "ProductName", newProduct.ProductName },
                { "Description", newProduct.Description },
                { "ListPrice", newProduct.ListPrice },
                { "DiscountPercent", newProduct.DiscountPercent }
            };

            var result = await _repo.GetDataAsync("AddProduct", parameters);
            var insertedData = result.FirstOrDefault();
            
            if (insertedData != null && insertedData.ContainsKey("ProductID"))
            {
                newProduct.ProductID = Convert.ToInt32(insertedData["ProductID"]);
                return CreatedAtRoute("GetProductById", new { id = newProduct.ProductID }, newProduct);
            }

            return StatusCode(500, "Failed to create product.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP PUT method to update an existing product
    [HttpPut("{id}", Name = "UpdateProduct")]
    public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
    {
        try
        {
            if (updatedProduct == null)
                return BadRequest("Invalid product data.");

            var parameters = new Dictionary<string, object?>
            {
                { "ProductID", id },
                { "CategoryID", updatedProduct.CategoryId },
                { "ProductCode", updatedProduct.ProductCode },
                { "ProductName", updatedProduct.ProductName },
                { "Description", updatedProduct.Description },
                { "ListPrice", updatedProduct.ListPrice },
                { "DiscountPercent", updatedProduct.DiscountPercent }
            };

            await _repo.GetDataAsync("UpdateProduct", parameters);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // HTTP DELETE method to delete a product by ID
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var parameters = new Dictionary<string, object?> { { "ProductID", id } };
            await _repo.GetDataAsync("DeleteProduct", parameters);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    
    private static Product ConvertToProduct(IDictionary<string, object?> row)
    {
        return new Product
        {
            ProductID = row["ProductID"] != DBNull.Value ? Convert.ToInt32(row["ProductID"]) : 0,
            CategoryId = row["CategoryID"] != DBNull.Value ? Convert.ToInt32(row["CategoryID"]) : 0,
            ProductCode = row["ProductCode"]?.ToString() ?? string.Empty,
            ProductName = row["ProductName"]?.ToString() ?? string.Empty,
            Description = row["Description"]?.ToString() ?? string.Empty,
            ListPrice = row["ListPrice"] != DBNull.Value ? Convert.ToDecimal(row["ListPrice"]) : 0.0m,
            DiscountPercent = row["DiscountPercent"] != DBNull.Value ? Convert.ToDecimal(row["DiscountPercent"]) : 0.0m 
        };
    }

}