using FirebirdSql.Data.FirebirdClient;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class ControladorDeStatusDoProduto
    {
        public static void DesativarProduto(string connectionString, int idProduto)
        {
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            const string updateProductQuery = "UPDATE PRODUTO SET ativo = 0 WHERE idProduto = @idProduto";
            using var command = new FbCommand(updateProductQuery, conexao);
            command.Parameters.AddWithValue("@idProduto", idProduto);
            command.ExecuteNonQuery();
        }

        public static void ReativarProduto(string connectionString, int idProduto)
        {
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            const string updateProductQuery = "UPDATE PRODUTO SET ativo = 1 WHERE idProduto = @idProduto";
            using var command = new FbCommand(updateProductQuery, conexao);
            command.Parameters.AddWithValue("@idProduto", idProduto);
            command.ExecuteNonQuery();
        }
    }
}
