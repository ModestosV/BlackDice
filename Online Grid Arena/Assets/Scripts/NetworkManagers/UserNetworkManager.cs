using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public sealed class UserNetworkManager : AbstractNetworkManager, IUserNetworkManager
{
    public UserNetworkManager() : base(URLs.BASE_URL + "/account") { }

    public async Task<IHttpResponseMessage> CreateUserAsync(UserDto userDto)
    {
        HttpResponseMessage response = await PostAsync("/register", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }

    public async Task<IHttpResponseMessage> LoginAsync(UserDto userDto)
    {
        HttpResponseMessage response = await PostAsync("/login", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }
    public async Task<IHttpResponseMessage> LogoutAsync(UserDto userDto)
    {
        HttpResponseMessage response = await PostAsync("/logout", JsonConvert.SerializeObject(userDto));
        return new HttpResponseMessageAdapter(response);
    }
}