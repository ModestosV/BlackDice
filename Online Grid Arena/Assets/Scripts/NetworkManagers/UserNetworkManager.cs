using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class UserNetworkManager : AbstractNetworkManager
{
    public UserNetworkManager() : base(URLs.USER_URL) { }

    public IEnumerator CreateUser(UserDTO userDto)
    {
        yield return Post(mainURL + "/register", JsonConvert.SerializeObject(userDto));
    }

    public IEnumerator Login(UserDTO userDto)
    {
        yield return Post(mainURL + "/login", JsonConvert.SerializeObject(userDto));
    }
    public IEnumerator Logout(UserDTO userDto)
    {
        yield return Post(mainURL + "/logout", JsonConvert.SerializeObject(userDto));
    }
}