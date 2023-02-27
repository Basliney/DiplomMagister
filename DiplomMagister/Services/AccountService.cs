using DiplomMagister.Classes;
using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.StaticData;
using DiplomMagister.Data;
using DiplomMagister.Models;
using JWT_Example_ASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
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
                UserInfo = new UserInfo()
                {
                    FirstName = registerViewModel.FirstName,
                    Lastname = registerViewModel.LastName,
                    Mail = registerViewModel.Email,
                    Privacy = Privacy.Public
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
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var now = DateTime.UtcNow;
            var exp = now.Add(TimeSpan.FromMinutes(int.Parse(builder["Bearer:LIFETIME"])));
            var issuer = builder["Bearer:ISSUER"];
            var audience = builder["Bearer:AUDIENCE"];

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: exp,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            httpContext.Response.Cookies.Append("accessToken",  $"{encodedJwt}");
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

        internal void Logout(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();
            httpContext.Request.Headers.Remove("Authorization");
            httpContext.Response.Cookies.Delete("accessToken");
            var t = httpContext.Request.Headers;
        }
    }
}