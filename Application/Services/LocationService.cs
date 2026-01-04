using Application.Models;
using Domain.Enum;

namespace Application.Services
{
    public class LocationService
    {
        public List<LocationDto> GetAllLocations()
        {
            var locations = Enum.GetValues<Locations>()
                .Select(location => new LocationDto
                {
                    Value = location.ToString(),
                    Label = FormatLocationName(location.ToString())
                })
                .ToList();

            return locations;
        }

        private static string FormatLocationName(string value)
        {
            return value switch
            {
                "DistritoFederal" => "Distrito Federal",
                "EspiritoSanto" => "Espírito Santo",
                "MatoGrosso" => "Mato Grosso",
                "MatoGrossoDoSul" => "Mato Grosso do Sul",
                "MinasGerais" => "Minas Gerais",
                "RioDeJaneiro" => "Rio de Janeiro",
                "RioGrandeDoNorte" => "Rio Grande do Norte",
                "RioGrandeDoSul" => "Rio Grande do Sul",
                "SantaCatarina" => "Santa Catarina",
                "SaoPaulo" => "São Paulo",
                _ => AddSpacesToPascalCase(value)
            };
        }

        private static string AddSpacesToPascalCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var result = new System.Text.StringBuilder(text.Length * 2);
            result.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && !char.IsUpper(text[i - 1]))
                    result.Append(' ');
                result.Append(text[i]);
            }

            return result.ToString();
        }
    }
}
