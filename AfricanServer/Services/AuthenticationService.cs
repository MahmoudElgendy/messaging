using AfricanServer;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace africanserver.Services
{
    public class AuthenticationService : AuthProtoService.AuthProtoServiceBase
    {
        [AllowAnonymous]
        public override async Task<CreateIdentityResponse> GenerateToken(Empty request, ServerCallContext context)
        {
            try

            {
                var expiration = DateTime.UtcNow.AddHours(1);
                Claim[] claims = [new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySuperSecureKey!mySuperSecureKey!!"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7159",
                    audience: "https://localhost:7159",
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                );
                string _token = new JwtSecurityTokenHandler().WriteToken(token);
                await Task.Delay(100); // Simulate some async work
                return new CreateIdentityResponse
                {
                    Token = _token
                };
            }
            catch (System.Exception ex)

            {

                Console.WriteLine("EXCEPTION: " + ex);
                throw;
            }
        }
    }
}
