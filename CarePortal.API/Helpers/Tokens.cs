using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarePortal.API.Authentication;
using CarePortal.Data.Models;

namespace CarePortal.API.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, string userRole, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            //var response = new
            //{
            //    id = identity.Claims.Single(c => c.Type == "id").Value,
            //    username = userName,
            //    role = userRole,
            //    auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
            //    expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
            //};

            //return JsonConvert.SerializeObject(response, serializerSettings);
            return await jwtFactory.GenerateEncodedToken(userName, identity);
        }
    }
}
