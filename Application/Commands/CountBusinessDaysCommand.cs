using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class CountBusinessDaysCommand : IRequest<int>
    {
        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; } = DateTime.Now;

        public required bool UseHolidays { get; set; } = false;
    }
}
