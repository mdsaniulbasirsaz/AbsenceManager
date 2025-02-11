using System.Diagnostics;
using AbsenceManager.Models;
using AbsenceManager.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = AbsenceManager.DTOs.LoginRequest;
using AbsenceManager.Data;
using Microsoft.EntityFrameworkCore;

namespace AbsenceManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Apply()
        {
            var employeeId = HttpContext.Session.GetInt32("EmployeeId");



            if(!employeeId.HasValue)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

		[HttpPost]
		public IActionResult Apply(LeaveRequest leaveRequest, IFormFile? file)
		{
			var employeeId = HttpContext.Session.GetInt32("EmployeeId");

			if (!employeeId.HasValue)
			{
				return RedirectToAction("Index", "Home");
			}

			leaveRequest.EmployeeId = employeeId.Value;
			leaveRequest.IsApproved = false;

			// Convert StartDate and EndDate to UTC before saving
			leaveRequest.StartDate = leaveRequest.StartDate.ToUniversalTime();
			leaveRequest.EndDate = leaveRequest.EndDate.ToUniversalTime();

			// Handle file upload (Convert to Base64)
			if (file != null && file.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					file.CopyTo(ms);
					byte[] fileBytes = ms.ToArray();
					leaveRequest.DocumentPath = Convert.ToBase64String(fileBytes);
				}
			}
			else
			{
				leaveRequest.DocumentPath = null; // No file uploaded
			}

			if (ModelState.IsValid)
			{
				_applicationDbContext.LeaveRequests.Add(leaveRequest);
				_applicationDbContext.SaveChanges();
				return RedirectToAction("Profile");
			}

			// Log errors for debugging
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			foreach (var error in errors)
			{
				Console.WriteLine(error.ErrorMessage); // Debugging purpose
			}

			return View(leaveRequest);
		}



		[HttpGet]
		public IActionResult ViewProof(int id)
		{
			// Fetch the leave request by its ID
			var leaveRequest = _applicationDbContext.LeaveRequests
				.FirstOrDefault(lr => lr.Id == id);

			if (leaveRequest == null || string.IsNullOrEmpty(leaveRequest.DocumentPath))
			{
				return NotFound("Proof document not found.");
			}

			// Return the document as a file (it could be an image, PDF, etc.)
			byte[] fileBytes = Convert.FromBase64String(leaveRequest.DocumentPath);
			string fileType = "application/octet-stream"; // Default type

			if (leaveRequest.DocumentPath.StartsWith("/9j/"))
				fileType = "image/jpeg";  // If it's a JPEG image
			else if (leaveRequest.DocumentPath.StartsWith("iVBOR"))
				fileType = "image/png";   // If it's a PNG image
			else if (leaveRequest.DocumentPath.StartsWith("JVBER"))
				fileType = "application/pdf"; // If it's a PDF

			return File(fileBytes, fileType);
		}




		public IActionResult Dashboard()
		{
			var employeeRole = HttpContext.Session.GetString("EmployeeRole");

			if (employeeRole != "ADMIN")
			{
				return RedirectToAction("Login", "Home");
			}

			// Retrieve all leave requests from the database
			var leaveRequests = _applicationDbContext.LeaveRequests
				.Include(lr => lr.Employee)  // Include the Employee details
				.OrderByDescending(lr => lr.StartDate) // Optional, to sort by start date
				.ToList();

			// Pass the leave requests to the view
			return View(leaveRequests);
		}

		public IActionResult Add()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(string.IsNullOrEmpty(loginRequest.PhoneNumber) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return View("Index");
            }

            var employee = await _applicationDbContext.Employees
                .Where(e => e.PhoneNumber == loginRequest.PhoneNumber && e.Password == loginRequest.Password)
                .FirstOrDefaultAsync();


            if(employee == null)
            {
                ViewData["ErrorMessage"] = "Invalid credentials";
                return View("Index");
            }

			// Store employee details in Session
			HttpContext.Session.SetInt32("EmployeeId", employee.Id);
			HttpContext.Session.SetString("EmployeeFullName", employee.FullName);
			HttpContext.Session.SetString("EmployeeDepartment", employee.Department);
			HttpContext.Session.SetString("EmployeePhoneNumber", employee.PhoneNumber);
			HttpContext.Session.SetString("EmployeeRole", employee.Role);


			if(employee.Role == "ADMIN")
            {
				return RedirectToAction("Dashboard","Home");
			} 
            else { 
                return RedirectToAction("Profile"); 
            }
			
		}

		public IActionResult Profile()
		{
			// Retrieve employee details from Session or database
			var employeeId = HttpContext.Session.GetInt32("EmployeeId");
			var employeeFullName = HttpContext.Session.GetString("EmployeeFullName");
			var employeeDepartment = HttpContext.Session.GetString("EmployeeDepartment");
			var employeePhoneNumber = HttpContext.Session.GetString("EmployeePhoneNumber");
			var employeeRole = HttpContext.Session.GetString("EmployeeRole");

			if (!employeeId.HasValue || string.IsNullOrEmpty(employeeFullName))
			{
				return RedirectToAction("Index");
			}

			// Create Employee model
			var employeeProfile = new Employee
			{
				Id = employeeId.Value,
				FullName = employeeFullName,
				Department = employeeDepartment,
				PhoneNumber = employeePhoneNumber,
				Role = employeeRole
			};

			// Fetch Leave Request History for the employee
			var leaveRequests = _applicationDbContext.LeaveRequests
				.Where(lr => lr.EmployeeId == employeeId.Value)
				.OrderByDescending(lr => lr.StartDate)
				.ToList();

			// Pass both employeeProfile and leaveRequests to the view
			var model = new EmployeeProfileViewModel
			{
				Employee = employeeProfile,
				LeaveRequests = leaveRequests
			};

			return View(model);
		}



		public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
