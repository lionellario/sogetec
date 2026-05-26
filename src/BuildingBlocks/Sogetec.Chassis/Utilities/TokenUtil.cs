using Microsoft.IdentityModel.Tokens;

namespace Sogetec.Chassis.Utilities;

public static class TokenUtil
{
    public static string GenerateAccessToken(string secretKey, string audience, IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var exp = now.AddMinutes(GlobalConstant.Duration.ACCESS_TOKEN_MINUTE);

        var tokenOptions = new JwtSecurityToken(
          issuer: GlobalConstant.JWTKey.ISSUER,
          audience: audience,
          claims: claims,
          expires: exp,
          notBefore: now,
          signingCredentials: credentials
        );

        var token = tokenHandler.WriteToken(tokenOptions);

        return token;
    }
}
