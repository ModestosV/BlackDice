using Newtonsoft.Json;
using System.Collections;

public class UserNetworkManager : AbstractNetworkManager
{
    public UserNetworkManager() : base(URLs.USER_URL) { }

    public IEnumerator CreateUser(AccountDTO userDto)
    {
        yield return Post(mainURL + "/register", JsonConvert.SerializeObject(userDto));
    }

    public IEnumerator Login(AccountDTO userDto)
    {
        yield return Post(mainURL + "/login", JsonConvert.SerializeObject(userDto));
    }
    public IEnumerator Logout(AccountDTO userDto)
    {
        yield return Post(mainURL + "/logout", JsonConvert.SerializeObject(userDto));
    }
}