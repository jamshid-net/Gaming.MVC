namespace Gaming.MVC.Models;

public class CategoryView
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public IFormFile? CategoryImage { get; set; }
}
