using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJwt(List<Claim> claims, int expirationInMinutes);

        List<Claim> GetClaimsFromJwt(string jwt);
    }
}
