using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class UserNetworkManager : AbstractNetworkManager
{
    public UserNetworkManager() : base(URLs.USER_URL) { }

    public async Task<HttpResponseMessage> CreateUserAsync(UserDTO userDto)
    {
        return await PostAsync(mainURL + "/register", JsonConvert.SerializeObject(userDto));
    }

    public async Task<HttpResponseMessage> LoginAsync(UserDTO userDto)
    {
        return await PostAsync(mainURL + "/login", JsonConvert.SerializeObject(userDto));
    }
    public async Task<HttpResponseMessage> LogoutAsync(UserDTO userDto)
    {
        return await PostAsync(mainURL + "/logout", JsonConvert.SerializeObject(userDto));
    }
}