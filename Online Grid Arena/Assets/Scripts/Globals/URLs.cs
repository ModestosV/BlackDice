public static class URLs
{
    private const bool IS_PROD = false;
    public const string BASE_URL = IS_PROD ? "http://159.203.27.234:5500" : "http://localhost:5500";
    public const string ACCOUNT_URL_FOLDER = "/account";
    public const string REGISTER_URL_PATH = ACCOUNT_URL_FOLDER + "/register";
    public const string LOGIN_URL_PATH = ACCOUNT_URL_FOLDER + "/login";
    public const string LOGOUT_URL_PATH = ACCOUNT_URL_FOLDER + "/logout";
}
