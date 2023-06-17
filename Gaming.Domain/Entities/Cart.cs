using Gaming.Domain.Identity;

namespace Gaming.Domain.Entities;

public class Cart
{
    public Guid CartId { get; set; }

    public string UserId { get; set; }
    public virtual User User { get; set; }
    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; }

    public int Quantity { get; set; }
}
