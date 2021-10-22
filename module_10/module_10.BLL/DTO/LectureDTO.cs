using System.ComponentModel.DataAnnotations;

namespace module_10.BLL.DTO
{
    public class LectureDTO
    {
        public int ID { get; set; }
        public int? LecturerID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
