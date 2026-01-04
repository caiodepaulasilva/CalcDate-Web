using Application.Models;
using System;
using Mediator;

namespace Application.Commands
{
    public class CountBusinessDaysCommand : IRequest<int>
    {
        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);        
    }
}
