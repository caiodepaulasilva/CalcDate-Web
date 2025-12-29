namespace Application.Helpers
{
    public static class GrammarHelper
    {
        public static string NumberInflection(int quantidade, string singular, string plural)
        {
            if (quantidade == 0)
                return $"0 {plural}";

            if (quantidade == 1)
                return $"1 {singular}";

            return $"{quantidade} {plural}";
        }
    }
}
