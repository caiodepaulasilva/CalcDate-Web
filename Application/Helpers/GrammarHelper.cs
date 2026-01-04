namespace Application.Helpers
{
    public static class GrammarHelper
    {
        public static string NumberInflection(int quantity, string singular, string plural)
        {
            if (quantity == 0)
                return $"0 {plural}";

            if (quantity == 1)
                return $"1 {singular}";

            return $"{quantity} {plural}";
        }
    }
}
