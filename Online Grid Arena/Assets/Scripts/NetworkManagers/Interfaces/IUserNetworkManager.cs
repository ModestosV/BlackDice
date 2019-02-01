using System.Threading.Tasks;

public interface IUserNetworkManager
{
    Task<IHttpResponseMessage> CreateUserAsync(UserDto userDto);

    Task<IHttpResponseMessage> LoginAsync(UserDto userDto);
    Task<IHttpResponseMessage> LogoutAsync(UserDto userDto);
}
