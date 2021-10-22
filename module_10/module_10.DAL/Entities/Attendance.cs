using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Attendance
    {
        public int ID { get; set; }
        [Required]
        public int LectureID { get; set; }
        [Required]
        public int StudentID { get; set; }
        public bool? isPresent { get; set; }
        public bool? isHwDone { get; set; }
    }
}
