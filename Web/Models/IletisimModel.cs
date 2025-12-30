using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class IletisimModel
    {
        [Required]
        public string Ad_Soyad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^05[0-9]{9}$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Telefon { get; set; }

        public string Fotoğraf { get; set; }

        [Required]
        public string Konu { get; set; }

        [Required]
        public string Mesaj { get; set; }
    }
}
