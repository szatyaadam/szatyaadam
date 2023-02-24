namespace CookBook.API.Controllers.DTO
{
    public class IngredientDTO
    {
        public IngredientDTO()
        {

        }
        public IngredientDTO(short quantity, MaterialDTO material)
        {
            Quantity = quantity;
            Material = material;
        }
        public short? Quantity { get; set; }
        public MaterialDTO? Material { get; set; }
    }
}
