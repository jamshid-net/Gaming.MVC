
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;


namespace Gaming.MVC.Models;

public class ContactUsViewModel
{

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "NAME UNCORRECT")]
    public string Name { get; set; }
    [DataType(DataType.EmailAddress,ErrorMessage ="EMAIL NOTOGRI")]
    public string Email { get; set; }   
    public string Surname { get; set; } 
    public string Subject { get; set; } 
    public string Message { get; set; }
}
