using System;
using System.Linq;
using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons.Services
{ 
    public static class ManipuladorDeEnumService
    {
        private static T? ObterAtributoDoTipo<T>(this Enum valorEnum) where T : Attribute
        {
            var type = valorEnum.GetType();
            var memInfo = type.GetMember(valorEnum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static string ObterDescricao(this Enum valorEnum) =>
             valorEnum.ObterAtributoDoTipo<System.ComponentModel.DescriptionAttribute>()!.Description;

        public static void PreencherLookUpEditComOValorDoEnum<T>(this LookUpEdit lookUpEdit) where T : Enum
        {
            var dicionarioDeEnums = Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToDictionary(x => Convert.ToInt32(x), x => x.ObterDescricao());

            lookUpEdit.Properties.DataSource = dicionarioDeEnums;
            lookUpEdit.Properties.ValueMember = "Key";
            lookUpEdit.Properties.DisplayMember = "Value";
        }
    }
}
