using Gaming.Domain.Entities;
using System.Collections;

namespace Gaming.MVC.Models;

public class ProductAndCategoryViewModels
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categories { get; set; }  
}
