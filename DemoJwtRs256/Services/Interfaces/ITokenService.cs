using System.Security.Cryptography;

namespace DemoJwtRs256.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(string user);
    RSA LoadPublicKey();
}
