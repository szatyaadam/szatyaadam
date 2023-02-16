using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBook.Models.Models
{
    [Index("Email", Name = "Email", IsUnique = true)]
    [Index("UserName", Name = "UserName", IsUnique = true)]
    [Index("RoleId", Name = "users_ibfk_1")]
    [Table("users")]
    public partial class User
    {
        public User()
        {
            Favorites = new HashSet<Favorite>();
            Meals = new HashSet<Meal>();
            Token = new HashSet<Token>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(60)]
        public string? Password { get; set; }
        [Column(TypeName = "int(11)")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("User")]
        public virtual Role? Role { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Favorite>? Favorites { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Meal>? Meals { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Token>? Token { get; set; }
    }
}
