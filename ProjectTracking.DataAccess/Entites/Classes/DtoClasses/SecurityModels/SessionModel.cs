namespace ProjectTracking.DataAccess.Entites.Classes.DtoClasses.SecurityModels;

public class SessionModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Fullname => $"{Name} {Surname}";
}
