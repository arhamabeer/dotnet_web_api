
namespace dotnet_webapi.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetAllEmployees());

            }catch(Exception ex)
            {
                return StatusCode(501, new
                {
                    originalError = ex.Message,
                    errorMessage = "Failed to fetch Data from the Database."
                });
            }
        }
    }
}
