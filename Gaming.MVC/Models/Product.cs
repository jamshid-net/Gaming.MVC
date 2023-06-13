﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.MVC.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }=null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }
    public string? ProductImage { get; set; }

    public int? CategoryId { get; set;}
    public Category Category { get; set; }
}
