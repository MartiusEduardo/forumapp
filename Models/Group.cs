using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models{
    [Table("group")]
    public class Group{
        [Key, Column("idgroup")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idGroup { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}