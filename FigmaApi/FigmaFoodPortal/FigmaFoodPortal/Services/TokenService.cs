using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#nullable disable


namespace FoodPortal.Services
{
    public class TokenService : ITokenGenerate
    {
        #region Field
        private readonly SymmetricSecurityKey _key;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        #endregion

        #region service method to generate token based on user
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns> token in string data type</returns>
        public string GenerateToken(User userDTO)
        {
            string token = string.Empty;
            //User identity
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userDTO.UserName),
                new Claim(ClaimTypes.Role,userDTO.Role),
                new Claim(ClaimTypes.Email,userDTO.EmailId),
                new Claim(ClaimTypes.MobilePhone,userDTO.PhoneNumber),
                new Claim(ClaimTypes.Name,userDTO.Name)
            };
            //Signature algorithm
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            //Assembling the token details
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            //Using teh handler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            token = tokenHandler.WriteToken(myToken);
            return token;
        }
        #endregion
    }
}
