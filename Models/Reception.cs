using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoldenHeart.Models
{
    // Запись на приём
    public class Reception
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Пациент
        [Required]
        public string PatientName { get; set; }

        // Телефон
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        // Дата приёма
        [Required]
        public DateTime DateReception { get; set; }

        // Доктор
        public virtual User Doc { get; set; }
    }

    // Удаление времени
    public class DeleteTime
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Дата и время удаления
        public DateTime Time { get; set; }

        // У какого врача удалить время
        public virtual User Docs { get; set; }
    }
}