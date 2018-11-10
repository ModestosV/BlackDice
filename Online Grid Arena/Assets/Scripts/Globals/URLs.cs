public static class URLs
{
    private const bool IS_PROD = false;
    public const string BASE_URL = IS_PROD ? "http://159.203.27.234:5500" : "http://localhost:5500";
    public const string USER_URL = "account";
}
