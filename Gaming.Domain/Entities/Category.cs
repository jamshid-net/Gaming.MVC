using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? CategoryImage { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
