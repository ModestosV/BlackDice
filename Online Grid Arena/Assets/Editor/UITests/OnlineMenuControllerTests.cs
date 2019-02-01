using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using System.Net;

public class OnlineMenuControllerTests
{
    OnlineMenuController sut;
    IRegistrationPanel registrationPanel;
    ILoginPanel loginPanel;
    IUserNetworkManager userNetworkManager;
    IActivePlayer activePlayer;
    IHttpResponseMessage responseMessage;
    UserDto userDTO;

    const string VALID_EMAIL = "foo@bar.com";
    const string VALID_PASSWORD = "eightchr";
    const string VALID_USERNAME = "bob";
    const string INVALID_EMAIL = "foobar";
    const string INVALID_PASSWORD = "7chars!";
    const string INVALID_SHORT_USERNAME = "hi";
    const string INVALID_LONG_USERNAME = "thisusernameis17c";
    const string LOGGED_IN_TOKEN = "a8n2kao30cd0al3m";

    static readonly string validPasswordHash = Hash128.Compute(VALID_PASSWORD).ToString();

    [SetUp]
    public void Init()
    {
        registrationPanel = Substitute.For<IRegistrationPanel>();
        loginPanel = Substitute.For<ILoginPanel>();
        userNetworkManager = Substitute.For<IUserNetworkManager>();
        activePlayer = Substitute.For<IActivePlayer>();
        responseMessage = Substitute.For<IHttpResponseMessage>();

        responseMessage.ReadContentAsStringAsync().Returns(LOGGED_IN_TOKEN);

        sut = new OnlineMenuController
        {
            RegistrationPanel = registrationPanel,
            LoginPanel = loginPanel,
            UserNetworkManager = userNetworkManager,
            ActivePlayer = activePlayer,
            Validator = new Validator()
        };
    }

    [Test]
    public void Register_with_valid_information_indicates_activity_and_sends_registration_request()
    {
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).ActivateLoadingCircle();
        registrationPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            ));
        registrationPanel.Received(1).DeactivateLoadingCircle();
    }

    [Test]
    public void Registration_disables_register_button_while_in_progress()
    {
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).DisableRegisterButton();
        registrationPanel.Received(1).EnableRegisterButton();
    }

    [Test]
    public void Registration_with_201_response_code_sets_registration_success_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.Created);
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
    }

    [Test]
    public void Registration_with_400_response_code_sets_connectivity_issues_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.BadRequest);
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Registration_with_412_response_code_sets_duplicate_keys_message()
    {
        responseMessage.StatusCode.Returns((HttpStatusCode)412);
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
    }

    [Test]
    public void Registration_with_500_response_code_sets_registration_success_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.InternalServerError);
        userNetworkManager.CreateUserAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash && x.Username == VALID_USERNAME
            )).Returns(responseMessage);

        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.SERVER_ERROR_MESSAGE);
    }

    [Test]
    public void Register_with_invalid_email_displays_invalid_email_message()
    {
        sut.Register(INVALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_EMAIL_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_password_displays_invalid_password_message()
    {
        sut.Register(VALID_EMAIL, INVALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_username_displays_invalid_username_message()
    {
        sut.Register(VALID_EMAIL, VALID_PASSWORD, INVALID_SHORT_USERNAME);
        sut.Register(VALID_EMAIL, VALID_PASSWORD, INVALID_LONG_USERNAME);

        registrationPanel.Received(2).SetStatus(Strings.INVALID_USERNAME_MESSAGE);
        userNetworkManager.DidNotReceive();
    }

    [Test]
    public void Login_indicates_activity_and_sends_login_request()
    {
        userNetworkManager.LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            )).Returns(responseMessage);

        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            ));
        loginPanel.Received(1).DeactivateLoadingCircle();
    }

    [Test]
    public void Login_disables_login_logout_while_in_progress()
    {
        userNetworkManager.LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            )).Returns(responseMessage);

        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).EnableLoginLogoutButtons();
    }

    [Test]
    public void Login_with_200_response_code_updates_active_player_and_status_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.OK);
        userNetworkManager.LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            )).Returns(responseMessage);

        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        activePlayer.Received(1).LoggedInUser = Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            );
        loginPanel.Received(1).ToggleLoginLogoutButtons();
        loginPanel.Received(1).SetStatus(Arg.Is<string>(x => x.Contains(Strings.LOGIN_SUCCESS_MESSAGE)));
    }

    [Test]
    public void Login_with_400_response_code_sets_invalid_credentials_status_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.BadRequest);
        userNetworkManager.LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            )).Returns(responseMessage);

        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        loginPanel.Received(1).SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
    }

    [Test]
    public void Login_with_500_response_code_sets_invalid_credentials_status_message()
    {
        responseMessage.StatusCode.Returns(HttpStatusCode.InternalServerError);
        userNetworkManager.LoginAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.PasswordHash == validPasswordHash
            )).Returns(responseMessage);

        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Logout_indicates_activity_and_sends_logout_request_for_active_account()
    {
        activePlayer.IsLoggedIn().Returns(true);
        userDTO = new UserDto(VALID_EMAIL) { LoggedInToken = LOGGED_IN_TOKEN };
        activePlayer.LoggedInUser.Returns(userDTO);
        userNetworkManager.LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            )).Returns(responseMessage);

        sut.Logout();

        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userNetworkManager.Received(1).LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            ));
        loginPanel.Received(1).DeactivateLoadingCircle();
    }

    [Test]
    public void Logout_disables_login_logout_buttons_while_in_progress()
    {
        activePlayer.IsLoggedIn().Returns(true);
        userDTO = new UserDto(VALID_EMAIL) { LoggedInToken = LOGGED_IN_TOKEN };
        activePlayer.LoggedInUser.Returns(userDTO);
        userNetworkManager.LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            )).Returns(responseMessage);

        sut.Logout();

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).EnableLoginLogoutButtons();
    }

    [Test]
    public void Logout_with_200_response_code_updates_active_player_and_status_message()
    {
        activePlayer.IsLoggedIn().Returns(true);
        userDTO = new UserDto(VALID_EMAIL) { LoggedInToken = LOGGED_IN_TOKEN };
        activePlayer.LoggedInUser.Returns(userDTO);
        responseMessage.StatusCode.Returns(HttpStatusCode.OK);
        userNetworkManager.LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            )).Returns(responseMessage);

        sut.Logout();

        activePlayer.Received(1).Logout();
        loginPanel.Received(1).ToggleLoginLogoutButtons();
        loginPanel.Received(1).SetStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
    }

    [Test]
    public void Logout_with_400_response_code_sets_logout_failed_status_message()
    {
        activePlayer.IsLoggedIn().Returns(true);
        userDTO = new UserDto(VALID_EMAIL) { LoggedInToken = LOGGED_IN_TOKEN };
        activePlayer.LoggedInUser.Returns(userDTO);
        responseMessage.StatusCode.Returns(HttpStatusCode.BadRequest);
        userNetworkManager.LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            )).Returns(responseMessage);

        sut.Logout();

        loginPanel.Received(1).SetStatus(Strings.LOGOUT_FAIL_MESSAGE);
    }

    [Test]
    public void Logout_with_500_response_code_sets_connectivity_issues_message()
    {
        activePlayer.IsLoggedIn().Returns(true);
        userDTO = new UserDto(VALID_EMAIL) { LoggedInToken = LOGGED_IN_TOKEN };
        activePlayer.LoggedInUser.Returns(userDTO);
        responseMessage.StatusCode.Returns(HttpStatusCode.InternalServerError);
        userNetworkManager.LogoutAsync(Arg.Is<UserDto>(
            x => x.Email == VALID_EMAIL && x.LoggedInToken == LOGGED_IN_TOKEN
            )).Returns(responseMessage);

        sut.Logout();

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }
}
