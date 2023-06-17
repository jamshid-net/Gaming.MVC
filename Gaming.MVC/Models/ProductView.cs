namespace Gaming.MVC.Models;

public class ProductView
{
    public Guid ProductId { get; set; }    
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }    
    public IFormFile? ProductImage { get; set; }

}
