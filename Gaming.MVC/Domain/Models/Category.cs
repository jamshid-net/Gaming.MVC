using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.MVC.Domain.Models;

public class Category
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? CategoryImage { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
