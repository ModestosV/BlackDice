using UnityEngine;
using TMPro;

public class RegisterButton : MonoBehaviour {

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI emailText;
    public TextMeshProUGUI passwordText;

    public void Register()
    {
        string email = emailText.text;
        string password = passwordText.text; 
         
        if (!email.Contains("@"))
        {
            SetInvalidEmailStatusMessage();
            return;
        }

        if (password.Length < 8)
        {
            SetInvalidPasswordMessage();
            return;
        }

        SetSuccessfulRegisterStatusMessage();
    }

    private void SetInvalidEmailStatusMessage()
    {
        statusText.text = "You have not entered a valid email. Come on, you know better.";
    }

    private void SetInvalidPasswordMessage()
    {
        statusText.text = "Your password sucks! Don't you know your password has to conform to the 32 arbitrary constraints we impose on passwords?";
    }

    private void SetSuccessfulRegisterStatusMessage()
    {
        statusText.text = "Well, you have successfully registered with those credentials, but you could probably do a lot better.";
    }
}
