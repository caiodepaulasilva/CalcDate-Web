using Domain.Enum;
namespace Domain
{
    public class Holiday : Entity
    {
        public string? Date { get; set; }
        public string? Name { get; set; }
        public Locations Location { get; set; }
        public string DayOfWeek { get; set; }

        public Holiday(string? date, string? name, Locations location)
        {
            Date = date;
            Name = name;
            Location = location;
        }

        public Holiday(string? date, string? name, Locations location, string dayofWeek)
        {
            Date = date;
            Name = name;
            Location = location;
            DayOfWeek = dayofWeek;
        }
    }
}
