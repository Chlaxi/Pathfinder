using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;

namespace WebService.Middleware
{
    public class AuthService
    {

        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public static string SaltGenerator(int size)
        {
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public static string HashPassword(string password, string salt, int size)
        {
            var _salt = Encoding.UTF8.GetBytes(salt);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, _salt, KeyDerivationPrf.HMACSHA256, 10000, size));
        }

        /// <summary>
        /// Checks whether the player is authorized to the content
        /// </summary>
        /// <param name="context">The HttpContext from the webservice</param>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>Returns true if the player is authorised</returns>
        public static bool AuthorizePlayer(HttpContext context, int playerId)
        {
            
            foreach (var claim in context.User.Claims)
            {
                var pid = -1;
                if (claim.Type.Equals("PlayerId"))
                {
                    Int32.TryParse(claim.Value, out pid);
                    Console.WriteLine("The authorized player had the following claims: {0} with value {1}", claim.Type, claim.Value);
                    if (pid == playerId) return true;

                }
            }
            
            return false;

        }

        public static bool AuthorizeCharacters()
        {


            return false;
        }
    }
}
