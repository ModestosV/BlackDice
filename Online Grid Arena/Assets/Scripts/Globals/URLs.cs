public static class URLs
{
    // Note: None of these are being used at the moment. If working with the express-server directory to handle user
    // feedback, you can replace BASE_URL values with a local server path and live path respectively. If using Loggly
    // for live testing, simply enter a valid token in USER_TOKEN
    private static readonly string env = System.Environment.GetEnvironmentVariable("BLACK_DICE", System.EnvironmentVariableTarget.Machine);
    public static readonly string BASE_URL = string.Equals(env, "dev") ? "" : "";
    private static readonly string USER_TOKEN = "";
    public static readonly string URL_LOGGLY = "http://logs-01.loggly.com/inputs/"+ USER_TOKEN + "/tag/Unity3D";
}
