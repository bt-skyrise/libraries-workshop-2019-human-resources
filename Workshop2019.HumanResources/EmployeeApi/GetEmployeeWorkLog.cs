using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Workshop2019.HumanResources.EmployeeApi
{
    public class GetEmployeeWorkLog
    {
        private readonly EmployeeApiHttpClient _httpClient;

        public GetEmployeeWorkLog(EmployeeApiHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmployeeWorkLogItemDto[]> Get(int employeeId, DateTime from, DateTime to)
        {
            var response = await _httpClient.Get<GetEmployeeWorkLogResponse>($"api/employees/{employeeId}/work-log", new Dictionary<string, string>
            {
                { "from", FormatDate(from) },
                { "to", FormatDate(to) }
            });

            return response.WorkLogItems;
        }

        private static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }

    public class GetEmployeeWorkLogResponse
    {
        public EmployeeWorkLogItemDto[] WorkLogItems { get; set; }
    }

    public class EmployeeWorkLogItemDto
    {
        public DateTime Date { get; set; }
        public decimal WorkedHours { get; set; }
    }
}