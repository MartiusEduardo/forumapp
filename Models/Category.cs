using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models{
    [Table("category")]
    public class Category{
        [Key, Column("idcategory")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategory { get; set; }
        [Column("idgroup")]
        public int idGroup { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}