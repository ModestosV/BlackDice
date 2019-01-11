using System.Net.Mail;
using System.Text.RegularExpressions;

public sealed class Validator : IValidator
{
    public bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public bool ValidatePassword(string password)
    {
        bool isPasswordLongEnough = password.Length > 7;

        return isPasswordLongEnough;
    }

    public bool ValidateUsername(string username)
    {
        Regex regex = new Regex("^[a-zA-Z0-9]{3,16}$");
        bool isUserNameValid = regex.IsMatch(username);

        return isUserNameValid;
    }
}
