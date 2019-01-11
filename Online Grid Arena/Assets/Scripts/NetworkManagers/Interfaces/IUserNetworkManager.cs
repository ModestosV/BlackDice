using System.Threading.Tasks;

public interface IUserNetworkManager
{
    Task<IHttpResponseMessage> CreateUserAsync(UserDTO userDto);

    Task<IHttpResponseMessage> LoginAsync(UserDTO userDto);
    Task<IHttpResponseMessage> LogoutAsync(UserDTO userDto);
}
