using System.ComponentModel.DataAnnotations;

namespace AbsenceManager.Models
{
	public class LeaveRequest
	{
		public int Id { get; set; }

		// Foreign Key to Employee
		public int EmployeeId { get; set; } // Reference to Employee's Id

		[Required]
		public string LeaveType { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public string Reason { get; set; }

		public string DocumentPath { get; set; }

		public bool IsApproved { get; set; }

		// Navigation property to Employee
		public Employee Employee { get; set; }
	}
}
