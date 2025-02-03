using AbsenceManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AbsenceManager.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		// DbSet properties
		public DbSet<Employee> Employees { get; set; }
		public DbSet<LeaveRequest> LeaveRequests { get; set; }
	}
}
