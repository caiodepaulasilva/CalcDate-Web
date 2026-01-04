using Domain;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CalcDate.Server.Extensions;

public static class EndpointExtensions
{
    public static RouteGroupBuilder MapDateEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/Date/DiffBetweenDates", (
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromServices] Application.Services.DateService dateService) =>
        {
            try
            {
                var result = dateService.GetDiffBetweenDates(startDate, endDate);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Results.Problem(title: "Erro interno do servidor", detail: ex.Message, statusCode: 500);
            }
        });

        group.MapGet("/Date/CountDaysOfWeek", (
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromServices] Application.Services.DateService dateService) =>
        {
            try
            {
                var result = dateService.CountDaysOfWeek(startDate, endDate);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Results.Problem(title: "Erro interno do servidor", detail: ex.Message, statusCode: 500);
            }
        });

        group.MapGet("/Date/CountBusinessDays", (
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromServices] Application.Services.DateService dateService) =>
        {
            try
            {
                var result = dateService.CountBusinessDays(startDate, endDate);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Results.Problem(title: "Erro interno do servidor", detail: ex.Message, statusCode: 500);
            }
        });

        return group;
    }

    public static RouteGroupBuilder MapHolidayEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/Holiday/name", (
            [FromQuery] string? name,
            [FromQuery] Locations location,
            [FromQuery] int? year,
            [FromServices] Application.Services.HolidayService holidayService) =>
        {
            try
            {
                var result = holidayService.GetHolidaysByName(name, location, year ?? DateTime.Now.Year);
                return Results.Ok(result.Count == 0 ? new List<Holiday>() : result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Results.Problem(title: "Erro interno do servidor", detail: ex.Message, statusCode: 500);
            }
        });

        group.MapGet("/Holiday/locations", ([FromServices] Application.Services.LocationService locationService) =>
        {
            try
            {
                var result = locationService.GetAllLocations();
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Results.Problem(title: "Erro interno do servidor", detail: ex.Message, statusCode: 500);
            }
        });

        return group;
    }
}
