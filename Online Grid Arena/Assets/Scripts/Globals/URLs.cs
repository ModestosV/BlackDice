public static class URLs
{
    // Developers by default will have requests pointed to local server. Users by default will have requests pointed to remote server.
    private static readonly string env = System.Environment.GetEnvironmentVariable("BLACK_DICE", System.EnvironmentVariableTarget.Machine);
    public static readonly string BASE_URL = string.Equals(env, "dev") ? "http://localhost:5500" : "http://159.203.27.234:5500";
    private static readonly string USER_TOKEN = "2b3c5cc8-24e1-4013-99d5-6606610aa2c1";
    public static readonly string URL_LOGGLY = "http://logs-01.loggly.com/inputs/"+ USER_TOKEN + "/tag/Unity3D"; 
}
