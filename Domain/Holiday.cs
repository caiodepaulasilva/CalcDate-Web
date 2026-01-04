using Domain.Enum;
namespace Domain
{
    public class Holiday(string? date, string? name, Locations location) : Entity
    {
        public string? Date { get; set; } = date;
        public string? Name { get; set; } = name;
        public Locations Location { get; set; } = location;
    }
}
