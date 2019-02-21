using System.Linq;
using System.Threading.Tasks;

namespace Workshop2019.HumanResources.EmployeeApi
{
    public class GetEmployees
    {
        private readonly EmployeeApiHttpClient _httpClient;

        public GetEmployees(EmployeeApiHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetEmployeeDto[]> Get()
        {
            var response = await _httpClient.Get<GetEmployeesResponse>("api/employees");

            return response.Employees;
        }
    }

    public class GetEmployeesResponse
    {
        public GetEmployeeDto[] Employees { get; set; }
    }

    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string GetInitials()
        {
            return $"{FirstName.First()}{Surname.First()}";
        }
    }
}