using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.Services;
using Microsoft.IdentityModel.Tokens;

public class Base64JwtService : IJwtService{
     public string GenerateJwtToken(string username){
        var b64=Convert.ToBase64String(Encoding.UTF8.GetBytes("admin123admin123admin123admin0123"));
        var key=Convert.FromBase64String(b64);
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
        var b64=Convert.ToBase64String(Encoding.UTF8.GetBytes(secretKey));
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(b64)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        ClaimsPrincipal principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
        string a="Base64JwtService: ";
        foreach (var claim in principal.Claims)
        {
            a += claim.Type + "=" + claim.Value + "    ";

        }

        return a.Trim();
        
    }

}