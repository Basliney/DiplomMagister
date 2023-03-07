using DiplomMagister.Classes;
using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.StaticData;
using DiplomMagister.Data;
using DiplomMagister.Models;
using JWT_Example_ASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

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
        public async Task<ProfileSettings> Register(RegisterViewModel registerViewModel, HttpContext httpContext)
        {
            if (!FreeEmail(registerViewModel.Email)) { throw new UnauthorizedAccessException("Email is not free"); }

            var dateNow = DateTime.UtcNow;
            UserClient uClient = new UserClient()
            {
                Id = Guid.NewGuid().ToString(),
                ProfileInformation = new ProfileInformation()
                {
                    Name = $"user{dateNow.Ticks}",
                    EditingDate = dateNow,
                    FirstName = registerViewModel.FirstName,
                    Lastname = registerViewModel.LastName,
                    Mail = registerViewModel.Email,
                    Privacy = Privacy.Public
                },
            };

            ProfileSettings uData = new ProfileSettings()
            {
                Login = registerViewModel.Email,
                Password = CryptoService.HashPassword(registerViewModel.Password),
                Role = Role.User,
                Owner = uClient,
                CreationDate = DateTime.UtcNow
            };


            _context.UserClients.Add(uClient);
            _context.ProfileSettings.Add(uData);
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

            try
            {
                httpContext.Response.Cookies.Append("accessToken", $"{encodedJwt}");
            }
            catch(Exception ex)
            {
                httpContext.Response.Cookies.Delete("accessToken");
                httpContext.Response.Cookies.Append("accessToken", $"{encodedJwt}");
            }
        }

        public AccountService RenewTokenOfAuthorizedUser(HttpContext httpContext, string id)
        {
            ProfileSettings person = GetUserDataById(id);
            ClaimsIdentity claimsIdentity = GetClaimsIdentity(person);

            if (claimsIdentity == null) { throw new BadHttpRequestException("User not found"); }

            try
            {
                Authenticate(claimsIdentity, httpContext);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
                return null;
            }

            Debug.WriteLine("\n\n\nToken was renew\n\n\n");
            return this;
        }

        public ClaimsIdentity? GetIdentity(string login, string password)
        {
            ProfileSettings person = GetUserData(login);
            VerifyPassword(password, person);
            ClaimsIdentity claimsIdentity = GetClaimsIdentity(person);

            return claimsIdentity;
        }

        private ClaimsIdentity GetClaimsIdentity(ProfileSettings person)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Owner.Id),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.GetEnumDescription())
                };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "accessToken", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private void VerifyPassword(string password, ProfileSettings person)
        {
            if (!CryptoService.VerifyHashedPassword(person.Password, password))
            {
                throw new Exception("Identity wasn't found");
            }
        }

        private ProfileSettings GetUserData(string login)
        {
            ProfileSettings person = _context.ProfileSettings.Include(x => x.Owner).FirstOrDefault(x => x.Login.Equals(login));
            if (person == null || person.Owner == null) { throw new Exception("User not found"); }

            return person;
        }

        private ProfileSettings GetUserDataById(string id = "")
        {
            ProfileSettings person = _context.ProfileSettings.Include(x => x.Owner).FirstOrDefault(x => x.Owner.Id.Equals(id));
            if (person == null || person.Owner == null) { throw new Exception("User not found"); }

            return person;
        }

        /// <summary>
        /// Проверка почты на занятость
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns></returns>
        public bool FreeEmail(string email) => !string.IsNullOrEmpty(email) && _context.ProfileSettings.Select(x=>x.Login)
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
        }
    }
}