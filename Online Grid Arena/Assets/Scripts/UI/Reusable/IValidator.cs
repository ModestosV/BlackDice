public interface IValidator
{
    bool ValidateEmail(string email);
    bool ValidateUsername(string username);
    bool ValidatePassword(string password);
}
