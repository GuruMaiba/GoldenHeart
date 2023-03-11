using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldenHeart.Models
{
    public class ReceptionUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public int Mounth { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public int DocId { get; set; }

        [Required]
        public string Birthday { get; set; }
    }
}