using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AbacasX.UI.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AbacasX.UI.Apis
{

    public class UserClass
    {
        public string UserName;
        public string Password;
        public string Role;
        public int clientId;
    }

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        Dictionary<string, UserClass> UserDictionary = new Dictionary<string, UserClass>();

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // GET api/values
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (UserDictionary.Count() == 0)
            {
                UserClass[] users = new UserClass[] {
                new UserClass { UserName = "TradezDigital",     Password = "Abacas123", Role = "Investor", clientId = 1 },
                new UserClass { UserName = "DigitalBroker",     Password = "Abacas123", Role = "Broker", clientId = 2 },
                new UserClass { UserName = "DigitalInvestor",   Password = "Abacas123", Role = "Investor", clientId = 3 },
                new UserClass { UserName = "AbacasAdmin",       Password = "Abacas123", Role = "Admin", clientId = 0 },
                new UserClass { UserName = "KingdomTrust",      Password = "Abacas123", Role = "Custodian", clientId = 0 },
                new UserClass { UserName = "Guest",             Password = "Guest123", Role = "Guest", clientId = 0 },
                new UserClass { UserName = "AbacasOps",         Password = "Abacas123", Role = "Ops", clientId = 0 }};

                foreach (UserClass userRecord in users)
                {
                    UserDictionary.Add(userRecord.UserName, userRecord);
                }
            }
            
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (UserDictionary.TryGetValue(user.UserName, out UserClass loginAccount) == false)
            {
                return Unauthorized();
            }

            if (user.UserName == loginAccount.UserName && user.Password == loginAccount.Password)
            {

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


                var tokeOptions = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Issuer"],
                    claims: new List<Claim> {
                        new Claim(ClaimTypes.System,"AbacasXChange"),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, loginAccount.Role)
                    },
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}