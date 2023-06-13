using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.MVC.Domain.Models;

public class Admin
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdminId { get; set; }
    public string AdminName { get; set; }
    public string Password { get; set; }

    public string Role { get; set; }
}
