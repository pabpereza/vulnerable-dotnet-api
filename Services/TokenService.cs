namespace WebApi.Services;

using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;


public interface ITokenService
{
    string CreateToken(User user);
}

// Hay que ejecutar el siguiente comando en la consola de NuGet para instalar el paquete System.IdentityModel.Tokens.Jwt
// dotnet add package System.IdentityModel.Tokens.Jwt

public class TokenService : ITokenService
{
    private readonly string _secretKey;
    private readonly IMapper _mapper;
    public TokenService(IMapper mapper)
    {
        _secretKey="This is my custom Secret key for authentication";
        _mapper = mapper;
    }

    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}