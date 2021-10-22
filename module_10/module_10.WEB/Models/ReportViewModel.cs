using System.ComponentModel.DataAnnotations;

namespace module_10.BLL.DTO
{
    public class ReportViewModel
    {
        [Required]
        [StringLength(50)]
        public string Format { get; set; }
        [StringLength(50)]
        public string studentName { get; set; }
        [StringLength(50)]
        public string lectureName { get; set; }
    }
}