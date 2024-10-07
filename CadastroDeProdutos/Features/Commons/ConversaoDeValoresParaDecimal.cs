using System.Globalization;
using System.Linq;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class ConversorParaDecimal
    {
        public static decimal ParseDecimal(string value)
        {
            var limparValor = new string(value.Where(c => char.IsDigit(c) || c == ',').ToArray());
            limparValor = limparValor.Replace(",", ".");

            return decimal.TryParse(limparValor, NumberStyles.Any, CultureInfo.InvariantCulture, out var resultado)
                ? resultado
                : 0;
        }
    }
}