using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models.Models
{
    [Index("UserId", Name = "UserId")]
    [Table("tokens")]
    public partial class Token
    {
        public Token(string token, int userId)
        {
            this.token = token;
            Exp_date = DateTime.Now.AddDays(7);
            UserId = userId;
        }


        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(255)]
        public string token { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Exp_date { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Token")]
        public virtual User User { get; set; } = null!;
    }
}
