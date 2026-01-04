using Application.Helpers;
using Domain;
using Domain.Enum;

namespace Application.Services
{
    public class HolidayService
    {
        public List<Holiday> GetHolidaysByName(string? name, Locations location, int year)
        {
            var holidays = HolidayHelper.GetHolidays(year, location);

            var filteredHolidays = holidays
                .Where(holiday => holiday.Name.Contains(name ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var uniqueResults = filteredHolidays
                .GroupBy(h => (h.Name + "|" + h.Location).ToLowerInvariant())
                .Select(g => g.First())
                .ToList();

            return uniqueResults;
        }
    }
}
