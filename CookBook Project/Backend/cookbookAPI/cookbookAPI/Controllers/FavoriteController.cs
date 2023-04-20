using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Services;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

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
        [Route("Add/{mealId}")]
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
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            Favorite? result = await context.Favorites.FirstOrDefaultAsync(x => x.Id == id);
        
            if (result == null)
            {
                return NotFound("Ilyen recept nem szerepel a kedvencek között!");
            }
            else
            {
                context.Favorites.Remove(result);
                context.SaveChanges();

                return Ok("A recept törölve lett a kedvencek közül!");

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


        /// <summary>
        /// Return the top 10 meals what have most likes.
        /// Method GET, endpoint: api/favorite/top
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("top")]
        public async Task<ActionResult<List<TopMealDTO>>> GetTopMeals()
        {
            var likesWithMealIds = await context.Favorites
                .GroupBy(x => x.MealsId)
                .Select(y => new
                {
                    likes = y.Count(),
                    mealId = y.Key
                })
                .OrderByDescending(z => z.likes)
                .Take(10)
                .ToListAsync();

            if (likesWithMealIds.Count > 0)
            {
                List<TopMealDTO> TopList = new();

                for (int i = 0; i < likesWithMealIds.Count; i++)
                {
                    var item = context.Meals
                        .Include(type => type.MealType)
                        .FirstOrDefault(index => index.Id == likesWithMealIds.ElementAt(i).mealId);

                    if (item != null)
                    {
                        TopMealDTO meal = item.ToTopMealDTO(likesWithMealIds.ElementAt(i).likes, i + 1);

                        TopList.Add(meal);
                    }
                }

                return Ok(TopList);
            }

            return Conflict("Nem sikerült a toplista lekérése!");
        }
    }
}
