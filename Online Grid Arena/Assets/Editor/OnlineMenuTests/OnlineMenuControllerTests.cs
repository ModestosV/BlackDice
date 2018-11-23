using NSubstitute;
using NUnit.Framework;

public class OnlineMenuControllerTests
{
    OnlineMenuController sut;
    IRegistrationPanel registrationPanel;
    ILoginPanel loginPanel;
    IUserNetworkManager userNetworkManager;
    IActivePlayer activePlayer;

    const string expectedEmail = "foo@bar.com";
    const string expectedPassword = "eightchr";
    const string expectedUsername = "bob";
    const string invalidEmail = "foobar";
    const string invalidPassword = "7chars!";
    const string invalidShortUsername = "hi";
    const string invalidLongUsername = "thisusernameis17c";

    [SetUp]
    public void Init()
    {
        registrationPanel = Substitute.For<IRegistrationPanel>();
        loginPanel = Substitute.For<ILoginPanel>();
        userNetworkManager = Substitute.For<IUserNetworkManager>();
        activePlayer = Substitute.For<IActivePlayer>();

        sut = new OnlineMenuController
        {
            RegistrationPanel = registrationPanel,
            LoginPanel = loginPanel,
            UserNetworkManager = userNetworkManager,
            ActivePlayer = activePlayer
        };
    }

    [Test]
    public void Register_with_valid_information_indicates_activity_and_sends_registration_request()
    {
        sut.Register(expectedEmail, expectedPassword, expectedUsername);

        registrationPanel.Received(1).ActivateLoadingCircle();
        registrationPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).CreateUserAsync(Arg.Any<UserDTO>());
    }

    [Test]
    public void Register_with_invalid_email_displays_invalid_email_message()
    {
        sut.Register(invalidEmail, expectedPassword, expectedUsername);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_EMAIL_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_password_displays_invalid_password_message()
    {
        sut.Register(expectedEmail, invalidPassword, expectedUsername);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_username_displays_invalid_username_message()
    {
        sut.Register(expectedEmail, expectedPassword, invalidShortUsername);
        sut.Register(expectedEmail, expectedPassword, invalidLongUsername);

        registrationPanel.Received(2).SetStatus(Strings.INVALID_USERNAME_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Login_indicates_activity_and_sends_login_request()
    {
        sut.Login(expectedEmail, expectedPassword);

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).LoginAsync(Arg.Any<UserDTO>());
    }

    [Test]
    public void Logout_indicates_activity_and_sends_logout_request_for_active_account()
    {
        activePlayer.IsLoggedIn().Returns(true);

        sut.Logout();

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).LogoutAsync(Arg.Any<UserDTO>());
    }
}
