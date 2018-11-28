using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class UserNetworkManager : AbstractNetworkManager, IUserNetworkManager
{
    public UserNetworkManager() : base("/account") { }

    public async Task<HttpResponseMessage> CreateUserAsync(UserDTO userDto)
    {
        return await PostAsync("/register", JsonConvert.SerializeObject(userDto));
    }

    public async Task<HttpResponseMessage> LoginAsync(UserDTO userDto)
    {
        return await PostAsync("/login", JsonConvert.SerializeObject(userDto));
    }
    public async Task<HttpResponseMessage> LogoutAsync(UserDTO userDto)
    {
        return await PostAsync("/logout", JsonConvert.SerializeObject(userDto));
    }
}