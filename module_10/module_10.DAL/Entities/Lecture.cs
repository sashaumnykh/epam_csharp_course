using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Lecture
    {
        public int ID { get; set; }
        public int? LecturerID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
