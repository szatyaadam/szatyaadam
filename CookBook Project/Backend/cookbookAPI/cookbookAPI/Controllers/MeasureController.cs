using CookBook.API.Data;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cookbookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly cookbookContext context = new cookbookContext();

        public MeasureController(cookbookContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]

        public async Task<ActionResult<List<Measures>>> GetAllMeassures()
        {
            return await context.Meassures.ToListAsync();
        }
 
    }
}
