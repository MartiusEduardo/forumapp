
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models{
    [Table("topic")]
    public class Topic{
        [Key, Column("idtopic")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTopic { get; set; }
        [Column("idcategory")]
        public int idCategory { get; set; }
        [Column("iduser")]
        public string idUser { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("dateregister")]
        public DateTime DateRegister { get; set; }
    }
}