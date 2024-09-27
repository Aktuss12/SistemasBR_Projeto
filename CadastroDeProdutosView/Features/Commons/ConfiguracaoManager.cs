using System.IO;
using System.Xml.Serialization;

namespace CadastroDeProdutosView.Features.Commons
{
    public class ConfiguracaoBanco
    {
        public string CaminhoBanco { get; set; }
    }

    public static class ConfiguracaoManager
    {
        private const string ArquivoConfiguracao = "config.xml";

        public static void SalvarConfiguracao(ConfiguracaoBanco config)
        {
            var serializer = new XmlSerializer(typeof(ConfiguracaoBanco));
            using var writer = new StreamWriter(ArquivoConfiguracao);
            serializer.Serialize(writer, config);
        }

        public static ConfiguracaoBanco CarregarConfiguracao()
        {
            if (!File.Exists(ArquivoConfiguracao))
            {
                return new ConfiguracaoBanco { CaminhoBanco = @"C:\Users\admin\Documents\BANCODEDADOSPRODUTOS.FDB" };
            }

            var serializer = new XmlSerializer(typeof(ConfiguracaoBanco));
            using var reader = new StreamReader(ArquivoConfiguracao);
            return (ConfiguracaoBanco)serializer.Deserialize(reader);
        }

        public static string ObterStringConexao()
        {
            var config = CarregarConfiguracao();
            return $"User ID=SYSDBA;Password=masterkey;Database={config.CaminhoBanco};DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;";
        }
    }
}