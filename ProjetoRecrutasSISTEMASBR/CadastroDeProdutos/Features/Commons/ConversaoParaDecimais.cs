namespace CadastroDeProdutosView.Features.Commons
{
    public static class ConversaoParaDecimais
    {
        public static decimal ConversaoParaDecimal(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            if (decimal.TryParse(valor, out var resultado))
                return resultado;

            return 0;
        }
    }
}
