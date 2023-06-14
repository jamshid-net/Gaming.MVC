namespace Gaming.Application.Common.Interfaces;

public interface IHashStringService
{
    Task<string> GetHashStringAsync(string text);
}
