using Application.Helpers;
using Application.Models;

namespace Application.Services
{
    public class DateService
    {
        public DiffBetweenDatesResult GetDiffBetweenDates(DateOnly startDate, DateOnly endDate)
        {
            (startDate, endDate) = DateHelper.OrderDates(startDate, endDate);

            int days = endDate.Day - startDate.Day;
            int months = endDate.Month - startDate.Month;
            int years = endDate.Year - startDate.Year;

            if (days < 0)
            {
                months--;
                var prevMonth = endDate.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return new DiffBetweenDatesResult { Years = years, Months = months, Days = days };
        }

        public CountDaysOfWeekResult CountDaysOfWeek(DateOnly startDate, DateOnly endDate)
        {
            (startDate, endDate) = DateHelper.OrderDates(startDate, endDate);

            int totalDays = endDate.DayNumber - startDate.DayNumber + 1;

            int fullWeeks = totalDays / 7;
            int remainingDays = totalDays % 7;

            var result = new CountDaysOfWeekResult
            {
                Monday = fullWeeks,
                Tuesday = fullWeeks,
                Wednesday = fullWeeks,
                Thursday = fullWeeks,
                Friday = fullWeeks,
                Saturday = fullWeeks,
                Sunday = fullWeeks
            };

            int startDayIndex = (int)startDate.DayOfWeek;

            for (int i = 0; i < remainingDays; i++)
            {
                int dayIndex = (startDayIndex + i) % 7;

                switch (dayIndex)
                {
                    case 0: result.Sunday++; break;
                    case 1: result.Monday++; break;
                    case 2: result.Tuesday++; break;
                    case 3: result.Wednesday++; break;
                    case 4: result.Thursday++; break;
                    case 5: result.Friday++; break;
                    case 6: result.Saturday++; break;
                }
            }

            return result;
        }

        public int CountBusinessDays(DateOnly startDate, DateOnly endDate)
        {
            (startDate, endDate) = DateHelper.OrderDates(startDate, endDate);

            int totalDays = endDate.DayNumber - startDate.DayNumber + 1;
            int businessDays = 0;

            for (int i = 0; i < totalDays; i++)
            {
                var day = startDate.AddDays(i);
                if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays++;
                }
            }

            return businessDays;
        }
    }
}
