using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CookBook.API.Data;
using CookBook.JWTSecurity.Models;
using CookBook.JWTSecurity.Services;
using CookBook.API.Controllers.DTO;
using CookBook.Models.Models;

namespace CookBook.Models.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly cookbookContext _context;
        private readonly JwtManagerService _jwtManagerService;

        public TokenController(cookbookContext context, JwtManagerService jwtManagerService)
        {
            _context = context;
            _jwtManagerService = jwtManagerService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login(UserLogin userLogin)
        {
            // Felhasználó kikeresése
            var dbUser = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserName == userLogin.Username);
            // Ha nem létezik a felhasználó
            if (dbUser == null)
            {
                return Unauthorized("A felhasználónév nincs regisztrálva.");
            }
            //Jelszó ellenőrzése
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, dbUser.Password))
            {
                return Unauthorized("Hibás felhasználónév vagy jelszó.");
            }

            //bejelentkezéskor a régi refress tokenek törlése
            var oldTokens = await _context.Tokens
                .Include(x => x.User)
                .Where(y => y.UserId == dbUser.Id)
                .ToListAsync();

            if (oldTokens != null) 
            { 
                foreach (var oldToken in oldTokens)
                {
                    _context.Tokens.Remove(oldToken);
                    await _context.SaveChangesAsync();
                }
            }


            // Követelési szintek létrehozása
            var claims = GetClaimsFromUser(dbUser);
            // Új Token generálása
            var jwtToken = _jwtManagerService.GenerateToken(claims);
            // Refresh token elmentése az adatbázisba
            _context.Tokens.Add(new Token(jwtToken.Refresh_Token, dbUser.Id));
            await _context.SaveChangesAsync();
            RedirectToRoute("http://localhost:5173/");
            // Felhasználói adatok és token visszaadása
            return new LoginDTO(dbUser.Id, dbUser.UserName, dbUser.Password , dbUser.Role.RoleName, jwtToken);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult<JwtToken>> Refresh(JwtToken jwtToken)
        {
            // Felhasználói adatok kinyerése a tokenből
            var principal = _jwtManagerService.GetPrincipalFromExpiredToken(jwtToken.Access_Token);
            var claimId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId.Value, out int userId);

            var dbUser = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.RoleId == userId);
            if (dbUser == null)
            {
                return Unauthorized("A felhasználónév nincs regisztrálva.");
            }
            // Token kikeresése
            var oldToken = await _context.Tokens
                .FirstOrDefaultAsync(x => x.Id == dbUser.Id && x.token == jwtToken.Refresh_Token);
            if (oldToken == null)
            {
                return BadRequest("Érvénytelen token.");
            }
            // Ha nem egyezik a refresh token vagy már lejárt
            if (oldToken.token != jwtToken.Refresh_Token || oldToken.Exp_date <= DateTime.Now)
            {
                // Régi lejárt token törlése
                _context.Tokens.Remove(oldToken);
                return Unauthorized("Lejárt vagy érvénytelen token.");
            }

            // Új token generálása
            var claims = GetClaimsFromUser(dbUser);
            var newToken = _jwtManagerService.GenerateToken(claims);
            // Refresh token elmentése az adatbázisba
            _context.Tokens.Add(new Token(newToken.Refresh_Token, dbUser.Id));
            await _context.SaveChangesAsync();

            // Új token érték visszaadása
            return newToken;
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(JwtToken jwtToken)
        {
            var claimId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId.Value, out int userId);

            var dbUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (dbUser != null)
            {
                var token = await _context.Tokens
                    .FirstOrDefaultAsync(x => x.UserId == dbUser.Id && x.token == jwtToken.Refresh_Token);
                if (token != null)
                {
                    _context.Tokens.Remove(token);
                    await _context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        // Követelési szintek létrehozása
        private List<Claim> GetClaimsFromUser(User users)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.UserName),
                new Claim(ClaimTypes.Role, users.Role.RoleName)
            };
        }
    }
}
