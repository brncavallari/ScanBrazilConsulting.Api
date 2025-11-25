namespace Domain.Interfaces.v1.Context;
public interface IUserContext
{
    string UserName { get; }
    string UserEmail { get; }
    string UserRole { get; }
}
