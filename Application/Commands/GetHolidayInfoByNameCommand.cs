using Domain;
using Domain.Enum;
using Mediator;

namespace Application.Commands
{
    public class GetHolidayInfoByNameCommand : IRequest<List<Holiday>>
    {
        public string Name { get; set; } = string.Empty;
        public required Locations Location { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
    }
}