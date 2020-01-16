using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; } = DateTime.Now;
        //public int TypeOfUserID { get; set; }
        public virtual UsersType UsersType { get; set; }

    }
    [Table("UsersTypes", Schema = "dbo")]
    public class UsersType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}