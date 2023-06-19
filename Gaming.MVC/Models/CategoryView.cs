using Gaming.MVC.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Gaming.MVC.Models;

public class CategoryView
{
    public Guid Id { get; set; }

    [Required(ErrorMessage ="ENTER CATEGORY NAME")]
    [MaxLength(15)]
    public string CategoryName { get; set; } = null!;

    [Required(ErrorMessage = "Please select a file TTT.")]
    [DataType(DataType.Upload)]
    [AllowedExtensions(new string[] { ".jpg", ".png", "jpeg" })]
    public IFormFile? CategoryImage { get; set; }
}
