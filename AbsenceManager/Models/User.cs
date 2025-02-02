using Microsoft.AspNetCore.Identity;

namespace AbsenceManager.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public string Department { get; set; }

	}
}
