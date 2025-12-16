namespace Domain.Interfaces.v1.Context;
public interface IUserContext
{
    string UserName { get; }
    string Email { get; }
    string CompanyEmail { get; }
    string UserRole { get; }
}
