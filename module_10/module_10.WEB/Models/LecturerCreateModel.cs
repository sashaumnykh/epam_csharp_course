﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace module_10.WEB.Models
{
    public class LecturerCreateModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}