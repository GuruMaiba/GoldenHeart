using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoldenHeart.Models
{
    public class MessageStaff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public DateTime DateCreate { get; set; }

        [DefaultValue(null)]
        public string Type { get; set; }

        public ICollection<User> Docs { get; set; }

        public MessageStaff()
        {
            Docs = new List<User>();
        }
    }
}