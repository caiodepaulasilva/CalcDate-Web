using Application.Helpers;

namespace Application.Models
{
    public class CountDaysOfWeekResult
    {
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }

        public override string ToString()
        {
            return $"{GrammarHelper.NumberInflection(Monday, "Segunda-feira", "Segundas-feiras")}\n" +
                   $"{GrammarHelper.NumberInflection(Tuesday, "Terça-feira", "Terças-feiras")}\n" +
                   $"{GrammarHelper.NumberInflection(Wednesday, "Quarta-feira", "Quartas-feiras")}\n" +
                   $"{GrammarHelper.NumberInflection(Thursday, "Quinta-feira", "Quintas-feiras")}\n" +
                   $"{GrammarHelper.NumberInflection(Friday, "Sexta-feira", "Sextas-feiras")}\n" +
                   $"{GrammarHelper.NumberInflection(Saturday, "Sábado", "Sábados")}\n" +
                   $"{GrammarHelper.NumberInflection(Sunday, "Domingo", "Domingos")}";
        }
    }
}