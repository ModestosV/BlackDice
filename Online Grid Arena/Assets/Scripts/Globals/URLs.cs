public static class URLs
{
    // Developers by default will have requests pointed to local server. Users by default will have requests pointed to remote server.
    private static readonly string env = System.Environment.GetEnvironmentVariable("BLACK_DICE", System.EnvironmentVariableTarget.Machine);
    public static readonly string BASE_URL = string.Equals(env, "dev") ? "http://localhost:5500" : "http://159.203.27.234:5500";
    public static readonly string USER_URL = "/account";
}
