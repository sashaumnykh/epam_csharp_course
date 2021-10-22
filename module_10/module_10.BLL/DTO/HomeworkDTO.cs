using System.ComponentModel.DataAnnotations;

namespace module_10.BLL.DTO
{
    public class HomeworkDTO
    {
        public int ID { get; set; }
        [Required]
        public int LectureID { get; set; }
        public int? StudentID { get; set; }
        [Range(0, 5)]
        public int? Mark { get; set; }
    }
}