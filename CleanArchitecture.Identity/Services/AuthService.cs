using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
        }




        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }
            var resultado = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);


            if(!resultado.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await GenerateToken(user);

            var auth = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName,
            };

            return auth;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await userManager.FindByNameAsync(request.Email);

            if(existingUser != null)
            {
                throw new Exception($"El username ya esta utilizado");
            }

            var existingMail = await userManager.FindByEmailAsync(request.Email);
            
            if (existingMail != null)
            {
                throw new Exception($"El correo ya esta utilizado");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var resultado = await userManager.CreateAsync(user,request.Password);

            if(resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Operator");

                var token = await GenerateToken(user);

                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id,
                    Username = user.UserName,
                };
            }

            throw new Exception($"{resultado.Errors}");

        }
    
        public async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
        
            foreach(var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signinCredentials);


            return jwtSecurityToken;
        }
    
    }
}
