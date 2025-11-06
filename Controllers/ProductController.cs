using Microsoft.AspNetCore.Mvc;
using SoftAPINew.Models;
using SoftAPINew.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataRepository _dataRepository;
    private readonly ProductStoredProcedures _storedProcedures;

    public ProductController(IDataRepository dataRepository, IOptions<ProductStoredProcedures> storedProcedures)
    {
        _dataRepository = dataRepository;
        _storedProcedures = storedProcedures.Value;
    }
    
    // HTTP GET method to retrieve all products
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _dataRepository.GetDataAsync(_storedProcedures.GetAllProducts);
            var products = data.Select(row => new Product
            {
                ProductID = Convert.ToInt32(row["ProductID"]),
                CategoryId = Convert.ToInt32(row["CategoryID"]),
                ProductCode = row["ProductCode"]?.ToString() ?? "",
                ProductName = row["ProductName"]?.ToString() ?? "",
                Description = row["Description"]?.ToString() ?? "",
                ListPrice = Convert.ToDecimal(row["ListPrice"]),
                DiscountPercent = Convert.ToDecimal(row["DiscountPercent"])
            }).ToList();

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
            var data = await _dataRepository.GetDataAsync(_storedProcedures.GetProductById, parameters);
            
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
    [HttpPost(Name = "CreateProduct")]
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

            var result = await _dataRepository.GetDataAsync(_storedProcedures.InsertProduct, parameters);
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

            await _dataRepository.GetDataAsync(_storedProcedures.UpdateProduct, parameters);
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
            await _dataRepository.GetDataAsync(_storedProcedures.DeleteProduct, parameters);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}