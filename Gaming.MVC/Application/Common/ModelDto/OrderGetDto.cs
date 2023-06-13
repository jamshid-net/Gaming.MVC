namespace Gaming.MVC.Application.Common.ModelDto;

public class OrderGetDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }

}

