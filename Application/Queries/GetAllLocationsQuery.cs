using Application.Models;
using Mediator;

namespace Application.Queries
{
    public class GetAllLocationsQuery : IRequest<List<LocationDto>>
    {
    }
}
