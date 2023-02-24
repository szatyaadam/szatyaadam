using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("Measure", Name = "Measure", IsUnique = true)]
    [Table("meassures")]
    public partial class Measures
    {
        public Measures()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(20)]
        public string? Measure { get; set;}

        [InverseProperty("UnitOfMeasure")]
        [JsonIgnore]
        public virtual ICollection<Material>? Materials { get; set; }
    }
}
