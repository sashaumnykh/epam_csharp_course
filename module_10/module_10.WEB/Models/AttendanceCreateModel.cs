using System.ComponentModel.DataAnnotations;

namespace module_10.WEB.Models
{
    public class AttendanceCreateModel
    {
        [Required]
        public int LectureID { get; set; }
        [Required]
        public int StudentID { get; set; }
        public bool? isPresent { get; set; }
        public bool? isHwDone { get; set; }
    }
}
