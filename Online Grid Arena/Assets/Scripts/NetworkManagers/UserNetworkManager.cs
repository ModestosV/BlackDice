using System.Collections;
using UnityEngine;

public class UserNetworkManager : AbstractNetworkManager
{
    public UserNetworkManager() : base(URLs.USER_URL)
    {
    }

    public IEnumerator CreateUser(UserDTO userDto)
    {
        yield return Post(mainURL + "/register", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\",\"username\":\"{userDto.Username}\"}}");
    }

    public IEnumerator Login(UserDTO userDto)
    {
        yield return Post(mainURL + "/login", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
    public IEnumerator Logout(UserDTO userDto)
    {
        yield return Post(mainURL + "/logout", $"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\"}}");
    }
}