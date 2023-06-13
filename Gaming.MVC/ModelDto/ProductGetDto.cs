﻿namespace Gaming.MVC.ModelDto;

public class ProductGetDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }
    public string? ProductImage { get; set; }

    public int? CategoryId { get; set; }
}
