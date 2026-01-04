using Application.Commands;
using Application.Helpers;
using Domain;
using Mediator;

namespace Application.Handlers
{
    public class HolidayInfoHandler :
        IRequestHandler<GetHolidayInfoByNameCommand, List<Holiday>>
    {     
        public ValueTask<List<Holiday>> Handle(GetHolidayInfoByNameCommand request, CancellationToken cancellationToken)
        {            
            var holidays = HolidayHelper.GetHolidays(request.Year, request.Location);
            
            var filteredHolidays = holidays
                .Where(holiday => holiday.Name.Contains(request.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var uniqueResults = filteredHolidays
                .GroupBy(h => (h.Name + "|" + h.Location).ToLowerInvariant())
                .Select(g => g.First())
                .ToList();

            return new ValueTask<List<Holiday>>(uniqueResults);
        }       
    }
}