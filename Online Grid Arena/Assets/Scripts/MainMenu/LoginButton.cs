using UnityEngine;
using TMPro;

public class LoginButton : MonoBehaviour {

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI emailText;
    public TextMeshProUGUI passwordText;

    public void Login()
    {
        string email = emailText.text;
        string password = passwordText.text;

        if (!email.Contains("@") || password.Length < 8)
        {
            SetInvalidCredentialsStatusMessage();
            return;
        }

        SetSuccessfulLoginStatusMessage();
    }
    
    private void SetInvalidCredentialsStatusMessage()
    {
        statusText.text = "The email and password combination you have entered is ridiculously incorrect. Try again... or don't.";
    }

    private void SetSuccessfulLoginStatusMessage()
    {
        statusText.text = "You have been logged in successfully. I am so impressed.";
    }
}
