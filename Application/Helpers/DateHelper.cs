namespace Application.Helpers
{
    public static class DateHelper
    {
        public static (DateOnly, DateOnly) OrderDates(DateOnly date1, DateOnly date2)
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
    }
}