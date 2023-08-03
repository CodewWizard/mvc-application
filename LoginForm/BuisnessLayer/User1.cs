using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    [Table("CodeW-Emp")]
    public class User1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        [Required]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage = "Username is Requred")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Requred")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Select the gender")]
        public string Gender { get; set; }
        [Required]

        public string UserType { get; set; }
    }
}
