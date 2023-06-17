using Gaming.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }

    public string UserId { get; set; }
    public virtual User User { get; set; }

    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; }
}
