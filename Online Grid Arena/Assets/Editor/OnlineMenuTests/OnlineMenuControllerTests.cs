/*
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

    const string RESPONSE_TEXT = "{\"_id\":\"5be912a46e634a45002951b7\",\"createdAt\":\"2018-11-12T05:41:56.887Z\",\"email\":\"aaa @aaa.com​\",\"passwordHash\":\"12ebb5319ff4f677c7588421aa0176ef82f4de5e8198fb9c044e96d8fccdd208fbc752d413348733f5c1c83f115e01a2fcd7aeda655d3bdc74ae6034b4f22bf6\",\"username\":\"aaaasdasdasd​\",\"__v\":0,\"loggedInToken\":\"DeyZgzaNHTKRfMs1eQyeUqTKr5v\"}";
    const string RESPONSE_USERNAME = "aaaasdasdasd​";

    IWebResponse webResponse;

    [SetUp]
    public void Init()
    {
        registrationPanel = Substitute.For<IRegistrationPanel>();
        loginPanel = Substitute.For<ILoginPanel>();

        userWebRequestService = Substitute.For<IUserWebRequestService>();

        webResponse = Substitute.For<IWebResponse>();
        webResponse.IsNetworkError.Returns(false);
        webResponse.ResponseCode.Returns(200);
        webResponse.ResponseText.Returns(RESPONSE_TEXT);

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

    #region RegisterCallback tests

    [Test]
    public void Register_callback_resets_activity_indication_and_enables_register_button()
    {
        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).EnableRegisterButton();
        registrationPanel.Received(1).DeactivateLoadingCircle();
    }

    [Test]
    public void Register_callback_sets_error_message_when_network_error()
    {
        webResponse.IsNetworkError.Returns(true);

        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Register_callback_sets_success_message_when_successful()
    {
        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
    }

    [Test]
    public void Register_callback_sets_error_message_when_response_has_code_400()
    {
        webResponse.ResponseCode.Returns(400);

        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Register_callback_sets_duplicate_message_when_response_has_code_412()
    {
        webResponse.ResponseCode.Returns(412);

        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).SetStatus(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
    }

    [Test]
    public void Register_callback_sets_server_error_message_when_response_has_code_500()
    {
        webResponse.ResponseCode.Returns(500);

        sut.RegisterCallback(webResponse);

        registrationPanel.Received(1).SetStatus(Strings.SERVER_ERROR_MESSAGE);
    }

#endregion

    #region LoginCallback tests

    [Test]
    public void Login_callback_resets_activity_indication_and_enables_login_logout_buttons()
    {
        sut.LoginCallback(webResponse);

        loginPanel.Received(1).EnableLoginLogoutButtons();
        loginPanel.Received(1).DeactivateLoadingCircle();
    }

    [Test]
    public void Login_callback_sets_error_message_when_network_error()
    {
        webResponse.IsNetworkError.Returns(true);

        sut.LoginCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Login_callback_sets_success_message_and_logged_in_user_when_successful()
    {
        sut.LoginCallback(webResponse);

        loginPanel.Received(1).SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {RESPONSE_USERNAME}.");
        userController.Received(1).LoggedInUser = Arg.Any<UserDTO>();
        loginPanel.Received(1).ToggleLoginLogoutButtons();
    }

    [Test]
    public void Login_callback_sets_error_message_when_response_has_code_400()
    {
        webResponse.ResponseCode.Returns(400);

        sut.LoginCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
    }

    [Test]
    public void Login_callback_sets_server_error_message_when_response_has_code_500()
    {
        webResponse.ResponseCode.Returns(500);

        sut.LoginCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    #endregion

    #region LogoutCallback tests

    [Test]
    public void Logout_callback_resets_activity_indication_and_enables_login_logout_buttons()
    {
        sut.LogoutCallback(webResponse);

        loginPanel.Received(1).EnableLoginLogoutButtons();
        loginPanel.Received(1).DeactivateLoadingCircle();
    }


    [Test]
    public void Logout_callback_sets_error_message_when_network_error()
    {
        webResponse.IsNetworkError.Returns(true);

        sut.LogoutCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    [Test]
    public void Logout_callback_sets_success_message_and_logged_in_user_when_successful()
    {
        sut.LogoutCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
        userController.Received(1).LoggedInUser = null;
        loginPanel.Received(1).ToggleLoginLogoutButtons();
    }

    [Test]
    public void Logout_callback_sets_error_message_when_response_has_code_400()
    {
        webResponse.ResponseCode.Returns(400);

        sut.LogoutCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.LOGOUT_FAIL_MESSAGE);
    }

    [Test]
    public void Logout_callback_sets_error_message_when_response_has_code_500()
    {
        webResponse.ResponseCode.Returns(500);

        sut.LogoutCallback(webResponse);

        loginPanel.Received(1).SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }
    #endregion
}
*/
