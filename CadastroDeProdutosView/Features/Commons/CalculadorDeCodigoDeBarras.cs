using System;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class CalculadorDeCodigoDeBarras
    {
        public static string GerarCodigoDeBarrasEAN13()
        {
            var random = new Random();
            var codigoParcial = "";
            for (var i = 0; i < 12; i++)
            {
                codigoParcial += random.Next(0, 10).ToString();
            }

            var digitoVerificador = CalcularDigitoVerificador(codigoParcial);
            return codigoParcial + digitoVerificador;
        }

        private static string CalcularDigitoVerificador(string codigoParcial)
        {
            var numeros = new int[12];
            for (var i = 0; i < 12; i++)
            {
                numeros[i] = int.Parse(codigoParcial[i].ToString());
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
            var digitoVerificador = (10 - (somaTotal % 10)) % 10;

            return digitoVerificador.ToString();
        }
    }
}