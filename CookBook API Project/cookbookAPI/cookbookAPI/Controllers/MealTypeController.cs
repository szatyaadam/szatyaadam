using CookBook.API.Data;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        private readonly cookbookContext context = new cookbookContext();

        public MealTypeController(cookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get back all the mealtype
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<List<Mealtype>>>GetAllMealType()
        {
 
            var mealtype = await context.Mealtypes.Where(x =>x.MealTypeName!=null).ToListAsync();
            return mealtype;
        }
    }
}
