using dotnet_webapi.DataContext;

namespace dotnet_webapi.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = applicationDbContext.AddAsync(employee);
            applicationDbContext.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(int id)
        {
            Employee emp = applicationDbContext.Employees.Find(id);
            if(emp != null)
            {
                applicationDbContext.Employees.Remove(emp); applicationDbContext.SaveChanges();
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await applicationDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            Employee emp =  await applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(emp != null)
            {
                emp.Name = employee.Name;
                emp.City = employee.City;
                applicationDbContext.SaveChanges();
                return emp;
            }
            return null;
        }
        
    }
}
