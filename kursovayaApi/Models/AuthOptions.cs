using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace kursovayaApi.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "SUPER_SECRET_KEY_FOR_MY_KURSOVAYA_PROJECT_1234567890!";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
