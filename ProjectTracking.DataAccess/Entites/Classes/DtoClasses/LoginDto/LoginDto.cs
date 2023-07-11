using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataAccess.Entites.Classes.DtoClasses.LoginDto;

public class LoginDto
{
    [DisplayName("Mail")]
    [Required(ErrorMessage = "{0} doldurulması zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir mail adresi giriniz")]
    public string Mail { get; set; }
    [DisplayName("Parola")]
    [Required(ErrorMessage = "{0} doldurulması zorunludur")]
    public string Password { get; set; }
}
