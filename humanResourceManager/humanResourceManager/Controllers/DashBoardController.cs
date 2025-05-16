using humanResourceManager.Datas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashBoardController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        public DashBoardController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("overview")]
        public IActionResult GetDashboardOverview()
        {
            var result = new
            {
                TotalEmployees = _dbContext.Employees.Count(),
                TotalDepartments = _dbContext.Departments.Count(),
                TotalContracts = _dbContext.Contracts.Count(),
                TotalPositions = _dbContext.Positions.Count(),
                TotalAttendance = _dbContext.Attendance.Count(),
                TotalPayrolls = _dbContext.Payroll.Count()
            };
            return Ok(result);
        }

    }
}
