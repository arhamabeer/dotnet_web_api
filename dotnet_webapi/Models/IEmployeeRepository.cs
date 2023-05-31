namespace dotnet_webapi.Models
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployee(int id);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> AddEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
