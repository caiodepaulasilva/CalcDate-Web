namespace Application.Models
{
    public class DiffBetweenDatesResult
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        public override string ToString()
        {
            var parts = new List<string>();

            if (Years > 0)
                parts.Add($"{Years} {(Years == 1 ? "ano" : "anos")}");
            if (Months > 0)
                parts.Add($"{Months} {(Months == 1 ? "mês" : "meses")}");
            if (Days > 0)
                parts.Add($"{Days} {(Days == 1 ? "dia" : "dias")}");

            return parts.Count switch
            {
                0 => "0 dias",
                1 => parts[0],
                2 => $"{parts[0]} e {parts[1]}",
                _ => $"{string.Join(", ", parts.Take(parts.Count - 1))} e {parts.Last()}",
            };
        }
    }
}