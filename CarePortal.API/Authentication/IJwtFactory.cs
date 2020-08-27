using System.Security.Claims;
using System.Threading.Tasks;
using CarePortal.Data.Models;

namespace CarePortal.API.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
