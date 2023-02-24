using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("IngredientName", Name = "IngredientName")]
    [Index("UnitOfMeasureId", Name = "UnitOfMeassuredId")]
    [Table("materials")]
    public partial class Material
    {
        public Material()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(50)]
        public string IngredientName { get; set; }
        [Column("UnitOfMeassuredId", TypeName = "int(11)")]
        public int UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        [InverseProperty("Materials")]
        public virtual Measures? UnitOfMeasure { get; set; }
        [InverseProperty("Materials")]
        [JsonIgnore]
        public virtual ICollection<Ingredient>? Ingredients { get; set; }
    }
}
