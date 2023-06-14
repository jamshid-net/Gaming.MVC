using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.Domain.Entities;

public class Product
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }
    public string? ProductImage { get; set; }

    public int? CategoryId { get; set; }
    public virtual Category Category { get; set; }


    public virtual ICollection<Cart> Carts { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}
