using Application.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace CalcDate.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DateController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("DiffBetweenDates")]
        public async Task<IActionResult> GetDiffBetweenDates(
            [FromQuery] GetDiffBetweenDatesCommand command,
            [FromServices] IValidator<GetDiffBetweenDatesCommand> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return StatusCode(500, new { error = "Erro interno do servidor", message = ex.Message });
            }
        }

        [HttpGet("CountDaysOfWeek")]
        public async Task<IActionResult> CountDaysOfWeek(
            [FromQuery] CountDaysOfWeekCommand command,
            [FromServices] IValidator<CountDaysOfWeekCommand> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return StatusCode(500, new { error = "Erro interno do servidor", message = ex.Message });
            }
        }

        [HttpGet("CountBusinessDays")]
        public async Task<IActionResult> CountBusinessDays(
            [FromQuery] CountBusinessDaysCommand command,
            [FromServices] IValidator<CountBusinessDaysCommand> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return StatusCode(500, new { error = "Erro interno do servidor", message = ex.Message });
            }
        }
    }
}