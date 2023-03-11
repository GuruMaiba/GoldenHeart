using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoldenHeart.Models
{
    // Услуга
    public class Service
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Название услуги
        public string Name { get; set; }

        // Доктора
        public virtual ICollection<User> Docs { get; set; }
    }
}