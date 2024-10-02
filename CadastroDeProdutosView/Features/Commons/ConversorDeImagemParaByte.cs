using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class ConversorDeImagemParaByte
    {
        public static byte[] ConversorDeValoresDeImagemParaByte(string caminhoDaImagem, int largura, int altura)
        {
            using var imagemOriginal = Image.FromFile(caminhoDaImagem);

            using var imagemRedimensionada = new Bitmap(largura, altura);
            using var g = Graphics.FromImage(imagemRedimensionada);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imagemOriginal, 0, 0, largura, altura);

            using var imgStream = new MemoryStream();
            imagemRedimensionada.Save(imgStream, ImageFormat.Png);
            return imgStream.ToArray();
        }
    }
}
