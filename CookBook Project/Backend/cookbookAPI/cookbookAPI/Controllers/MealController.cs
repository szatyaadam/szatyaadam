using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Services;
using CookBook.ApiClient.Models;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly cookbookContext context = new cookbookContext();

        public MealController(cookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a new recept to database
        /// </summary>
        /// <param name="newrecept"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<MealDTO>> NewRecept(Meal newrecept)
        {
            Meal recept = new()
            {
                UserId = UserService.GetUserId(User),
                MealName = newrecept.MealName,
                PreperationTime = newrecept.PreperationTime,
                Price = newrecept.Price,
                Photo = newrecept.Photo,
                MealTypeId = newrecept.MealTypeId,
                Discription = newrecept.Discription
            };
            //mealtype id megadásával hozzákötjük a recepthez
            Mealtype? mT = await context.Mealtypes.FirstOrDefaultAsync(x => x.Id == newrecept.MealTypeId);

            if (mT != null)
            {
                recept.MealType= mT;
            }
            //a recept láthatóságának beállítása
            if (newrecept.Privacy == 1) recept.Privacy = newrecept.Privacy;
            else recept.Privacy = 0;

            try
            {
                //az recept hozzáadása az adatbázishoz
                context.Meals.Add(recept);
                context.SaveChanges();

                List<Ingredient>? NotSavedIngredients = new();
                //a hozzáadott recept ingredient-jeinek a feltöltése
                foreach (var ingredient in newrecept.Ingredients)
                {
                    Ingredient i = new()
                    {
                        MealId = recept.Id,
                        Quantity = ingredient.Quantity
                    };
                    //létezik e már a megadott material
                    Material? material = context.Materials
                        .Include(x => x.UnitOfMeasure)
                        .FirstOrDefault(x => x.IngredientName == ingredient.Materials.IngredientName &&
                                             x.UnitOfMeasureId == ingredient.Materials.UnitOfMeasureId);

                    if (material != null)
                    {
                        //ha nem létezik még a material akkor hozzáadom az adatbázishoz
                        i.Materials = material;
                        context.Add(i);
                    }
                    else
                    {
                        Material m = new()
                        {
                            IngredientName = ingredient.Materials.IngredientName,
                            UnitOfMeasureId = ingredient.Materials.UnitOfMeasureId
                        };

                        Measures? meassure = context.Meassures.Find(m.UnitOfMeasureId);

                        //TODO kiíratás kellen a fel nem vitt Ingredientről

                        if (meassure == null)
                        {
                            continue;
                        }
                        m.UnitOfMeasure = meassure;
                        context.Materials.Add(m);
                        i.Materials = m;
                        context.Add(i);
                    }
                    context.Ingredients.Add(i);
                }
                context.SaveChanges();
                return CreatedAtAction("newRecept", new { id = recept.Id }, recept.ToMealDTO());
            }
            catch (Exception)
            {
                throw new Exception("Nem sikerült felvinni a receptet.");
            }
        }

        /// <summary>
        /// Delete one recepet by id if you are who created that or you are the admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        //Delete one meal from the database by id 
        public async Task<IActionResult> DeleteRecept(int id)
        {
            var meal = await context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound("Ilyen recept nem létezik ");
            }
            try
            {
                if (UserService.GetUserId(User) != meal.UserId && UserRole(UserService.GetUserId(User)) != 1) return BadRequest("Ön nem jogosult törölni ezt a receptet.");

                else
                {

                    var result = await context.Meals.FirstOrDefaultAsync(x => x.Id == id);
                    if (result != null) context.Meals.Remove(result);

                    context.Meals.Remove(meal);
                    await context.SaveChangesAsync();

                    return Ok("A megadott recept törölve lett az adatbázisból");

                }


            }

            catch
            {
                return BadRequest("Valami hiba történt.");
            }

        }


        /// <summary>
        /// Search meal with different parameters
        /// </summary>
        /// <param name="search"></param>
        /// <returns><see cref="MealDTO"/></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TableDTO<MealDTO>>> SearchMeal(
            string? search = null,
            string? mType = null,
            int page = 0,
            int itemsPerPage = 0,
            string? sortBy = null,
            bool ascending = true)
        {

            var query = context.Meals
                .Include(x => x.MealType)
                .Include(x => x.User)
                .Include(x => x.Ingredients)
                .ThenInclude(y => y.Materials)
                .ThenInclude(z => z.UnitOfMeasure)
                .Where(x => x.Privacy == 0)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                int.TryParse(search, out int price);

                query = query
                    .Where(x => x.Price.Equals(price) ||
                    x.MealName.Contains(search) ||
                    x.Ingredients.Select(x => x.Materials.IngredientName).Contains(search))
                    .OrderBy(x => x.Privacy == 0);
            }

            if (!string.IsNullOrWhiteSpace(mType))
            {
                query = query.Where(x => x.MealType.MealTypeName.Contains(mType));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "MealName":
                        query = ascending ? query.OrderBy(x => x.MealName) : query.OrderByDescending(x => x.MealName);
                        break;
                    case "MealType":
                        query = ascending ? query.OrderBy(x => x.MealType) : query.OrderByDescending(x => x.MealType);
                        break;
                    case "Price":
                        query = ascending ? query.OrderBy(x => x.Price) : query.OrderByDescending(x => x.Price);
                        break;
                    case "Ingredients.Material.IngredientName":
                        query = ascending ? query.OrderBy(x => x.Ingredients.OrderBy(y => y.Materials.IngredientName)) :
                                            query.OrderByDescending(x => x.Ingredients.OrderBy(y => y.Materials.IngredientName));
                        break;
                    case "PreperationTime":
                        query = ascending ? query.OrderBy(x => x.PreperationTime) : query.OrderByDescending(x => x.PreperationTime);
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

            return new TableDTO<MealDTO>(count, result.ToListMealsDTO());
        }

        /// <summary>
        /// The admin or the user who crate the recept can only modify it .
        /// </summary>
        /// <param name="newRecept"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("modify")]
        public async Task<ActionResult<Meal>> ModifyMeals(Meal newRecept)
        {


            try
            {

                Meal recept = await context.Meals.FirstOrDefaultAsync(x => x.Id == newRecept.Id);
                if (recept.UserId != UserService.GetUserId(User) && UserRole(UserService.GetUserId(User)) != 1) return BadRequest("Ön nem jogosult módosítani ezt a receptet.");
                if (newRecept.MealName != null) recept.MealName = newRecept.MealName;
                if (newRecept.PreperationTime != null) recept.PreperationTime = newRecept.PreperationTime;
                if (newRecept.Price != null) recept.Price = newRecept.Price;
                if (newRecept.Photo != null) recept.Photo = newRecept.Photo;
                if (newRecept.Discription != null) recept.Discription = newRecept.Discription;


                context.Meals.Update(recept);
                context.SaveChanges();
            }
            catch { return NotFound("ilyen recept nemtalálható"); }


            return Ok("A recept módosult.");
        }

        public int UserRole(int id)
        {

            var search = context.Users.FirstOrDefault(x => x.Id == id);
            return search.RoleId;
        }
    }
}
