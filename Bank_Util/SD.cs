namespace Bank_Util
{
    public static class SD
    {
        public enum Roles
        {
            Admin,
            User
        }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static string SessionToken = "JWTToken";

    }
}
