using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.Services;
using Microsoft.IdentityModel.Tokens;

public class ASCIIJwtService : IJwtService{
     public string GenerateJwtToken(string username){
        var key=Encoding.ASCII.GetBytes("admin123admin123admin123admin0123");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string DecodeJwtToken(string jwtToken, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("admin123admin123admin123admin0123")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        ClaimsPrincipal principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
        string a="ASCIIJwtService: ";
        foreach (var claim in principal.Claims)
        {
            a += claim.Type + "=" + claim.Value + "    ";

        }

        return a.Trim();
        
    }

}