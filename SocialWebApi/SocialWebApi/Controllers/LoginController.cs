using CodeFirst.Data;
using CodeFirst.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly SocialDbContext _db;

        public LoginController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMemoryCache memoryCache, IDistributedCache distributedCache, SocialDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _db = db;
        }

        //Id'ye göre kullanıcının postlarının memorycache ve distributed cachede tutan ve gösteren method
        [HttpGet("CacheGetUserPost")]
        [ResponseCache(Duration = 1800, VaryByHeader = "UserPost", VaryByQueryKeys = new string[] { "userPostId" })]
        public IEnumerable<UserPost> GetUserPost(int userPostId)
        {
            UserPost[] userPosts = _db.UserPosts.Where(x => x.PostId == userPostId).ToArray();

            if (_memoryCache.TryGetValue("userposts", out userPosts))
            {
                return userPosts;
            }

            var userPostsByts = _distributedCache.Get("userposts");
            var userPostsJson = Encoding.UTF8.GetString(userPostsByts);
            var userPostsArr = JsonSerializer.Deserialize<UserPost[]>(userPostsJson);

            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions();
            memoryCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            memoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromHours(1);
            memoryCacheEntryOptions.Priority = CacheItemPriority.Normal;

            _memoryCache.Set("userposts", userPosts, memoryCacheEntryOptions);

            var dstUserPostsArr = JsonSerializer.Serialize(userPosts);

            _distributedCache.Set("userposts", Encoding.UTF8.GetBytes(dstUserPostsArr));

            return userPosts;
        }

        //Kullanıcı giriş yaptığında kullanıcıya JWT token atanmasını sağlayan method(Role ve email tokenda tutuluyor). Yetkili giriş yapılmadığında yetkilendirme hatası olarak status code dönüyor.
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User userLogin)
        {
            List<Claim> claims = new List<Claim>();
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null) throw new Exception("Böyle bir emaile sahip kullanıcı bulunmamaktadır");

            var result = await _userManager.CheckPasswordAsync(user, userLogin.PasswordHash);
            if (result)
            {
                var roles = await _userManager.GetRolesAsync((User)user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                claims.Add(new Claim(ClaimTypes.Name, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = GetToken(claims);

                var handler = new JwtSecurityTokenHandler();
                string jwt = handler.WriteToken(token);

                return Ok(new
                {
                    token = jwt,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }


        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                 signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                 issuer: _configuration["JWT:Issuer"],
                 audience: _configuration["JWT:Audience"],
                 expires: DateTime.Now.AddHours(6),
                 claims: claims
                );

            return token;
        }
    }
}
