using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;

public class User : BaseEntity
{
    [DisplayName("Ad")]
    [Required(ErrorMessage = "{0} boş bırakılamaz")]
    [StringLength(50)]
    public string Name { get; set; }
    [DisplayName("Soyad")]
    [Required(ErrorMessage = "{0} boş bırakılamaz")]

    public string Surname { get; set; }
    [DisplayName("Email")]
    [Required(ErrorMessage = "{0} boş bırakılamaz")]
    [EmailAddress(ErrorMessage = "Geçerli bir mail adresi giriniz")]
    public string Email { get; set; }
    [DisplayName("Şifre")]
    [Required(ErrorMessage = "{0} boş bırakılamaz")]

    public string Password { get; set; }
    [DisplayName("Şifre Tekrar")]
    [Required(ErrorMessage = "{0} boş bırakılamaz")]
    [Compare("Password", ErrorMessage = "Şifre eşleşmiyor")]
    [NotMapped]
    public string PasswordAgain { get; set; }
    [NotMapped]
    public string Fullname => $"{Name} {Surname}";
    public ICollection<Requisition> Requisitions { get; set; }
    public ICollection<ProjectNote> ProjectNotes { get; set; }


}
