using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoldenHeart.Models
{
    public class User
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Фамилия
        [Required]
        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        // Имя
        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }

        // Отчество
        [Required]
        [DisplayName("Отчество")]
        public string LastName { get; set; }

        // Аватар
        public string Avatar { get; set; }

        // Образование
        [Required]
        [DisplayName("Образование")]
        public string Education { get; set; }

        // Сертификат
        [Required]
        [DisplayName("Сертификат")]
        public string Certificate { get; set; }

        // Тип врача
        [Required]
        [DisplayName("Должность")]
        public string Position { get; set; }

        // С какого числа работает в "Золотом сердце"
        [Required]
        [DisplayName("Начало деятельности в учреждении")]
        public string InstitutionWorks { get; set; }

        // Номер телефона
        [Required]
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        // Email
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        // Показ почты
        [DefaultValue(true)]
        public bool VisibleEmail { get; set; }

        // Показ телефона
        [DefaultValue(true)]
        public bool VisiblePhone { get; set; }

        // Начало Отпуска
        public DateTime BeginLeave { get; set; }

        // Конец Отпуска
        public DateTime EndLeave { get; set; }

        // Больничный
        [DefaultValue(false)]
        public bool Hospital { get; set; }

        // Хэш-код пароля
        public string HashPassword { get; set; }

        // Дата рождения
        [Required]
        [DisplayName("Дата рождения")]
        public string Birthday { get; set; }

        // Роль
        public virtual Role Role { get; set; }

        // Услуга
        public virtual Service Service { get; set; }

        // Скрытые вопросы
        public virtual ICollection<Question> HiddenQuestions { get; set; }

        // Записи
        public virtual ICollection<Reception> DocReceptions { get; set; }

        // Ответы
        public virtual ICollection<Reply> Replies { get; set; }

        // Удаление времени
        public virtual ICollection<DeleteTime> DeleteTime { get; set; }

        // Сообщения для персонала
        public virtual ICollection<MessageStaff> Messages { get; set; }

        public User()
        {
            HiddenQuestions = new List<Question>();
            Messages = new List<MessageStaff>();
        }
    }

    public class Role
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Имя
        [Required]
        public string Name { get; set; }

        // Коллекция пользователей
        public virtual ICollection<User> Docs { get; set; }
    }
}