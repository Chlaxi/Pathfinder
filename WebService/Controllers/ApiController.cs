using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.DataService;
using Microsoft.Extensions.Configuration;
using WebService.Middleware;
using DataLayer;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace WebService.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {

        private DataService ds = new DataService();
        private readonly IConfiguration config;

        public ApiController(IConfiguration config)
        {
            this.config = config;
        }

        public string Index()
        {

            return "Go to either races, spells, specials, classes, feats";
        }


        //TODO Link to log in
        [HttpPost("signup")]
        public ActionResult CreateUser([FromBody] UserCreationDTO newUser)
        {
            Console.WriteLine("Trying to create a new user. Information acquired\nUsername {0} \nPassword {1}", newUser.Username, newUser.Password);
            if (ds.GetPlayerByUsername(newUser.Username) != null)
            {
                return BadRequest("User already exists");
            }

            int.TryParse(config.GetSection("Auth:PwdSize").Value, out var size);

            if (size == 0)
            {
                throw new ArgumentException();
            }

              var salt = AuthService.SaltGenerator(size);

              var pwd = AuthService.HashPassword(newUser.Password, salt, size);
            
            Player player = ds.CreatePlayer(newUser.Username, pwd, salt);

            var newUserInfo = new
            { id = player.Id,
                name = player.Username,
                password = newUser.Password,
                token = GetToken(player.Id)
        };
            return CreatedAtRoute(null, newUserInfo);
        }

        [HttpPost("test/tokens")]
        public ActionResult LoginTest([FromBody] UserLoginDTO dto)
        {
            return Ok(String.Format("You tried to log in with username {0} and password {1}", dto.Username, dto.Password));
        }


        [HttpPost("tokens")]
        public ActionResult Login([FromBody] UserLoginDTO dto)
        {
            Player player = ds.GetPlayerByUsername(dto.Username);

            if (player == null)
                return NotFound("Wrong username");


            int.TryParse(config.GetSection("Auth:PwdSize").Value, out var size);

            if (size == 0)
            {
                throw new ArgumentException();
            }
            
            var pwd = AuthService.HashPassword(dto.Password, player.Salt, size);
            if (player.Password != pwd)
            {
                return BadRequest("Wrong Password");
            }


            var token = GetToken(player.Id);

            return Ok(new { player.Id, player.Username, token });


        }

        private string GetToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["Auth:Key"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("PlayerId", id.ToString()),
                }),
                Expires = DateTime.Now.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
    

}