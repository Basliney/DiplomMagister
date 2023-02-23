using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DiplomMagister.Classes
{
    public class AuthOptions
    {
        /*
         * Необходимо докачать через NuGet
         * Microsoft.AspNetCore.Authentication.JwtBearer
         */

        public const string ISSUER = "TestingAuthService";    // Издатель токена
        public const string AUDIENCE = "TestingService";  // Потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 15;  // Время жизни токена - 15 минут

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
