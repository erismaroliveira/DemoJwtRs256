using DemoJwtRs256.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DemoJwtRs256.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string user)
    {
        var rsa = LoadPrivateKey();
        var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RSA LoadPublicKey()
    {
        var publicKeyBase64 = _configuration["JwtSettings:PublicKey"];
        var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);

        using var reader = new StringReader(Encoding.UTF8.GetString(publicKeyBytes));
        var pemReader = new PemReader(reader);
        var keyParameter = pemReader.ReadObject();

        if (keyParameter is RsaKeyParameters rsaKeyParameters)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(DotNetUtilities.ToRSAParameters(rsaKeyParameters));
            return rsa;
        }
        throw new InvalidOperationException("Formato de chave pública inválido.");
    }

    private RSA LoadPrivateKey()
    {
        var privateKeyBase64 = _configuration["JwtSettings:PrivateKey"];
        var privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

        using var reader = new StringReader(Encoding.UTF8.GetString(privateKeyBytes));
        var pemReader = new PemReader(reader);
        var keyParameter = pemReader.ReadObject();

        if (keyParameter is RsaPrivateCrtKeyParameters rsaPrivateKeyParameters)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(DotNetUtilities.ToRSAParameters(rsaPrivateKeyParameters));
            return rsa;
        }
        throw new InvalidOperationException("Formato de chave privada inválido.");
    }
}
