using Application.Commands;
using Application.Models;
using Application.Queries;
using Domain;
using Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CalcDate.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class HolidayController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("name")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByNameAsync(
            [FromQuery] string? Name,
            [FromQuery] Locations Location,
            [FromQuery] int? Year)
        {
            try 
            {
                var command = new GetHolidayInfoByNameCommand
                {
                    Name = Name ?? string.Empty,
                    Location = Location,
                    Year = Year ?? DateTime.Now.Year
                };
                
                var response = await _mediator.Send(command);
                
                if (response == null || response.Count == 0)
                {
                    return Ok(new List<Holiday>());
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");

                return StatusCode(500, new { error = "Erro interno do servidor", message = ex.Message });
            }
        }

        [HttpGet("locations")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLocations()
        {
            try 
            { 
                var items = await _mediator.Send(new GetAllLocationsQuery());
                return Ok(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");

                return StatusCode(500, new { error = "Erro interno do servidor", message = ex.Message });
            }
        }
    }
}
