namespace Gaming.MVC.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? UserName { get; }
    int? UserId { get; }
}
