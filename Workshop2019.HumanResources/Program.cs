using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Workshop2019.HumanResources.EmployeeApi;

namespace Workshop2019.HumanResources
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var employeeApi = new EmployeeApiHttpClient(
                apiUrl: new Uri(configuration["EmployeeApiUrl"]),
                apiKey: configuration["EmployeeApiKey"]
            );

            var getEmployees = new GetEmployees(employeeApi);
            var getEmployeeWorkLog = new GetEmployeeWorkLog(employeeApi);
            var getEmployeeOvertime = new GetEmployeeOvertime(getEmployeeWorkLog);

            Console.WriteLine("HR overtime report:");

            foreach (var employee in await getEmployees.Get())
            {
                var employeeOvertime = await getEmployeeOvertime.Get(
                    employeeId: employee.Id,
                    from: new DateTime(2019, 2, 1),
                    to: new DateTime(2019, 2, 28)
                );

                Console.WriteLine($"    {employee.GetInitials()}, total overtime: {employeeOvertime}");
            }

            Console.ReadKey();
        }
    }
}
