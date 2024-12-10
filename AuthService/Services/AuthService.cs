using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class AuthService : Auth.AuthBase
{
    private readonly Models.AuthContext _context;
    private readonly ILogger<AuthService> _logger;
    private const string SecretKey = "54a65f43-aab1-4810-ad8b-734174a14500"; // Replace with your secret key
    private readonly SymmetricSecurityKey _signingKey = new(Encoding.UTF8.GetBytes(SecretKey));
    
    public AuthService(ILogger<AuthService> logger, Models.AuthContext context)
    {
        _logger = logger;
        _context = context;
        
    }
    
    public override Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        
        _context.Auths.Add(new Models.Auth()
        {
            Username = request.Username,
            Hpassword = request.Password,
        });

        var reply = new LoginReply
        {
            Token = tokenString,
            Status = 200
        };
        return Task.FromResult(reply);
    }


    public override Task<VerifyReply> Verify(VerifyRequest request, ServerCallContext context)
    {
        return base.Verify(request, context);
    }
}