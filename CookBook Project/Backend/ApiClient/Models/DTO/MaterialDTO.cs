using CookBook.Models.Models;
namespace CookBook.API.Controllers.DTO
{
    public class MaterialDTO
    {
        public MaterialDTO()
        {
        }
        public MaterialDTO(int id, Measures unitOfMeasure, string ingredientName)
        {
            Id = id;
            UnitOfMeasure = unitOfMeasure;
            IngredientName = ingredientName;
        }
        public int Id { get; set; }
        public Measures UnitOfMeasure { get; set; } = null!;
        public string? IngredientName { get; set; }
    }
}
