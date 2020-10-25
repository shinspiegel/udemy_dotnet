using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Database
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext context;
    private readonly IConfiguration configuration;

    public AuthRepository(DataContext context, IConfiguration configuration)
    {
      this.configuration = configuration;
      this.context = context;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
      ServiceResponse<int> response = new ServiceResponse<int>();

      if (await UserExist(user.UserName))
      {
        response.Status = false;
        return response;
      }

      CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await this.context.Users.AddAsync(user);
      await this.context.SaveChangesAsync();

      response.Data = user.Id;
      return response;
    }

    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
      ServiceResponse<string> response = new ServiceResponse<string>();

      User user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(username.ToLower()));

      if (user == null)
      {
        response.Status = false;
        return response;
      }

      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        response.Status = false;
        return response;
      }

      response.Data = CreateToken(user);

      return response;
    }

    public async Task<bool> UserExist(string username)
    {
      if (await this.context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower()))
      {
        return true;
      }
      return false;
    }

    private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(passwordBytes);
      }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
          {
            return false;
          }
        }
        return true;
      }
    }

    private string CreateToken(User user)
    {
      List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
      };

      SymmetricSecurityKey key = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(this.configuration.GetSection("AppSettings:Token").Value)
      );

      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds,
      };

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}