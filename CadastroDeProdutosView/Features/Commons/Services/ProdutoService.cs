using System.Collections.Generic;

namespace CadastroDeProdutosView.Features.Commons.Services
{
    public static class ProdutoService
    {
        public static List<Produto> ObterProdutosAtualizados()
        {
            var conexao = new ConexaoProdutosFirebird();
            return conexao.ListarProdutos(incluirDesativados: false);
        }
    }
}
