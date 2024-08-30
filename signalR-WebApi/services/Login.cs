using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Services;

namespace Service;

public class Login : ILogin
{
    public const string secretKey = "32144334334343433443434399021332113";
    public const string issuer = "signalr-side-project";
    Result ILogin.Login(User user)
    {
        System.Console.WriteLine("Login service");
        string token = GenerateJwtToken(user);
        return new Result() { Success = true, Value = token};
    }
    /// <summary>
    /// Generate JWT token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string GenerateJwtToken(User user) {
        // TODO: Generate JWT token with user information
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey); // TODO: Use a secure key
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = Login.issuer
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var strToken = tokenHandler.WriteToken(token);

        System.Console.WriteLine("strToken {0}", strToken);
        return strToken;

    }
}