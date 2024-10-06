using CadastroDeProdutosView.FEATURES.COMMONS.SERVICES;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Drawing;
using System.IO;

namespace CadastroDeProdutosView.Features.Commons
{
    public class ConexaoProdutosFirebird
    {
        private readonly string _connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();

        private FbConnection PegarConexao()
        {
            return new FbConnection(_connectionString);
        }
        public class Produto
        {
            public int Id { get; set; }
            public string? Nome { get; set; }
            public string? Categoria { get; set; }
            public string? Fornecedor { get; set; }
            public string? CodigoDeBarras { get; set; }
            public string? UnidadeDeMedida { get; set; }
            public int Estoque { get; set; }
            public string? Marca { get; set; }
            public decimal Custo { get; set; }
            public decimal Markup { get; set; }
            public decimal PrecoDaVenda { get; set; }
            public byte[]? Imagem { get; set; }
            public InformacoesFiscais InformacoesFiscais { get; set; } = null!;
        }

        public class InformacoesFiscais
        {
            public string? OrigemDaMercadoria { get; set; }
            public string? SituacaoTributaria { get; set; }
            public string? NaturezaDaOperacao { get; set; }
            public string? Ncm { get; set; }
            public decimal AliquotaDeIcms { get; set; }
            public decimal ReducaoDeCalculo { get; set; }
        }

        public int InserirProduto(Produto produto)
        {
            try
            {
                using var conexao = PegarConexao();
                conexao.Open();
                using var transacao = conexao.BeginTransaction();

                const string insertProdutoQuery = @"
                    INSERT INTO PRODUTO
                    (nome, categoria, Fornecedor, codigoDeBarras, unidadeDeMedida, estoque, marca, custo, markup, precoDaVenda, imagem)
                    VALUES (@Nome, @Categoria, @Fornecedor, @codigoDeBarras, @unidadeDeMedida, @Estoque, @Marca, @Custo, @Markup, @precoDaVenda, @Imagem)
                    RETURNING idProduto";

                int idProduto;

                using (var command = new FbCommand(insertProdutoQuery, conexao, transacao))
                {
                    command.Parameters.Add("@Nome", FbDbType.VarChar).Value = produto.Nome ?? (object)DBNull.Value;
                    command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = produto.Categoria!;
                    command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = produto.Fornecedor ?? (object)DBNull.Value;
                    command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = produto.CodigoDeBarras ?? (object)DBNull.Value;
                    command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = produto.UnidadeDeMedida!;
                    command.Parameters.Add("@Marca", FbDbType.VarChar).Value = produto.Marca!;
                    command.Parameters.Add("@Estoque", FbDbType.Integer).Value = produto.Estoque;
                    command.Parameters.Add("@Custo", FbDbType.Decimal).Value = produto.Custo;
                    command.Parameters.Add("@Markup", FbDbType.Decimal).Value = produto.Markup;
                    command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = produto.PrecoDaVenda;
                    command.Parameters.Add("@Imagem", FbDbType.Binary).Value = produto.Imagem ?? (object)DBNull.Value;

                    idProduto = (int)command.ExecuteScalar();
                }

                InserirInformacoesFiscais(conexao, transacao, idProduto, produto.InformacoesFiscais);

                transacao.Commit();
                return idProduto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir o produto", ex);
            }
        }

        public void AlterarProduto(Produto produto)
        {
            try
            {
                using var conexao = PegarConexao();
                conexao.Open();
                using var transacao = conexao.BeginTransaction();

                const string updateProdutoQuery = @"
                UPDATE PRODUTO
                SET Nome = @Nome, 
                    Categoria = @Categoria, 
                    Fornecedor = @Fornecedor, 
                    CodigoDeBarras = @CodigoDeBarras, 
                    UnidadeDeMedida = @UnidadeDeMedida, 
                    Estoque = @Estoque, 
                    Marca = @Marca, 
                    Custo = @Custo, 
                    Markup = @Markup, 
                    PrecoDaVenda = @PrecoDaVenda,
                    imagem = @Imagem
                WHERE idProduto = @idProduto";

                using (var command = new FbCommand(updateProdutoQuery, conexao, transacao))
                {
                    command.Parameters.Add("@idProduto", FbDbType.Integer).Value = produto.Id;
                    command.Parameters.Add("@Nome", FbDbType.VarChar).Value = produto.Nome ?? (object)DBNull.Value;
                    command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = produto.Categoria ?? (object)DBNull.Value;
                    command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = produto.Fornecedor ?? (object)DBNull.Value;
                    command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = produto.CodigoDeBarras ?? (object)DBNull.Value;
                    command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = produto.UnidadeDeMedida ?? (object)DBNull.Value;
                    command.Parameters.Add("@Estoque", FbDbType.Integer).Value = produto.Estoque;
                    command.Parameters.Add("@Marca", FbDbType.VarChar).Value = produto.Marca ?? (object)DBNull.Value;
                    command.Parameters.Add("@Custo", FbDbType.Decimal).Value = produto.Custo;
                    command.Parameters.Add("@Markup", FbDbType.Decimal).Value = produto.Markup;
                    command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = produto.PrecoDaVenda;
                    command.Parameters.Add("@Imagem", FbDbType.Binary).Value = produto.Imagem ?? (object)DBNull.Value;
                    command.ExecuteNonQuery();
                }

                AlterarInformacoesFiscais(conexao, transacao, produto.Id, produto.InformacoesFiscais);

                transacao.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar o produto", ex);
            }
        }

        public Produto CarregarProduto(int idProduto)
        {
            try
            {
                using var conexao = PegarConexao();
                conexao.Open();
                const string preenchimentoDeTabelasQuery = @"
                    SELECT P.*, I.*
                    FROM PRODUTO P
                    LEFT JOIN INFORMACOESFISCAIS I ON P.idProduto = I.idProduto
                    WHERE P.idProduto = @idProduto";

                using var command = new FbCommand(preenchimentoDeTabelasQuery, conexao);
                command.Parameters.AddWithValue("@idProduto", idProduto);
                using var leituraDeDados = command.ExecuteReader();

                if (!leituraDeDados.Read())
                    return null!;

                var produto = new Produto
                {
                    Id = idProduto,
                    Nome = leituraDeDados["Nome"].ToString(),
                    Categoria = leituraDeDados["Categoria"].ToString(),
                    Fornecedor = leituraDeDados["Fornecedor"].ToString(),
                    CodigoDeBarras = leituraDeDados["CodigoDeBarras"].ToString(),
                    UnidadeDeMedida = leituraDeDados["UnidadeDeMedida"].ToString(),
                    Estoque = Convert.ToInt32(leituraDeDados["Estoque"]),
                    Marca = leituraDeDados["Marca"].ToString(),
                    Custo = Convert.ToDecimal(leituraDeDados["Custo"]),
                    Markup = Convert.ToDecimal(leituraDeDados["Markup"]),
                    PrecoDaVenda = Convert.ToDecimal(leituraDeDados["PrecoDaVenda"]),
                    InformacoesFiscais = new InformacoesFiscais
                    {
                        OrigemDaMercadoria = leituraDeDados["origemDaMercadoria"].ToString(),
                        SituacaoTributaria = leituraDeDados["situacaoTributaria"].ToString(),
                        NaturezaDaOperacao = leituraDeDados["naturezaDaOperacao"].ToString(),
                        Ncm = leituraDeDados["ncm"].ToString(),
                        AliquotaDeIcms = Convert.ToDecimal(leituraDeDados["aliquotaDeIcms"]),
                        ReducaoDeCalculo = Convert.ToDecimal(leituraDeDados["reducaoDeCalculo"])
                    }
                };

                if (leituraDeDados["Imagem"] != DBNull.Value)
                {
                    produto.Imagem = (byte[])leituraDeDados["Imagem"];
                }

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar o produto", ex);
            }
        }

        public bool ValidarCodigoDeBarras(string codigoDeBarras)
        {
            return CalculadorDeCodigoDeBarras.ValidarCodigoDeBarrasEAN13(codigoDeBarras);
        }

        public string GerarCodigoDeBarras()
        {
            return CalculadorDeCodigoDeBarras.GerarCodigoDeBarrasEAN13();
        }

        public byte[] ConverterImagemParaBytes(string caminhoDaImagem, int largura, int altura)
        {
            return ConversorDeImagemParaByte.ConversorDeValoresDeImagemParaByte(caminhoDaImagem, largura, altura);
        }

        public Image ConverterBytesParaImagem(byte[]? imagemBytes)
        {
            if (imagemBytes == null || imagemBytes.Length == 0)
                return null!;

            using var ms = new MemoryStream(imagemBytes);
            return Image.FromStream(ms);
        }

        public decimal CalcularPrecoVenda(decimal custo, decimal markup)
        {
            return custo * (1 + markup / 100);
        }

        private static void InserirInformacoesFiscais(FbConnection conexao, FbTransaction transacao, int idProduto, InformacoesFiscais informacoesFiscais)
        {
            const string insertInformacoesFiscaisQuery = @"
                INSERT INTO INFORMACOESFISCAIS
                (idProduto, origemDaMercadoria, situacaoTributaria, naturezaDaOperacao, ncm, aliquotaDeIcms, reducaoDeCalculo)
                VALUES (@idProduto, @origemDaMercadoria, @situacaoTributaria, @naturezaDaOperacao, @Ncm, @aliquotaDeIcms, @reducaoDeCalculo)";

            using var command = new FbCommand(insertInformacoesFiscaisQuery, conexao, transacao);
            command.Parameters.AddWithValue("@idProduto", idProduto);
            command.Parameters.AddWithValue("@origemDaMercadoria", informacoesFiscais.OrigemDaMercadoria ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@situacaoTributaria", informacoesFiscais.SituacaoTributaria ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@naturezaDaOperacao", informacoesFiscais.NaturezaDaOperacao ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ncm", informacoesFiscais.Ncm ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@aliquotaDeIcms", informacoesFiscais.AliquotaDeIcms);
            command.Parameters.AddWithValue("@reducaoDeCalculo", informacoesFiscais.ReducaoDeCalculo);
            command.ExecuteNonQuery();
        }

        private static void AlterarInformacoesFiscais(FbConnection conexao, FbTransaction transacao, int idProduto, InformacoesFiscais informacoesFiscais)
        {
            const string updateInformacoesFiscaisQuery = @"
                UPDATE INFORMACOESFISCAIS
                SET origemDaMercadoria = @origemDaMercadoria,
                    situacaoTributaria = @situacaoTributaria,
                    naturezaDaOperacao = @naturezaDaOperacao,
                    ncm = @ncm,
                    aliquotaDeIcms = @aliquotaDeIcms,
                    reducaoDeCalculo = @reducaoDeCalculo
                WHERE idProduto = @idProduto";

            using var command = new FbCommand(updateInformacoesFiscaisQuery, conexao, transacao);
            command.Parameters.AddWithValue("@idProduto", idProduto);
            command.Parameters.AddWithValue("@origemDaMercadoria", informacoesFiscais.OrigemDaMercadoria ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@situacaoTributaria", informacoesFiscais.SituacaoTributaria ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@naturezaDaOperacao", informacoesFiscais.NaturezaDaOperacao ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ncm", informacoesFiscais.Ncm ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@aliquotaDeIcms", informacoesFiscais.AliquotaDeIcms);
            command.Parameters.AddWithValue("@reducaoDeCalculo", informacoesFiscais.ReducaoDeCalculo);
            command.ExecuteNonQuery();
        }
    }
}
