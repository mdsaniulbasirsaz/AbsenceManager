using System.ComponentModel.DataAnnotations;

namespace AbsenceManager.Models
{
	public class Employee
	{
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		[StringLength(100)]
		public string Password { get; set; }

		[Required]
		public string Department { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }

		public string Role { get; set; } = "USER";
	}
}
