namespace JwtAuthApi.Services;

public interface IJwtService
{
    string GenerateJwtToken(string username);
    string DecodeJwtToken(string jwtToken, string secretKey);
}