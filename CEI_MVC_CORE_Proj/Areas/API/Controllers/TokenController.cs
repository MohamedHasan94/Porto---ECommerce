using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CEI_MVC_CORE_Proj.Areas.API.Controllers
{
    public class TokenController : Controller
    {
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public TokenController(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        
        [Route("/Token")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            List<string> credentialsSplited = new List<string>();
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                credentialsSplited = reader.ReadToEndAsync().Result.Split('&').ToList();
            }
            foreach (var item in credentialsSplited)
            {
                string[] arr = item.Split('=');
                credentials.Add(arr[0], arr[1]);
            }
            if (await IsValidCredintials(credentials["username"], credentials["password"]))
            {
                return new ObjectResult(await CreateToken(credentials["username"]));
            }
            return BadRequest("Invalid Credintials");
        }

       
        private async Task<bool> IsValidCredintials(string userName, string password)
        {
            AppUser user = await userManager.FindByNameAsync(userName);
            IList<string> roles = await userManager.GetRolesAsync(user);
            return await userManager.CheckPasswordAsync(user, password);
        }

        private async Task<object> CreateToken(string userName)
        {
            AppUser user = await userManager.FindByNameAsync(userName);
            IList<string> roles = await userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecrectSecurityKey12483082#@")),
                    SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));

            return new { Access_Token = new JwtSecurityTokenHandler().WriteToken(token), UserName = user.UserName };
        }
    }
}