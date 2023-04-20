using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("MealId", Name = "MealId")]
    [Index("MaterialId", Name = "ingredients_ibfk_2")]
    [Table("ingredients")]
    public partial class Ingredient
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int MealId { get; set; }
        [Column(TypeName = "smallint(11)")]
        public short? Quantity { get; set; }
        [Column("MaterialsId", TypeName = "int(11)")]
        public int? MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        [InverseProperty("Ingredients")]
        public virtual Material? Materials { get; set; }
        [ForeignKey("MealId")]
        [JsonIgnore]
        [InverseProperty("Ingredients")]
        public virtual Meal? Meals { get; set; }
    }
}
