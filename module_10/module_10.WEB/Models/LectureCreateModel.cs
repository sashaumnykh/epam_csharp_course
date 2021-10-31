using System.ComponentModel.DataAnnotations;

namespace module_10.WEB.Models
{
    public class LectureCreateModel
    {
        public int? LecturerID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}