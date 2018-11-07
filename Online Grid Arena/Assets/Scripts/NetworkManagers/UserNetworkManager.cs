using System.Collections;

public class UserNetworkManager : AbstractNetworkManager
{
    private readonly string baseUrl = "http://localhost:5500/account";
    private UserDto userDto;

    public IEnumerator CreateUser(UserDto userDto)
    {
        this.userDto = userDto; 
        yield return Post(baseUrl + "/register", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\",\"username\":\"{userDto.Username}\"}}");
    }

    public IEnumerator Login(UserDto userDto)
    {
        this.userDto = userDto;
        yield return Post(baseUrl + "/login", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
    public IEnumerator Logout(UserDto userDto)
    {
        this.userDto = userDto;
        yield return Post(baseUrl + "/logout", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
}