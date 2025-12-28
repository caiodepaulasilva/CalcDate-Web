using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class CountDaysOfWeekCommand : IRequest<CountDaysOfWeekResult>
    {
        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; } = DateTime.Now;        
    }
}
