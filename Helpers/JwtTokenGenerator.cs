using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator
{
    public string GenerateToken(string email)
    {
        var key = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());//secret key
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),//token claims
        };

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7233/",
            audience: "https://localhost:7233/",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), 
            signingCredentials: credentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}
