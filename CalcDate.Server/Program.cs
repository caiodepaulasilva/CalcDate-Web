using Application.Services;
using CalcDate.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DateService>();
builder.Services.AddScoped<HolidayService>();
builder.Services.AddScoped<LocationService>();

builder.Services.AddCors(static options =>
{
    options.AddPolicy("AllowAngularApp",
        static policy =>
        {
            policy.WithOrigins("http://localhost:56459")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

var apiV1 = app.MapGroup("/api/v1");

apiV1.MapDateEndpoints();
apiV1.MapHolidayEndpoints();

app.MapFallbackToFile("/index.html");

app.Run();

