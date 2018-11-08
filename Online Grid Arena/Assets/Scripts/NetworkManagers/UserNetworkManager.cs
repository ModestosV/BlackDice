using System.Collections;

public class UserNetworkManager : AbstractNetworkManager
{
    private readonly string baseUrl = "http://localhost:5500/account";

    public IEnumerator CreateUser(UserDTO userDto)
    {
        yield return Post(baseUrl + "/register", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\",\"username\":\"{userDto.Username}\"}}");
    }

    public IEnumerator Login(UserDTO userDto)
    {
        yield return Post(baseUrl + "/login", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
    public IEnumerator Logout(UserDTO userDto)
    {
        yield return Post(baseUrl + "/logout", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
}