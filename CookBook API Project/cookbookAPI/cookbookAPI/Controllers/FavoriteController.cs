using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Services;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly cookbookContext context = new cookbookContext();

        public FavoriteController(cookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add new meal to the favorites
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("Add")]
        public async Task<ActionResult<Favorite>> NewFavorites(int mealId)
        {
            try 
            {               
                var meal= context.Meals.FirstOrDefault(x => x.Id == mealId);
                if (meal==null) return NotFound("A megadott recept nem szerepel az adatbázisban");
               
                {
                    Favorite? result = await context.Favorites.FirstOrDefaultAsync(x=>x.UserId==UserService.GetUserId(User)&&x.MealsId==meal.Id);
   

                    if (result != null)
                    {
                        return NotFound("Ez az étel már szerepel a kedvencek között.");
                    }

                    Favorite favor= new Favorite();
                    favor.MealsId = meal.Id;
                    
                    favor.UserId =UserService.GetUserId(User);
                
                    context.Favorites.Add(favor);
                    context.SaveChanges();
                    return Ok("Az étel sikeresen hozzá lett adva a kedvencekhez");
                }
            }
            catch { return BadRequest("Hibás adatot adatt meg!"); }
        }

        /// <summary>
        /// Delete one favorite from the favorites by name
        /// </summary>
        /// <param name="mealName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteFavorite(int mealId)
        {
           
            Meal meal = await context.Meals.FirstOrDefaultAsync(x => x.Id == mealId);
            if (meal==null) 
            {
                return NotFound("Ilyen recept nem létezik ");
            }
            Favorite result = await context.Favorites.FirstOrDefaultAsync(x => x.MealsId == meal.Id&&x.UserId==UserService.GetUserId(User));
        
            if (result == null)
            {
                return NotFound("Ilyen recept nem szerepel a kedvencek között ");
            }
            else
            {
                context.Favorites.Remove(result);
                context.SaveChanges();

                return Ok("A megadott kedvenc törölve lett a kedvencek közül");

            }
        }

        /// <summary>
        /// Get all the favorites meal 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<List<MealDTO>>> GetAllFavorites()
        {
            try
            {
                var favorits = await context.Favorites.Where(x => x.UserId ==UserService.GetUserId(User)).ToListAsync();

                if (favorits != null)
                {
                    List<Meal> meals  = new();
                    foreach (var favorit in favorits)
                    {
                       Meal? meal = await context.Meals
                            .Include(x => x.MealType)
                            .Include(x => x.Ingredients)
                            .ThenInclude(x => x.Materials)
                            .ThenInclude(x => x.UnitOfMeasure)
                            .FirstOrDefaultAsync(x => x.Id == favorit.MealsId);

                        if (meal != null)
                        {
                            meals.Add(meal);
                        }
                        else
                        {
                            context.Favorites.Remove(favorit);
                        }
                    }
                    context.SaveChanges();
                    return Ok(meals.ToListMealsDTO());
                }
                return Ok("Nincs még kedvelt recept.");
            }
            catch (Exception ex) { return BadRequest("Valami hiba történt"); }
        }
    }
}
