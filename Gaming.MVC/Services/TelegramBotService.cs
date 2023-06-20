using Telegram.Bot;

namespace Gaming.MVC.Services;

public class TelegramBotService
{
    private  TelegramBotClient? client { get; set; }
    private readonly IConfiguration _configuration;
    public TelegramBotService(IConfiguration configuration)
    {

        _configuration = configuration;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("NEW INSTANCE TELEGRAM BOT");
        Console.ResetColor();

    }
    public  TelegramBotClient GetBot()
    {
        if (client != null)
        {
            return client;
        }
        client = new TelegramBotClient(_configuration?.GetConnectionString("TelegramBot"));
        return client;
    }

}
