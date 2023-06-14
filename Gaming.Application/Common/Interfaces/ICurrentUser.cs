namespace Gaming.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? UserName { get; }
    int? UserId { get; }
}
