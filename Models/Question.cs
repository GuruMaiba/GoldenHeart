using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenHeart.Models
{
    // Вопрос
    public class Question
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Имя
        [Required]
        public string Name { get; set; }

        // Почта
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Текст вопроса
        [Required]
        public string Text { get; set; }

        // Дата создания
        public DateTime DateCreate { get; set; }

        // Ответ
        public Reply Reply { get; set; }

        // Врачи скрывшие вопрос
        public virtual ICollection<User> HiddenDocs { get; set; }

        public Question()
        {
            HiddenDocs = new List<User>();
        }
    }

    // Ответ
    public class Reply
    {
        // Идентификатор
        [Key]
        [ForeignKey("Question")]
        public int Id { get; set; }

        // Текст
        [Required]
        public string Text { get; set; }

        // Дата создания
        public DateTime DateCreate { get; set; }

        // Ответивший доктор
        public string NameDoc { get; set; }

        // вопрос
        //public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}