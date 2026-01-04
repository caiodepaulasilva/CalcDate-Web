using Application.Commands;
using Mediator;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator();

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

builder.Services.AddControllers();

builder.Services.AddCors(static options =>
{
    options.AddPolicy("AllowAngularApp",
        static policy =>
        {
            policy.WithOrigins("http://localhost:56459") // URL do Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

