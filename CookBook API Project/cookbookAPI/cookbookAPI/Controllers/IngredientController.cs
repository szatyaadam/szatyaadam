using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CookBook.API.Data;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using CookBook.API.Controllers.DTO;
using CookBook.API.Services;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly cookbookContext context;

        public IngredientController(cookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Modify a material request.
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("Modify")]
        public async Task<IActionResult> PutIngredient(Ingredient ingredient)
        {
            try
            {
                NewMaterial(ingredient);
                context.Ingredients.Update(ingredient);
                context.SaveChanges();

                return Ok("Frissült a megadott adat.");
            }
            catch { return BadRequest("Hiba történt"); }
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<IngredientDTO>> PostIngredient(Ingredient ingredient)
        {
            Ingredient? ingr = await context.Ingredients.
                Include(x => x.Materials).
                FirstOrDefaultAsync(x => 
                x.Materials.IngredientName ==ingredient.Materials.IngredientName&&
                x.Materials.UnitOfMeasureId==ingredient.Materials.UnitOfMeasureId);
            if (ingr!=null)
            {
                if (ingr.Quantity!=ingredient.Quantity)
                {
                    ingr.Quantity = ingredient.Quantity;
                    context.Ingredients.Update(ingr);
                    context.SaveChanges();
                    return Ok("Csak Változtatás történt.");
                }
                return BadRequest("Ilyen ingredients létezik.");
            }

            Meal? meal = await context.Meals.Include(x=>x.Ingredients).FirstOrDefaultAsync(x => x.Id == ingredient.MealId);
            if (meal == null)
            {
                return BadRequest("Ilyen recept nemtalálható.");
            }
           
                 //Ezt kell kivenni ha többször megakarjuk jeleníteni ugyan azt a hozzávalót pl szósz esetén.
            var change = context.Ingredients.Include(x => x.Materials)
                      .FirstOrDefault(x => x.MealId == ingredient.MealId &&
                      x.Materials.IngredientName == ingredient.Materials.IngredientName);
            if (change != null)
            {
                    context.Ingredients.Remove(change);
            }
                     //idáig
            else
            {
                NewMaterial(ingredient);
            }
            context.Ingredients.Add(ingredient);
            context.SaveChanges();

            return Ok(ingredient.ToIngredientDTO());      
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {

            var ingredient = context.Ingredients.Include(x => x.Meals).FirstOrDefault(x => x.Id == id);
            User? user = await context.Users.FirstOrDefaultAsync(x => x.Id == UserService.GetUserId(User));
            if (user.Id != ingredient.Meals.UserId && user.RoleId != 1)
            {
                return BadRequest("Ön nem jogosult a hozzávaló törlésére.");
            }
            if (ingredient == null)
            {
                return NotFound("Ilyen hozzávaló nem található");
            }

            context.Ingredients.Remove(ingredient);
            context.SaveChanges();

            return Ok("A hozzávaló törölve lett");
        }

        public Material NewMaterial(Ingredient ingredient)
        {
            var material = context.Materials.FirstOrDefault
               (x => x.IngredientName == ingredient.Materials.IngredientName &&
                x.UnitOfMeasureId == ingredient.Materials.UnitOfMeasureId);

            if (material != null)
            {

                return ingredient.Materials = material;
            }
            Material newMaterial = new Material();
            newMaterial.IngredientName = ingredient.Materials.IngredientName;
            newMaterial.UnitOfMeasureId = ingredient.Materials.UnitOfMeasureId;

            return newMaterial;
        }
    }
    
}
