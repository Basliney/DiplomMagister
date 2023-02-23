using DiplomMagister.Classes;
using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.StaticData;
using DiplomMagister.Data;
using DiplomMagister.Models;
using JWT_Example_ASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiplomMagister.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Login(ClaimsIdentity identity, HttpContext httpContext)
        {
            Authenticate(identity, httpContext);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="registerViewModel">Модель регистрируемого пользователя</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserData> Register(RegisterViewModel registerViewModel, HttpContext httpContext)
        {
            if (!FreeEmail(registerViewModel.Email)) { throw new UnauthorizedAccessException("Email is not free"); }

            UserClient uClient = new UserClient()
            {
                Id = Guid.NewGuid().ToString(),
                Userinfo = new UserInfo()
                {
                    FirstName = registerViewModel.FirstName,
                    Lastname = registerViewModel.LastName,
                },
            };

            UserData uData = new UserData()
            {
                Login = registerViewModel.Email,
                Password = CryptoService.HashPassword(registerViewModel.Password),
                Role = Role.User,
                UserClient = uClient
            };

            _context.UserClients.Add(uClient);
            _context.UsersData.Add(uData);
            await _context.SaveChangesAsync();

            var identity = GetIdentity(registerViewModel.Email, registerViewModel.Password);

            if (identity == null) { throw new BadHttpRequestException("User not found"); }

            try
            {
                Authenticate(identity, httpContext);
            }
            catch(Exception ex)
            {
                Log(ex.ToString());
                return null;
            }

            return uData;
        }

        public void Authenticate(ClaimsIdentity identity, HttpContext httpContext)
        {
            var now = DateTime.UtcNow;
            var exp = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: exp,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            httpContext.Response.Cookies.Append("accessToken", $"{encodedJwt}");

            var token = new Token()
            {
                EncodedJwt = encodedJwt,
                Expires = exp,
                NotBefore = now,
                Scope = ""
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();
        }

        public ClaimsIdentity? GetIdentity(string login, string password)
        {
            UserData person = _context.UsersData.Include(x=>x.UserClient).FirstOrDefault(x => x.Login.Equals(login));

            if (person == null || person.UserClient == null) { throw new Exception("User not found"); }

            if (!CryptoService.VerifyHashedPassword(person.Password, password))
            {
                throw new Exception("Identity wasn't found");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserClient.Id),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.GetEnumDescription())
                };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "accessToken", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        /// <summary>
        /// Проверка почты на занятость
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns></returns>
        public bool FreeEmail(string email) => !string.IsNullOrEmpty(email) && _context.UsersData.Select(x=>x.Login)
            .FirstOrDefault(x => x.Equals(email)) == null;

        public void Log(string text)
        {
            Debug.WriteLine($"\n\n\n{text}\n\n\n");
        }
    }
}