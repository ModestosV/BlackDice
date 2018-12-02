using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class UserNetworkManager : AbstractNetworkManager, IUserNetworkManager
{
    public UserNetworkManager() : base("/api/account") { }

    public async Task<IHttpResponseMessage> CreateUserAsync(UserDTO userDto)
    {
        HttpResponseMessage response = await PostAsync("/register", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }

    public async Task<IHttpResponseMessage> LoginAsync(UserDTO userDto)
    {
        HttpResponseMessage response = await PostAsync("/login", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }
    public async Task<IHttpResponseMessage> LogoutAsync(UserDTO userDto)
    {
        HttpResponseMessage response = await PostAsync("/logout", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }
}