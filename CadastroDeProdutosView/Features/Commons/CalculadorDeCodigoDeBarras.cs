using System;
using System.Linq;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class CalculadorDeCodigoDeBarras
    {
        public static bool ValidarCodigoDeBarrasEan13(string codigoDeBarras)
        {
            if (codigoDeBarras.Length != 13 || !codigoDeBarras.All(char.IsDigit))
                return false;

            var codigoParcial = codigoDeBarras.Substring(0, 12);

            if (!int.TryParse(codigoDeBarras[12].ToString(), out var digitoVerificadorInformado))
                return false;

            var digitoVerificadorCalculado = CalcularDigitoVerificador(codigoParcial);

            return digitoVerificadorCalculado == digitoVerificadorInformado;
        }

        public static string GerarCodigoDeBarrasEan13()
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

        private static int CalcularDigitoVerificador(string codigoParcial)
        {
            int somaPar = 0, somaImpar = 0;

            for (var i = 0; i < codigoParcial.Length; i++)
            {
                var numero = int.Parse(codigoParcial[i].ToString());
                if (i % 2 == 0)
                    somaImpar += numero;
                else
                    somaPar += numero;
            }

            var somaTotal = somaImpar + (somaPar * 3);
            var digitoVerificador = (10 - (somaTotal % 10)) % 10;

            return digitoVerificador;
        }
    }
}
