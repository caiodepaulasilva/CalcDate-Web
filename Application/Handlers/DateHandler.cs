using Application.Commands;
using Application.Helpers;
using Application.Models;
using Mediator;

namespace Application.Handlers
{
    public class DateHandler :
        IRequestHandler<GetDiffBetweenDatesCommand, DiffBetweenDatesResult>,
        IRequestHandler<CountDaysOfWeekCommand, CountDaysOfWeekResult>        
    {
        public ValueTask<DiffBetweenDatesResult> Handle(GetDiffBetweenDatesCommand request, CancellationToken cancellationToken)
        {
            (request.StartDate, request.EndDate) = DateHelper.OrderDates(request.StartDate, request.EndDate);

            int days = request.EndDate.Day - request.StartDate.Day;
            int months = request.EndDate.Month - request.StartDate.Month;
            int years = request.EndDate.Year - request.StartDate.Year;

            if (days < 0)
            {
                months--;
                var prevMonth = request.EndDate.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            var result = new DiffBetweenDatesResult { Years = years, Months = months, Days = days };
            return new ValueTask<DiffBetweenDatesResult>(result);
        }

        public ValueTask<CountDaysOfWeekResult> Handle(CountDaysOfWeekCommand request, CancellationToken cancellationToken)
        {
            (request.StartDate, request.EndDate) = DateHelper.OrderDates(request.StartDate, request.EndDate);

            int totalDays = request.EndDate.DayNumber - request.StartDate.DayNumber + 1;

            var result = new CountDaysOfWeekResult();

            for (int i = 0; i < totalDays; i++)
            {
                var day = request.StartDate.AddDays(i);

                switch (day.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        result.Monday++;
                        break;
                    case DayOfWeek.Tuesday:
                        result.Tuesday++;
                        break;
                    case DayOfWeek.Wednesday:
                        result.Wednesday++;
                        break;
                    case DayOfWeek.Thursday:
                        result.Thursday++;
                        break;
                    case DayOfWeek.Friday:
                        result.Friday++;
                        break;
                    case DayOfWeek.Saturday:
                        result.Saturday++;
                        break;
                    case DayOfWeek.Sunday:
                        result.Sunday++;
                        break;
                }
            }

            return new ValueTask<CountDaysOfWeekResult>(result);
        }      
    }
}