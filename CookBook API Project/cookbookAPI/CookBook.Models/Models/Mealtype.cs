using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("MealTypeName", Name = "MealTypeName", IsUnique = true)]
    [Table("mealtypes")]
    public partial class Mealtype
    {
        public Mealtype()
        {
            Meals = new HashSet<Meal>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? MealTypeName { get; set; }
        [JsonIgnore]
        [InverseProperty("MealType")]
        public virtual ICollection<Meal>? Meals { get; set; }
    }
}
