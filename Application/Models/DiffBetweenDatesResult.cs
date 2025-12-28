namespace Application.Models
{
    public class DiffBetweenDatesResult
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        public override string ToString()
        {
            var partes = new List<string>();

            if (Years > 0)
                partes.Add($"{Years} {(Years == 1 ? "ano" : "anos")}");
            if (Months > 0)
                partes.Add($"{Months} {(Months == 1 ? "mês" : "meses")}");
            if (Days > 0)
                partes.Add($"{Days} {(Days == 1 ? "dia" : "dias")}");

            return partes.Count switch
            {
                0 => "0 dias",
                1 => partes[0],
                2 => $"{partes[0]} e {partes[1]}",
                _ => $"{string.Join(", ", partes.Take(partes.Count - 1))} e {partes.Last()}",
            };
        }
    }
}