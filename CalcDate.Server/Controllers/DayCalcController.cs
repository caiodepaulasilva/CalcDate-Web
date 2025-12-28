using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalcDate.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DayCalculationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DayCalculationsController(IMediator mediator) => _mediator = mediator;


        [HttpGet("DiffBetweenDates")]
        public async Task<IActionResult> GetDiffBetweenDates([FromQuery] GetDiffBetweenDatesCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Retorne um erro JSON, não uma página HTML
                return StatusCode(500, new
                {
                    error = "Erro interno do servidor",
                    message = ex.Message
                });
            }
        }

        [HttpGet("CountDaysOfWeek")]
        public async Task<IActionResult> CountDaysOfWeek([FromQuery] CountDaysOfWeekCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Retorne um erro JSON, não uma página HTML
                return StatusCode(500, new
                {
                    error = "Erro interno do servidor",
                    message = ex.Message
                });
            }
        }

        [HttpGet("CountBusinessDays")]
        public async Task<IActionResult> CountBusinessDays([FromQuery] CountBusinessDaysCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Retorne um erro JSON, não uma página HTML
                return StatusCode(500, new
                {
                    error = "Erro interno do servidor",
                    message = ex.Message
                });
            }
        }

    }
}