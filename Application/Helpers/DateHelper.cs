namespace Application.Helpers
{
    public static class DateHelper
    {
        public static (DateTime, DateTime) OrderDates(DateTime date1, DateTime date2)
       => date1 > date2 ? (date2, date1) : (date1, date2);

        public static int DaysInMonth(int month, bool leapYear)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException("O mês deve estar entre 1 e 12.");

            return month switch
            {
                1 => 31,
                2 => leapYear ? 29 : 28,
                3 => 31,
                4 => 30,
                5 => 31,
                6 => 30,
                7 => 31,
                8 => 31,
                9 => 30,
                10 => 31,
                11 => 30,
                12 => 31,
                _ => 0,
            };
        }

        public static HashSet<DateTime> GetBrazilianHolidays(int startYear, int endYear)
        {
            var holidays = new HashSet<DateTime>();

            for (int year = startYear; year <= endYear; year++)
            {
                // Feriados nacionais fixos
                holidays.Add(new DateTime(year, 1, 1));   // Ano Novo
                holidays.Add(new DateTime(year, 4, 21));  // Tiradentes
                holidays.Add(new DateTime(year, 5, 1));   // Dia do Trabalho
                holidays.Add(new DateTime(year, 9, 7));   // Independência
                holidays.Add(new DateTime(year, 10, 12)); // Nossa Senhora Aparecida
                holidays.Add(new DateTime(year, 11, 2));  // Finados
                holidays.Add(new DateTime(year, 11, 15)); // Proclamação da República
                holidays.Add(new DateTime(year, 12, 25)); // Natal

                // Feriados móveis (Páscoa e derivados)
                var easter = CalculateEaster(year);
                holidays.Add(easter);                     // Páscoa
                holidays.Add(easter.AddDays(-47));        // Carnaval
                holidays.Add(easter.AddDays(-2));         // Sexta-feira Santa
                holidays.Add(easter.AddDays(60));         // Corpus Christi
            }

            return holidays;
        }

        private static DateTime CalculateEaster(int year)
        {
            // Algoritmo de Meeus/Jones/Butcher para calcular a Páscoa
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(year, month, day);
        }
    }
}