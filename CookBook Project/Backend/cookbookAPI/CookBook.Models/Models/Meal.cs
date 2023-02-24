using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("MealTypeId", Name = "MealTypeId")]
    [Index("UserId", Name = "UserId")]
    [Table("meals")]
    public partial class Meal
    {
        public Meal()
        {
            Ingredients = new HashSet<Ingredient>();
        }
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }

        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }

        [StringLength(100)]
        public string MealName { get; set; } = null!;

        [Column(TypeName = "time")]
        public TimeSpan? PreperationTime { get; set; }

        public float? Price { get; set; }

        [StringLength(160)]
        public string? Photo { get; set; }

        [Column(TypeName = "int(11)")]
        public int MealTypeId { get; set; }

        [Column(TypeName = "text")]
        public string? Discription { get; set; }

        [Column(TypeName = "tinyint(4)")]
        public sbyte Privacy { get; set; }

        [ForeignKey("MealTypeId")]
        [InverseProperty("Meals")]
        public virtual Mealtype? MealType { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Meals")]
        [JsonIgnore]
        public virtual User? User { get; set; }
        [InverseProperty("Meals")]
        public virtual ICollection<Ingredient> Ingredients { get; set; } = null!;
    }
}
