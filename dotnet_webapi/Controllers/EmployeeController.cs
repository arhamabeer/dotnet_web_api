
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

        [HttpGet("{id=int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);

                if(result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(501, new
                {
                    originalError = ex.Message,
                    errorMessage = "Failed to fetch Data from the Database."
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            try
            {
                var newEmployee = await _employeeRepository.AddEmployee(employee);
                return Ok(newEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(501, new
                {
                    originalError = ex.Message,
                    errorMessage = "Failed to save data to the Database."
                });
            }
        }
        [HttpPut("{id=int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
               if(id != employee.Id)
                {
                    return BadRequest("Id Mismatched!");
                }
                
                var fetchedEmployee = await _employeeRepository.GetEmployee(id);
                if(fetchedEmployee == null)
                {
                    return NotFound($"Cannot find any Employee with id {id}");
                }

                var result = await _employeeRepository.UpdateEmployee(employee);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(501, new
                {
                    originalError = ex.Message,
                    errorMessage = "Failed to update data from the Database."
                });
            }
        }
        [HttpDelete("{id=int}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var fetchedEmployee = await _employeeRepository.GetEmployee(id);
                if (fetchedEmployee == null)
                {
                    return NotFound($"Cannot find any Employee with id {id}");
                }
                 _employeeRepository.DeleteEmployee(id);
                return Ok("Employee Deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(501, new
                {
                    originalError = ex.Message,
                    errorMessage = "Failed to update data from the Database."
                });
            }
        }
    }
}
