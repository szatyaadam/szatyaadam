using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Services;
using CookBook.ApiClient.Models.DTO;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookBook.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly cookbookContext _context;
        public AdminController(cookbookContext cookbookContext)
        {
            _context = cookbookContext;
        }

        #region Ivo készítette

        /// <summary>
        /// Get all <see cref="User"/> and create List from them.
        /// Method GET, endpoint: api/admin
        /// </summary>
        /// <returns><see cref="List{T}"/> from <see cref="UserDTO"/></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("user/all")]
        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            var user = await _context.Users
                .Include(role => role.Role)
                .ToListAsync();

            return user.ToListUsersDTO();
        }

        /// <summary>
        /// <see cref="User"/> retrieval by <see cref="User.Id"/>
        /// Method GET, endpoint: api/admin/id
        /// </summary>
        /// <returns>A single <see cref="User"/> from database</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}")]
        public async Task<ActionResult<UserWithMealsDTO>> GetUser(int id)
        {
            User? user = await _context.Users
                .Include(y => y.Favorites)
                .Include(t => t.Meals)
                .ThenInclude(f => f.MealType)
                .Include(g => g.Meals)
                .ThenInclude(h => h.Ingredients)
                .ThenInclude(m => m.Materials)
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user != null)
                return user.ToUserWithMealsDTO();


            return NotFound("Ez a felhasználó nem létezik.");
        }

        /// <summary>
        /// Admin can update the <see cref="User"/>
        /// Method PUT, endpoint api/admin/mod/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/Modify")]
        public async Task<ActionResult> AdminModifyUser(User user)
        {
            User? oldUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (oldUser == null)
                return BadRequest("Nincs ilyen felhasználó ezzel az ID-val.");
            var actual = UserService.GetUserId(User);
         
            if (oldUser.Id != actual&&oldUser.RoleId == 1)
                return Conflict("Nincs jogosultságod felülírni másik admint.");

            //UserService swapolja a beadott userek propertieit
            oldUser = UserService.SwapUser(oldUser, user);
        
            var ExistEmailOrName = await _context.Users
                .Where(user => user.UserName.Equals(oldUser.UserName) ||
                                     user.Email.Equals(oldUser.Email)).ToListAsync();
            if (ExistEmailOrName.Count>=2)
            {
                return BadRequest("Ez a felhasználó név vagy email már foglalt.");
            }

            _context.Entry(oldUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Sikeresen frissítetted a felhasználót.");
            } 
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("A felhasználót nem sikerült frissíteni.");
            }
        }
      
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("user/delete/{id}")]
        public async Task<ActionResult> AdminDeleteUser(int id)
        {
            try
            {

            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user != null)
            {
                if (user.RoleId != 1)
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok($"Sikeres törlés.");
                }
                return BadRequest("Admin felhasználót nem törölhetsz!");
            }
            return BadRequest("A kiválasztott felhasználó nem létezik.");
            }
            catch 
            {
                throw new Exception($"Valami hiba történt");
            }
        }
        #endregion

        #region Ádám készítette

        #region MEALTYPE

        /// <summary>
        /// Add new Mealtype ,only available for the admin
        /// </summary>
        /// <param name="newMealtype"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("mealtype/add")]
        public async Task<ActionResult<Mealtype>> AddNewMealtype(Mealtype newMealtype)
        {
            try
            {
                var newType = await _context.Mealtypes.FirstOrDefaultAsync(x => x.MealTypeName == newMealtype.MealTypeName);
                if (newType != null)
                {
                    return BadRequest("Ilyen Ételtípus már létezik");
                }
                else
                {
                    _context.Mealtypes.Add(newMealtype);
                    _context.SaveChanges();
                    return Ok("A " + newMealtype.MealTypeName + " hozzá lett adva az adatbázishoz.");
                }
            }
            catch { return BadRequest("Valami hibatörtént"); }
        }
        /// <summary>
        /// Remove a mealtype ,only available for the admin.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("mealtype/Delete")]
        [Route("mealtype/Delete/{id:int}")]
        public async Task<ActionResult<string>> DeleteMealType(int id)
        {
            try
            {
            var search = await _context.Mealtypes.FindAsync(id);
                if (search == null)
                {
                    return BadRequest("Ilyen ételtípus ezzel az azonosítóval nemtalálható az adatbáziban ");
                }
                else
                {
                    _context.Mealtypes.Remove(search);
                    _context.SaveChanges();
                    return Ok("Az ételtípus törölve lett az adatbázisból");
                }
            }
            catch { return BadRequest("Valami hiba történt."); }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("mealtype/Modify")]
        public async Task<ActionResult> ModifyMealtype(Mealtype modify )
        {
            try
            {
            var newMealType = await _context.Mealtypes.FirstOrDefaultAsync(x => x.Id ==modify.Id);

                newMealType.MealTypeName = modify.MealTypeName;
                _context.Mealtypes.Update(newMealType);
                _context.SaveChanges();
                return Ok("Az ételtípus módosult");
            }
            catch
            {
                return NotFound("Rossz adatot adott meg");
            }
        }
        #endregion
        #region MEASURE
        /// <summary>
        /// Add a New measure, only available for the admin
        /// </summary>
        /// <param name="newMeasure"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("measure/Add")]
        public ActionResult<Measures> NewMeasure(Measures newMeasure)
        {
            try
            {
                Measures measure = new Measures();
                measure.Measure= newMeasure.Measure;
                Measures exist = _context.Meassures.FirstOrDefault(x => x.Measure== newMeasure.Measure);
                if (exist != null)
                {
                    return BadRequest($"A {exist.Measure} már létezik az adatbázisban");
                }
                else
                { 
                    _context.Meassures.Add(measure);
                    _context.SaveChanges();
                    return Ok($"A {measure.Measure} nevű hozzávaló sikeresen felkerült az adatbázisba.");
                }
            }
            catch { return BadRequest("Valami hiba történt"); }
        }

        /// <summary>
        /// Delete a measure, only available for the admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("measure/Delete/{id:int}")]
        public async Task<ActionResult<Measures>> DeleteMeasure(int id)
        {
            try
            {
            var deletable = await _context.Meassures.FirstOrDefaultAsync(x => x.Id == id);
                if (deletable != null)
                {
                _context.Meassures.Remove(deletable);
                _context.SaveChanges();
                return Ok("A megadott "+deletable.Measure+" törölve lett az adatbázisból");

                }
                else return NotFound("A megadott azonosító-val nem szerepel hozzávaló");
            }
            catch
            {
                return NotFound("Valami hiba történt.");
            }
        }

        /// <summary>
        /// Modify a measure, only available for the admin
        /// </summary>
        /// <param name="modify"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("measure/Modify")]
        public async Task<ActionResult<Measures>> ModifMeassure(Measures modify)
        {
            try
            {
            var newMeasire = await _context.Meassures.FirstOrDefaultAsync(x => x.Id == modify.Id);

                newMeasire.Measure = modify.Measure;
                _context.Meassures.Update(newMeasire);
                _context.SaveChanges();
                return Ok("A Hozzávaló módosult");
            }
            catch
            {
                return NotFound("Rossz adatot adott meg");
            }
        }
        #endregion
        #endregion
    }
}
