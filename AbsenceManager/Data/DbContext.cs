using AbsenceManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AbsenceManager.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Employee> Employees { get; set; }
		public DbSet<LeaveRequest> LeaveRequests { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Seed Roles with static GUIDs
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = "11111111-1111-1111-1111-111111111111", Name = "Admin", NormalizedName = "ADMIN" },
				new IdentityRole { Id = "22222222-2222-2222-2222-222222222222", Name = "User", NormalizedName = "USER" }
			);
		}
	}
}
