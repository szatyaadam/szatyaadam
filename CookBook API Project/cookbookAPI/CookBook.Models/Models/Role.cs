using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models.Models
{
    [Table("roles")]
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; } = null!;

        [InverseProperty("Role")]
        public virtual ICollection<User>? User { get; set; }
    }
}
