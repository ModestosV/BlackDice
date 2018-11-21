using System.Net.Http;
using System.Threading.Tasks;

public interface IUserNetworkManager
{
    Task<HttpResponseMessage> CreateUserAsync(UserDTO userDto);

    Task<HttpResponseMessage> LoginAsync(UserDTO userDto);
    Task<HttpResponseMessage> LogoutAsync(UserDTO userDto);
}
