using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DiplomMagister.Classes
{
    public sealed class AuthOptions
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var key = builder["Bearer:Secret"];

            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
