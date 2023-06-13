namespace Gaming.MVC.Application.Common.Interfaces;

public interface IHashStringService
{
    Task<string> GetHashStringAsync(string text);
}
