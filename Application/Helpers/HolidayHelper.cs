using Domain;
using Domain.Enum;
using System.Globalization;

namespace Application.Helpers
{
    public static class HolidayHelper
    {
        
        private static readonly List<Holiday> holidays = GetAll();

        public static Dictionary<Locations, string> GetLocationsLabels()
        {
            return new Dictionary<Locations, string>
            {
                { Locations.Nacional, "Nacional" },
                { Locations.Acre, "Acre" },
                { Locations.Alagoas, "Alagoas" },
                { Locations.Amapa, "Amapá" },
                { Locations.Amazonas, "Amazonas" },
                { Locations.Bahia, "Bahia" },
                { Locations.Ceara, "Ceará" },
                { Locations.DistritoFederal, "Distrito Federal" },
                { Locations.EspiritoSanto, "Espírito Santo" },
                { Locations.Goias, "Goiás" },
                { Locations.Maranhao, "Maranhão" },
                { Locations.MatoGrosso, "Mato Grosso" },
                { Locations.MatoGrossoDoSul, "Mato Grosso do Sul" },
                { Locations.MinasGerais, "Minas Gerais" },
                { Locations.Para, "Pará" },
                { Locations.Paraiba, "Paraíba" },
                { Locations.Parana, "Paraná" },
                { Locations.Pernambuco, "Pernambuco" },
                { Locations.Piaui, "Piauí" },
                { Locations.RioDeJaneiro, "Rio de Janeiro" },
                { Locations.RioGrandeDoNorte, "Rio Grande do Norte" },
                { Locations.RioGrandeDoSul, "Rio Grande do Sul" },
                { Locations.Rondonia, "Rondônia" },
                { Locations.Roraima, "Roraima" },
                { Locations.SantaCatarina, "Santa Catarina" },
                { Locations.SaoPaulo, "São Paulo" },
                { Locations.Sergipe, "Sergipe" },
                { Locations.Tocantins, "Tocantins" }
            };
        }

        public static List<Holiday> GetAll()
        {
            return
            [
                // National
                new(date: "01/01", name: "Ano Novo", location: Locations.Nacional),
                new(date: "21/04", name: "Tiradentes", location: Locations.Nacional ),
                new(date: "01/05", name: "Dia do Trabalho", location: Locations.Nacional ),
                new(date: "07/09", name: "Independência do Brasil", location: Locations.Nacional ),
                new(date: "12/10", name: "Nossa Senhora Aparecida", location: Locations.Nacional ),
                new(date: "02/11", name: "Finados", location: Locations.Nacional ),
                new(date: "15/11", name: "Proclamação da República", location: Locations.Nacional ),
                new(date: "25/12", name: "Natal", location: Locations.Nacional ),

                // Acre
                new(date: "15/06", name: "Aniversário do Estado", location: Locations.Acre),
                new(date: "08/09", name: "Dia da Padroeira (Nossa Senhora da Imaculada Conceição)", location: Locations.Acre),
                new(date: "17/11", name: "Assinatura do Tratado de Petrópolis", location: Locations.Acre),

                // Alagoas
                new(date: "24/06", name: "Dia de São João", location: Locations.Alagoas),
                new(date: "29/06", name: "Dia de São Pedro", location: Locations.Alagoas),
                new(date: "16/09", name: "Emancipação Política", location: Locations.Alagoas),

                // Amapá
                new(date: "19/03", name: "Dia de São José (Padroeiro)", location: Locations.Amapa),
                new(date: "13/09", name: "Criação do Território Federal", location: Locations.Amapa),

                // Amazonas
                new(date: "05/09", name: "Elevação do Amazonas à Categoria de Província", location: Locations.Amazonas),
                new(date: "20/11", name: "Dia da Consciência Negra", location: Locations.Amazonas),
                new(date: "08/12", name: "Dia de Nossa Senhora da Conceição (Padroeira)", location: Locations.Amazonas),

                // Bahia
                new(date: "02/07", name: "Independência da Bahia", location: Locations.Bahia),

                // Ceará
                new(date: "25/03", name: "Data Magna do Estado", location: Locations.Ceara),

                // Distrito Federal
                new(date: "21/04", name: "Fundação de Brasília", location: Locations.DistritoFederal),
                new(date: "30/11", name: "Dia do Evangélico", location: Locations.DistritoFederal),

                // Espírito Santo
                new(date: "23/05", name: "Colonização do Solo Espírito-santense", location: Locations.EspiritoSanto ),

                // Goiás
                new(date: "28/10", name: "Dia do Servidor Público (estadual)", location: Locations.Goias ),

                // Maranhão
                new(date: "28/07", name: "Adesão do Maranhão à Independência do Brasil", location: Locations.Maranhao ),

                // Mato Grosso
                new(date: "20/11", name: "Dia da Consciência Negra", location: Locations.MatoGrosso ),

                // Mato Grosso do Sul
                new(date: "11/10", name: "Criação do Estado", location: Locations.MatoGrossoDoSul ),

                // Minas Gerais
                new(date: "21/04", name: "Data Magna (Tiradentes)", location: Locations.MinasGerais ),

                // Pará
                new(date: "15/08", name: "Adesão do Pará à Independência do Brasil", location: Locations.Para ),

                // Paraíba
                new(date: "05/08", name: "Fundação do Estado e Dia da Padroeira (Nossa Senhora das Neves)", location: Locations.Paraiba ),

                // Paraná
                new(date: "19/12", name: "Emancipação Política", location: Locations.Parana ),

                // Pernambuco
                new(date: "06/03", name: "Revolução Pernambucana de 1817", location: Locations.Pernambuco ),

                // Piauí
                new(date: "19/10", name: "Dia do Piauí", location: Locations.Piaui ),

                // Rio de Janeiro
                new(date: "20/11", name: "Dia da Consciência Negra", location: Locations.RioDeJaneiro ),
                new(date: "23/04", name: "Dia de São Jorge", location: Locations.RioDeJaneiro ),

                // Rio Grande do Norte
                new(date: "03/10", name: "Mártires de Cunhaú e Uruaçu", location: Locations.RioGrandeDoNorte ),

                // Rio Grande do Sul
                new(date: "20/09", name: "Revolução Farroupilha", location: Locations.RioGrandeDoSul ),

                // Rondônia
                new(date: "04/01", name: "Criação do Estado", location: Locations.Rondonia ),
                new(date: "18/06", name: "Dia do Evangélico", location: Locations.Rondonia ),

                // Roraima
                new(date: "05/10", name: "Criação do Estado", location: Locations.Roraima ),

                // Santa Catarina
                new(date: "11/08", name: "Dia de Santa Catarina (Criação da Capitania)", location: Locations.SantaCatarina ),
                new(date: "25/07", name: "Dia do Colono e Motorista", location: Locations.SantaCatarina ),

                // São Paulo
                new(date: "09/07", name: "Revolução Constitucionalista de 1932", location: Locations.SaoPaulo ),
                new(date: "20/11", name: "Dia da Consciência Negra", location: Locations.SaoPaulo),

                // Sergipe
                new(date: "08/07", name: "Autonomia Política de Sergipe", location: Locations.Sergipe),

                // Tocantins
                new(date: "05/10", name: "Criação do Estado", location: Locations.Tocantins),
                new(date: "18/03", name: "Autonomia do Estado (Criação da Comarca do Norte)", location: Locations.Tocantins)
            ];
        }

        public static List<Holiday> GetHolidays(int year, Locations? locationFilter = null)
        {
            var fixedHolidays = GetFixedHolidays(year, locationFilter);
            var movableHolidays = GetMovableHolidays(year, locationFilter);
            
            var allHolidays = new List<Holiday>();
            allHolidays.AddRange(fixedHolidays);
            allHolidays.AddRange(movableHolidays);

            return [.. allHolidays.OrderBy(h => h.Date)];
        }

        private static List<Holiday> GetFixedHolidays(int year, Locations? locationFilter)
        {            
            var fixedHolidays = new List<Holiday>();

            foreach (var holiday in holidays)
            {
                if (!IsValidHoliday(holiday, locationFilter)) continue;

                if (TryParseDate(holiday.Date, year, out var date))
                {
                    fixedHolidays.Add(new Holiday(date.ToString("dd/MM/yyyy"), holiday.Name.Trim(), holiday.Location));
                }
            }

            return fixedHolidays;
        }

        private static List<Holiday> GetMovableHolidays(int year, Locations? locatioFilter)
        {
            if (locatioFilter == Locations.Nacional)
            { 
                var pascoa = CalculatePascoa(year);
                var movableHolidays = new[]
                {
                    new Holiday(pascoa.ToString("dd/MM/yyyy"), "Páscoa", Locations.Nacional),
                        new Holiday(pascoa.AddDays(-47).ToString("dd/MM/yyyy"), "Carnaval", Locations.Nacional),
                        new Holiday(pascoa.AddDays(-2).ToString("dd/MM/yyyy"), "Sexta-feira Santa", Locations.Nacional),
                        new Holiday(pascoa.AddDays(60).ToString("dd/MM/yyyy"), "Corpus Christi", Locations.Nacional)
                };
                return [.. movableHolidays];
            }
            return [];
        }
        
        private static DateOnly CalculatePascoa(int year)
        {
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

            return new DateOnly(year, month, day);
        }

        private static bool IsValidHoliday(Holiday holiday, Locations? locationFilter)
        {
            if (string.IsNullOrWhiteSpace(holiday.Date) || string.IsNullOrWhiteSpace(holiday.Name))
                return false;

            return locationFilter switch
            {
                null => holiday.Location == Locations.Nacional,
                _ => holiday.Location == locationFilter.Value
            };
        }

        private static bool TryParseDate(string dateString, int year, out DateOnly date)
        {
           date = default;

            if (string.IsNullOrWhiteSpace(dateString))
                return false;

            var fullDate = $"{dateString}/{year}";
            return DateOnly.TryParseExact(
                fullDate,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date
            );
        }
    }
}