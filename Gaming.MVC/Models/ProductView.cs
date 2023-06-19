using Gaming.MVC.Attributes;
using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Gaming.MVC.Models;

public class ProductView
{

    public Guid ProductId { get; set; }
    [MaxLength(15)]
    public string ProductName { get; set; } = null!;

    
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage ="PLease enter category name")]
    public string? CategoryName { get; set; }

    [Required(ErrorMessage = "Please select a file TTT.")]
    [DataType(DataType.Upload)]
    [AllowedExtensions(new string[] { ".jpg", ".png","jpeg" })]
    public IFormFile? ProductImage { get; set; }

}
