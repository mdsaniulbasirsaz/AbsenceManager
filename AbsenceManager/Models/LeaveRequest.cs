namespace AbsenceManager.Models
{
	public class LeaveRequest
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string LeaveType { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public string Reason { get; set; }

		public string DocumentPath { get; set; }

		public bool IsApproved { get; set; }
	}
}
