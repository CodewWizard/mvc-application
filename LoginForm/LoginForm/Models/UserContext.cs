using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BuisnessLayer;

namespace LoginForm.Models
{
    [Table("CodeW-Emp")]
    public class UserContext :DbContext
    {
        public DbSet<User1> Users { get; set; }
        public UserContext() : base("Default")
        {

        }
    }
}