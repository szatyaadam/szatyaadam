namespace CookBook.API.Controllers.DTO
{
    public class IngredientDTO
    {
        public IngredientDTO()
        {

        }
        public IngredientDTO(int id ,short quantity, MaterialDTO materials)
        {
            Id = id;
            Quantity = quantity;
            Materials = materials;
        }
        public int? Id { get; set; }
        public short? Quantity { get; set; }
        public MaterialDTO? Materials { get; set; }
    }
}
