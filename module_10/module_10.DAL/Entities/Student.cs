using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        //[Phone]
        public string PhoneNumber { get; set; }
    }
}