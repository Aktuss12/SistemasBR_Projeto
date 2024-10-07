using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class SituacaoTributariaView
    {
        public enum SituacaoTributaria
        {
            [Description("00 — Tributada integralmente")]
            TributadaIntegralmente,

            [Description("10 — Tributada e com cobrança do ICMS por substituição tributária")]
            TributadaEComCobrancaDoIcmsPorSubstituicaoTributaria,

            [Description("20 — Com redução de base de cálculo")]
            ComReducaoDeBaseDeCalculo,

            [Description("30 — Isenta ou não tributada e com cobrança do ICMS por substituição tributária")]
            IsentaOuNaoTributadaEComCobrancaDoIcmsPorSubstituicaoTributaria,
            [Description("40 - Isenta")] Isenta,
            [Description("41 - Nao é tributada")] NaoETributada,
            [Description("50 - Suspensão")] Suspencao,
            [Description("51 - Diferenciamento")] Diferenciamento,

            [Description("60 — ICMS cobrado anteriormente por substituição tributária")]
            IcmsCobradoAnteriormentePorSubstituicaoTributaria,

            [Description("70 — Com redução de base de cálculo e cobrança do ICMS por substituição tributária")]
            ComReducaoDeBaseDeCalculoECobrancaDoIcmsPorSubstituicaoTributaria,
            [Description("90 - Outras")] Outras
        }
    }
}