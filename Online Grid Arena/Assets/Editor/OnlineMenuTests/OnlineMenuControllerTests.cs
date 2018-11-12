using System;
using NSubstitute;
using NUnit.Framework;

public class OnlineMenuControllerTests
{
    OnlineMenuController sut;

    IRegistrationPanel registrationPanel;
    ILoginPanel loginPanel;

    IUserWebRequestService userWebRequestService;
    IUserController userController;

    const string VALID_EMAIL = "test@domain.com";
    const string INVALID_EMAIL = "09v8r0#3ur!jv9^8jasd(ofi8";

    const string VALID_PASSWORD = "Compl3x!ty_<Length";
    const string INVALID_PASSWORD = "pw";

    const string VALID_USERNAME = "0matleb2";
    const string INVALID_USERNAME = "0!";

    Action<IWebResponse> callback;
    IWebResponse webResponse;

    [SetUp]
    public void Init()
    {
        registrationPanel = Substitute.For<IRegistrationPanel>();
        loginPanel = Substitute.For<ILoginPanel>();

        userWebRequestService = Substitute.For<IUserWebRequestService>();
        callback = Substitute.For<Action<IWebResponse>>();
        webResponse = Substitute.For<IWebResponse>();

        userController = Substitute.For<IUserController>();
        userController.Email.Returns(VALID_EMAIL);
        userController.IsLoggedIn().Returns(false);


        sut = new OnlineMenuController
        {
            RegistrationPanel = registrationPanel,
            LoginPanel = loginPanel,
            UserWebRequestService = userWebRequestService,
            UserController = userController
        };
    }

    [Test]
    public void Register_with_valid_information_indicates_activity_and_sends_registration_request()
    {
        sut.Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).ActivateLoadingCircle();
        registrationPanel.Received(1).ClearStatus();
        userWebRequestService.Received(1).Register(VALID_EMAIL, VALID_PASSWORD, VALID_USERNAME, Arg.Any<Action<IWebResponse>>());
    }

    [Test]
    public void Register_with_invalid_email_displays_invalid_email_message()
    {
        sut.Register(INVALID_EMAIL, VALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_EMAIL_MESSAGE);
        userWebRequestService.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_password_displays_invalid_password_message()
    {
        sut.Register(VALID_EMAIL, INVALID_PASSWORD, VALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
        userWebRequestService.DidNotReceive();
    }

    [Test]
    public void Register_with_invalid_username_displays_invalid_username_message()
    {
        sut.Register(VALID_EMAIL, VALID_PASSWORD, INVALID_USERNAME);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_USERNAME_MESSAGE);
        userWebRequestService.DidNotReceive();
    }

    [Test]
    public void Login_indicates_activity_and_sends_login_request()
    {
        sut.Login(VALID_EMAIL, VALID_PASSWORD);

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userWebRequestService.Received(1).Login(VALID_EMAIL, VALID_PASSWORD, Arg.Any<Action<IWebResponse>>());
    }

    [Test]
    public void Logout_indicates_activity_and_sends_logout_request_for_active_account()
    {
        userController.IsLoggedIn().Returns(true);

        sut.Logout();

        loginPanel.Received(1).DisableLoginLogoutButtons();
        loginPanel.Received(1).ActivateLoadingCircle();
        loginPanel.Received(1).ClearStatus();
        userWebRequestService.Received(1).Logout(VALID_EMAIL, Arg.Any<Action<IWebResponse>>());
    }
}
