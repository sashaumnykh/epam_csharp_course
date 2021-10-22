using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Homework
    {
        public int ID { get; set; }
        [Required]
        public int LectureID { get; set; } 
        public int? StudentID { get; set; }
        [Range(0, 5)]
        public int? Mark { get; set; }

    }
}