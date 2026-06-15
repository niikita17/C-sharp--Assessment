using Application.DTOs;
using Application.Interfaces;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUnitOfWork uniOfWork,
            IConfiguration configuration)
        {
            _uniOfWork = uniOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(
    RegisterDto dto)
        {
            var existingUser =
                await _uniOfWork.Auth .GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Exception(
                    "User already exists");
            }

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        dto.Password),

                Role = "User"
            };

            await _uniOfWork.Auth.AddUserAsync(user);

            await _uniOfWork.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(
    LoginDto dto)
        {
            var user =
                await _uniOfWork.Auth
                    .GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception(
                    "Invalid credentials");
            }

            bool isValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isValid)
            {
                throw new Exception(
                    "Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

            var key =
           new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(
                   _configuration["Jwt:Key"]!));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],
                    claims: claims,

                expires:
                    DateTime.UtcNow.AddHours(2),

                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
