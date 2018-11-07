using System.Collections;

public class UserNetworkManager : AbstractNetworkManager
{
    private string baseUrl = "http://localhost:5500/account";
    private UserDto userDto;

    public IEnumerator CreateUser(UserDto userDto)
    {
        this.userDto = userDto; 
        yield return Post(baseUrl + "/register", $"{{\"password\":\"{userDto.passwordHash}\",\"email\":\"{userDto.email}\",\"username\":\"{userDto.username}\"}}");
    }
}