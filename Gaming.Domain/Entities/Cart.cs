using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.Domain.Entities;

public class Cart
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartId { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }

    public int Quantity { get; set; }
}
