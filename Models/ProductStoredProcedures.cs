namespace SoftAPINew.Models
{
    public class ProductStoredProcedures
    {
        public string GetAllProducts { get; set; } = "GetAllProducts";
        public string GetProductById { get; set; } = "GetProductByID";
        public string InsertProduct { get; set; } = "InsertProduct";
        public string UpdateProduct { get; set; } = "UpdateProduct";
        public string DeleteProduct { get; set; } = "DeleteProduct";
    }
}