using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldenHeart.Models
{
    public class LogDoc
    {
        // Номер телефона
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        // Email
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Пароль
        [Required]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }

    public class CreateDoc
    {

        // Фамилия
        [Required]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        // Имя
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        // Отчество
        [Required]
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        // Образование
        [Required]
        [Display(Name = "Образование:")]
        public string Education { get; set; }

        // Сертификат
        [Required]
        [Display(Name = "Сертификат:")]
        public string Certificate { get; set; }

        // Должность
        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        // С какого числа работает в "Золотом сердце"
        [Required]
        [Display(Name = "Начало деятельности в учреждении:")]
        public string InstitutionWorks { get; set; }

        // Услуга к которой прикрепляется врач
        public int ServiceId { get; set; }

        // Номер телефона
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон:")]
        public string Phone { get; set; }

        // Email
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        // Пароль
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 символов")]
        public string Pass { get; set; }

        // Подтверждение пароля
        [Required]
        [Compare("Pass", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 символов")]
        [Display(Name = "Повторите пароля:")]
        public string PassConfirm { get; set; }

        // Дата рождения
        [Required]
        [Display(Name = "Дата рождения:")]
        public string Birthday { get; set; }

    }

    public class EditDoc
    {
        // id врача
        [Required]
        public int Id { get; set; }

        // Фамилия
        [Required]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        // Имя
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        // Отчество
        [Required]
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        // Образование
        [Required]
        [Display(Name = "Образование:")]
        public string Education { get; set; }

        // Сертификат
        [Required]
        [Display(Name = "Сертификат:")]
        public string Certificate { get; set; }

        // Должность
        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        // С какого числа работает в "Золотом сердце"
        [Required]
        [Display(Name = "Начало деятельности в учреждении:")]
        public string InstitutionWorks { get; set; }

        // Услуга к которой прикрепляется врач
        public int ServiceId { get; set; }

        // Номер телефона
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон:")]
        public string Phone { get; set; }

        // Email
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        // Пароль
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 символов")]
        public string Pass { get; set; }

        // Подтверждение пароля
        [Compare("Pass", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 символов")]
        [Display(Name = "Повторите пароля:")]
        public string PassConfirm { get; set; }

        // Дата рождения
        [Required]
        [Display(Name = "Дата рождения:")]
        public string Birthday { get; set; }

    }
}