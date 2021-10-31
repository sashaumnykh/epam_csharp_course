using System.ComponentModel.DataAnnotations;

namespace module_10.WEB.Models
{
    public class HomeworkCreateModel
    {
        [Required]
        public int LectureID { get; set; }
        public int? StudentID { get; set; }
        public int? Mark { get; set; }
    }
}