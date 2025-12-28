using Application.Commands;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Handlers
{
    public class DayCalcHandler :
        IRequestHandler<GetDiffBetweenDatesCommand, DiffBetweenDatesResult>,
        IRequestHandler<CountDaysOfWeekCommand, CountDaysOfWeekResult>,
        IRequestHandler<CountBusinessDaysCommand, int>
    {
        public Task<DiffBetweenDatesResult> Handle(GetDiffBetweenDatesCommand request, CancellationToken cancellationToken)
        {
            (request.StartDate, request.EndDate) = DateHelper.OrderDates(request.StartDate, request.EndDate);

            int days = request.EndDate.Day - request.StartDate.Day;
            int months = request.EndDate.Month - request.StartDate.Month;
            int years = request.EndDate.Year - request.StartDate.Year;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(request.EndDate.Year, request.StartDate.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            var result = new DiffBetweenDatesResult { Years = years, Months = months, Days = days };
            return Task.FromResult(result);
        }

        public Task<CountDaysOfWeekResult> Handle(CountDaysOfWeekCommand request, CancellationToken cancellationToken)
        {
            (request.StartDate, request.EndDate) = DateHelper.OrderDates(request.StartDate, request.EndDate);

            TimeSpan span = request.EndDate - request.StartDate;
            int totalDias = span.Days + 1;

            var resultado = new CountDaysOfWeekResult();

            for (int i = 0; i < totalDias; i++)
            {
                var dia = request.StartDate.AddDays(i);

                switch (dia.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        resultado.Monday++;
                        break;
                    case DayOfWeek.Tuesday:
                        resultado.Tuesday++;
                        break;
                    case DayOfWeek.Wednesday:
                        resultado.Wednesday++;
                        break;
                    case DayOfWeek.Thursday:
                        resultado.Thursday++;
                        break;
                    case DayOfWeek.Friday:
                        resultado.Friday++;
                        break;
                    case DayOfWeek.Saturday:
                        resultado.Saturday++;
                        break;
                    case DayOfWeek.Sunday:
                        resultado.Sunday++;
                        break;
                }
            }

            return Task.FromResult(resultado);
        }

        public Task<int> Handle(CountBusinessDaysCommand request, CancellationToken cancellationToken)
        {
            (request.StartDate, request.EndDate) = DateHelper.OrderDates(request.StartDate, request.EndDate);

            HashSet<DateTime> holidays = request.UseHolidays ? DateHelper.GetBrazilianHolidays(request.StartDate.Year, request.EndDate.Year) : new HashSet<DateTime>();

            var totalDays = (request.EndDate - request.StartDate).Days + 1;
            var completeWeeks = totalDays / 7;
            var remainingDays = totalDays % 7;

            var businessDays = completeWeeks * 5;

            for (var i = 0; i < remainingDays; i++)
            {
                var currentDate = request.StartDate.AddDays(completeWeeks * 7 + i);
                if (currentDate.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday) && !holidays.Contains(currentDate.Date))
                {
                    businessDays++;
                }
            }

            return Task.FromResult(businessDays);
        }

    }
}