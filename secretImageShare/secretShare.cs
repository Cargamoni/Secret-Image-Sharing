using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace secretImageShare
{
    class secretShare
    {
        public byte[] resimBiti(byte tekPixel)
        {
            int pixel = 0;
            pixel = (int)tekPixel;
            BitArray bitler = new BitArray(new byte[] { (byte)pixel });
            bool[] boolDizi = new bool[bitler.Count];
            bitler.CopyTo(boolDizi, 0);
            byte[] bitDizisi = boolDizi.Select(bit => (byte)(bit ? 1 : 0)).ToArray();
            Array.Reverse(bitDizisi);
            return bitDizisi;
        }

        public byte resimByte(byte[] bitler)
        {
            String olusanBit = "";
            for (int i = 0; i < 8; i++)
                olusanBit += bitler[i];
            byte yeniPixel = Convert.ToByte(olusanBit, 2);
            int donusturulenPixel = (int)yeniPixel;
            return (byte)donusturulenPixel;
        }

        public Bitmap geleniGriYap(Image gelenGizli)
        {
            Bitmap gizli = new Bitmap(gelenGizli);

            for (int i = 0; i <= gizli.Width - 1; i++)
                for (int j = 0; j <= gizli.Height - 1; j++)
                {
                    Color pixel = gizli.GetPixel(i, j);
                    //Bir resmin RGB değerlerinin toplamı 3e bölümü gri olur
                    gizli.SetPixel(i, j, Color.FromArgb((pixel.R + pixel.G + pixel.B) / 3, (pixel.R + pixel.G + pixel.B) / 3, (pixel.R + pixel.G + pixel.B) / 3));
                }
            return gizli;
        }
    }
}
