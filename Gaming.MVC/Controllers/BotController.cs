using Gaming.MVC.Attributes;
using Gaming.MVC.Models;
using Gaming.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gaming.MVC.Controllers;



public class BotController : Controller
{
    
    private readonly TelegramBotService _telegram;

    public BotController(TelegramBotService telegram)
    {
        
        _telegram = telegram;
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromForm] ContactSended feedback)
    {

        
        if(ModelState.IsValid)
        {
            
            //33780774

           
            var fixedMessage = $"Name:{feedback.ContactUs.Name}\nEmail:{feedback.ContactUs.Email}\nSurname:{feedback.ContactUs.Surname}\nSubject:{feedback.ContactUs.Subject}\nMessage:{feedback.ContactUs.Message}";

            await _telegram.GetBot().SendTextMessageAsync(33780774, fixedMessage);

            feedback.IsSended = true;

            return RedirectToAction("Contact", "Home",feedback);
        }
        return RedirectToAction("Index","Home");
       
    }

    //[HttpPost("/api/bot")]
    //public Task<IActionResult> TelegramUpdate(Update update)
    //{
        
    //}
  
}
