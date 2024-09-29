using System.IO;
using System.Xml.Serialization;

namespace CadastroDeProdutosView.Features.Commons
{
    public class ConfiguracaoBanco
    {
        public string CaminhoBanco { get; set; }
    }

    public static class ConfiguracaoDoBancoDeDados
    {
        public static string ObterStringDeConexao()
        {
            var carregarBanco = CarregarBancoDeDados();
            return $"User ID=SYSDBA;Password=masterkey;Database={carregarBanco.CaminhoBanco};DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;";
        }

        private const string ArquivoConfiguracao = "config.xml";

        public static void SalvarConfiguracao(ConfiguracaoBanco config)
        {
            var serializer = new XmlSerializer(typeof(ConfiguracaoBanco));
            using var writer = new StreamWriter(ArquivoConfiguracao);
            serializer.Serialize(writer, config);
        }

        public static ConfiguracaoBanco CarregarBancoDeDados()
        {
            if (!File.Exists(ArquivoConfiguracao))
            {
                return new ConfiguracaoBanco { CaminhoBanco = @"C:\Users\admin\Documents\BANCODEDADOSPRODUTOS.FDB" };
            }

            var serializer = new XmlSerializer(typeof(ConfiguracaoBanco));
            using var reader = new StreamReader(ArquivoConfiguracao);
            return (ConfiguracaoBanco)serializer.Deserialize(reader);
        }
    }
}