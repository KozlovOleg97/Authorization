using static System.Net.WebRequestMethods;

namespace Authorization.JwtBearer.Constants
{
    public static class Constants
    {
        public const string Issuer = "https://localhost:7249"; 
        public const string Audience = Issuer;
        public const string SecretKey = "this_is_secret_key_for_jwt_token_generation";

    }
}
