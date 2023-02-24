using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("MealsId", Name = "MealsId")]
    [Index("UserId", Name = "UserId")]
    [Table("favorites")]
    public partial class Favorite
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "int(11)")]
        [JsonIgnore]
        public int UserId { get; set; }
        [Column(TypeName = "int(11)")]
        public int MealsId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Favorites")]
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
