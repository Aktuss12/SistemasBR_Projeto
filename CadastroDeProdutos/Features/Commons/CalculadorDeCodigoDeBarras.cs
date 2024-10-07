using System;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class CalculadorDeCodigoDeBarras
    {
        // Método para validar um código de barras EAN-13
        public static bool ValidandoCodigoDeBarrasEAN13(string codigoDeBarras)
        {
            // Verifica se o comprimento do código é exatamente 13
            if (codigoDeBarras.Length != 13) return false;

            var soma = 0; // Inicializa a soma dos dígitos
            // Loop para calcular a soma dos primeiros 12 dígitos
            for (var i = 0; i < 12; i++)
            {
                var digito = codigoDeBarras[i] - '0'; // Converte o caractere para seu valor numérico
                // Se o índice for par, soma o dígito; se ímpar, soma o dígito multiplicado por 3
                soma += (i % 2 == 0) ? digito : digito * 3;
            }

            // Calcula o dígito verificador
            var digitoVerificador = (10 - (soma % 10)) % 10;
            // Compara o dígito verificador calculado com o dígito verificador no código de barras
            return digitoVerificador == codigoDeBarras[12] - '0';
        }

        // Método para gerar um novo código de barras EAN-13
        public static string GerarCodigoDeBarrasEAN13()
        {
            var gerador = new Random(); // Cria uma nova instância de Random para gerar números aleatórios

            // Gera o primeiro bloco de 12 dígitos
            long codigo12Digitos = gerador.Next(100000, 999999);
            // Gera um segundo bloco de 6 dígitos e combina os dois
            codigo12Digitos = codigo12Digitos * 1000000 + gerador.Next(100000, 999999);
            var codigoString = codigo12Digitos.ToString(); // Converte o número gerado para string

            var soma = 0; // Inicializa a soma dos dígitos
            // Loop para calcular a soma dos 12 dígitos gerados
            for (var i = 0; i < 12; i++)
            {
                var digito = codigoString[i] - '0'; // Converte o caractere para seu valor numérico
                // Se o índice for par, soma o dígito; se ímpar, soma o dígito multiplicado por 3
                soma += (i % 2 == 0) ? digito : digito * 3;
            }

            // Calcula o dígito verificador
            var digitoVerificador = (10 - (soma % 10)) % 10;
            // Retorna o código de barras completo (12 dígitos + dígito verificador)
            return codigoString + digitoVerificador;
        }
    }
}
