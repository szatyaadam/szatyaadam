﻿using CookBook.JWTSecurity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CookBook.JWTSecurity.Services
{
    public class JwtManagerService
    {
        // Read from appsettings.json
        private readonly IConfiguration _configuration;

        private string Key;
        private string Issuer;
        private string Audience;

        public JwtManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtManagerService(string key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }
        public JwtToken TESTGenerateToken(List<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(Key);
            var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                        Issuer,
                        Audience,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(20),

                        signingCredentials: signIn);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            return new JwtToken { Access_Token = accessToken, Refresh_Token = refreshToken };
        }
        /// <summary>
        /// Generate a JWT token with claims.
        /// </summary>
        /// <param name="claims">Claims</param>
        /// <returns>JwtToken</returns>
        public JwtToken GenerateToken(List<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(45),

                        signingCredentials: signIn);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            return new JwtToken { Access_Token = accessToken, Refresh_Token = refreshToken };
        }

        /// <summary>
        /// Generate a random refresh token.
        /// </summary>
        /// <returns>BASE64 Encoded random number</returns>
        public static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        /// <summary>
        /// Gets Principal claims from expired access token.
        /// </summary>
        /// <param name="accessToken">JWT access token value</param>
        /// <returns>ClaimsPrincipal</returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
