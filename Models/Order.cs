using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FirstSiteShopWithMvc.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name = "Введіть ваше Ім'я")]
        [Required(ErrorMessage = "Ім'я обов'язкове для заповнення")]
        [StringLength(30, ErrorMessage = "Не більше 30 символів")]

        public string Name { get; set; }

        [Display(Name = "Введіть ваше Прізвище")]
        [Required(ErrorMessage = "Прізвище обов'язкове для заповнення")]
        [StringLength(50, ErrorMessage = "Не більше 50 символів")]

        public string Surname { get; set; }

        [Display(Name = "Введіть вашу Адресу")]
        [Required(ErrorMessage = "Адреса обов'язкова для заповнення")]
        [StringLength(100, ErrorMessage = "Не більше 100 символів")]

        public string Address { get; set; }

        [Display(Name = "Введіть ваш Номер телефону")]
        [Required(ErrorMessage = "Номер телефону обов'язковий для заповнення")]
        [StringLength(10, ErrorMessage = "Не більше 10 символів")]
        [Phone(ErrorMessage ="Невірний формат номеру телефону")]
        [DataType(DataType.PhoneNumber)]

        public string Phone { get; set; }

        [Display(Name = "Введіть вашу Електрону пошту")]
        [Required(ErrorMessage = "Електрона пошта обов'язкова для заповнення")]
        [StringLength(100, ErrorMessage = "Не більше 100 символів")]
        [EmailAddress(ErrorMessage ="Невірний формат Електроної пошти")]

        public string Email { get; set; }


        [Required(ErrorMessage = "Ваша корзина пуста")]
        public string Cart { get; set; }

    }
}
