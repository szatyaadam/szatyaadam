using CookBook.API.Controllers.DTO;
using CookBook.API.Services;
using CookBook.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CookBook.Models.Models;
using CookBook.ApiClient.Models;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly cookbookContext _context;

        private readonly int USER_ROLE_ID = 3;

        public UserController(cookbookContext cookbookContext)
        {
            _context = cookbookContext;
        }

        /// <summary>
        /// Get actual <see cref="User"/> profile
        /// Method GET, endpoint api/user/profile
        /// </summary>
        /// <returns><see cref="UserWithMealsAndFavoritesDTO"/></returns>
        [HttpGet]
        [Authorize]
        [Route("profile")]
        public async Task<ActionResult<UserWithMealsAndFavoritesDTO>> GetUser()
        {
            User? actualUser = await _context.Users
                .Include(y => y.Favorites)
                .Include(t => t.Meals)
                .ThenInclude(f => f.MealType)
                .Include(g => g.Meals)
                .ThenInclude(h => h.Ingredients)
                .ThenInclude(m => m.Materials)
                .ThenInclude(u => u.UnitOfMeasure)
                .FirstOrDefaultAsync(x => x.Id == UserService.GetUserId(User));

            return Ok(actualUser.ToUserWithMealsAndFavoritesDTO());
        }
        //ez a User viewModellhez kell. 
        //TODO Authorizálni kell csak kezdésnek változtattam rajta hogy megjelenítsen adatot .
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TableDTO<User>>> GetAll(string? search = null,
            string? mType = null,
            int page = 0,
            int itemsPerPage = 0,
            string? sortBy = null,
            bool ascending = true)
        {
            var query = _context.Users
                .AsQueryable();

            var user = await _context.Users
                    .Include(role => role.Role)
                    .ToListAsync();
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "UserName":
                        query = ascending ? query.OrderBy(x => x.UserName) : query.OrderByDescending(x => x.UserName);
                        break;
                    case "Email":
                        query = ascending ? query.OrderBy(x => x.Email) : query.OrderByDescending(x => x.Email);
                        break;
                    case "RoleId":
                        query = ascending ? query.OrderBy(x => x.RoleId) : query.OrderByDescending(x => x.RoleId);
                        break;

                    default:
                        break;
                }
            }
            //összes kiválasztott elem db
            int count = await query.CountAsync();

            // Oldaltördelés
            if (page > 0 && itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            var result = await query.ToListAsync();

            return new TableDTO<User>(count, result);
        }

        /// <summary>
        /// Update <see cref="User"/>
        /// Method PUT, endpoint api/user/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut]
        [Authorize]
        [Route("modify")]
        public async Task<ActionResult<UserDTO>> ModifyUser(User user)
        {
            User? userClaims = await _context.Users
                .Include(x => x.Role)
    .           FirstOrDefaultAsync(u => u.Id == UserService.GetUserId(User));

            if (CheckNameAndEmail(user.UserName, user.Email))
            {
                return Conflict("Ez az email vagy felhasználónév már foglalt.");
            }

            user = UserService.SwapUser(userClaims, user);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Sikeres frissítés.");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("A felhasznátót nem sikerült frissíteni.");
            }

        }


        /// <summary>
        /// Create <see cref="User"/>
        /// Method POST, endpoint api/user/signup
        /// </summary>
        /// <returns>CreateAtAction <see cref="UserDTO"/></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("signup")]
        public async Task<ActionResult<UserDTO>> SignUpUser(User user)
        {
            var existUser = _context.Users.Any(x => x.UserName.Equals(user.UserName) || x.Email.Equals(user.Email));

            if (!existUser)
            {
                user.RoleId = USER_ROLE_ID;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Role = await _context.Roles.FindAsync(user.RoleId);
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("profile", new { id = user.Id }, user.ToUserDTO());
            }
            
            return BadRequest("Failer registration. Please check name and email.");
        }


        /// <summary>
        /// Delete <see cref="User"/> from database
        /// Method DELETE, endpoint: api/user
        /// </summary>
        /// <returns>Remove <see cref="User"/></returns>
        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> DeleteUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == UserService.GetUserId(User));

            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return Ok("Felhasználó törölve lett.");
            }

            return Conflict("A felhasználót nem sikerült törölni.");
        }

        /// <summary>
        /// Return <see cref="true"/> if username or email occupied.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns><see cref="bool"/></returns>
        public bool CheckNameAndEmail(string name, string email)
        {
            return _context.Users.Where(x => x.Id != UserService.GetUserId(User)).Any(u => u.UserName == name || u.Email == email );
        }
    }
}
