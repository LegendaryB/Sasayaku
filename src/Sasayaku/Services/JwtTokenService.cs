﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Sasayaku.Configuration;
using Sasayaku.Data;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sasayaku.Services
{
    public class JwtTokenService(
        ILogger<JwtTokenService> logger,
        IOptions<AuthenticationOptions> authenticationOptions)
    {
        public string GenerateToken(ClientCredentials credentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyAsBytes = Encoding.ASCII.GetBytes(authenticationOptions.Value.JwtSecretKey);
            var key = new SymmetricSecurityKey(keyAsBytes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, credentials.ClientId),
                    new Claim(ClaimTypes.Role, GetRole(credentials))
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GetRole(ClientCredentials credentials)
        {
            var superuser = authenticationOptions.Value.Superuser;

            if (credentials.ClientId != superuser.ClientId)
                return "user";

            if (credentials.ClientSecret != superuser.ClientSecret)
                return "user";

            return "admin";
        }
    }
}
