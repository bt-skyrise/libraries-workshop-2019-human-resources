using System;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop2019.HumanResources.EmployeeApi
{
    public class GetEmployeeOvertime
    {
        private readonly GetEmployeeWorkLog _getEmployeeWorkLog;

        public GetEmployeeOvertime(GetEmployeeWorkLog getEmployeeWorkLog)
        {
            _getEmployeeWorkLog = getEmployeeWorkLog;
        }

        public async Task<decimal> Get(int employeeId, DateTime from, DateTime to)
        {
            var workLogItems = await _getEmployeeWorkLog.Get(employeeId, from, to);

            return workLogItems
                .Select(GetOvertime)
                .Sum();
        }

        private decimal GetOvertime(EmployeeWorkLogItemDto workLogItem)
        {
            var defaultWorkingHours = 8;

            return IsWeekend(workLogItem.Date)
                ? workLogItem.WorkedHours
                : workLogItem.WorkedHours - defaultWorkingHours;
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}