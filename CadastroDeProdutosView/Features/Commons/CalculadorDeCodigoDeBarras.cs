namespace CadastroDeProdutosView.Features.Commons
{
    public static class CalculadorDeCodigoDeBarras
    {
        public static bool ValidarCodigoDeBarrasEAN13(string codigoDeBarras) 
        {
            if (codigoDeBarras.Length != 13 || !long.TryParse(codigoDeBarras, out _))
                return false;

            var numeros = new int[12];
            for (var i = 0; i < 12; i++)
            {
                numeros[i] = int.Parse(codigoDeBarras[i].ToString());
            }

            var somaPar = 0;
            var somaImpar = 0;
            for (var i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                    somaImpar += numeros[i];
                else
                    somaPar += numeros[i];
            }

            var somaTotal = somaImpar + (somaPar * 3);
            var digitoVerificadorCalculado = (10 - (somaTotal % 10)) % 10;
            var digitoVerificadorInformado = int.Parse(codigoDeBarras[12].ToString());

            return digitoVerificadorCalculado == digitoVerificadorInformado;
        }
    }
}