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

        public Bitmap encodeImage(Image ortenResim, Image gizliResim)
        {
            Bitmap orten = new Bitmap(ortenResim);
            Bitmap gizli = new Bitmap(gizliResim);
            Color ortenResimRenk = new Color();
            Color gizliResimRenk = new Color();
            Form1 degisken = new Form1();

            byte[] gizliBit;
            byte[] asBitler;
            byte[] kirmiziBit;
            byte[] yesilBit;
            byte[] maviBit;

            byte yeniAsBit = 0;
            byte yeniKirmiziBit = 0;
            byte yeniYesilBit = 0;
            byte yeniMaviBit = 0;

            for (int i = 0; i <= orten.Width - 1; i++)
            {
                for (int j = 0; j <= orten.Height - 1; j++)
                {
                    gizliResimRenk = gizli.GetPixel(i, j);
                    gizliBit = this.resimBiti((byte)gizliResimRenk.R);

                    ortenResimRenk = orten.GetPixel(i, j);
                    asBitler = this.resimBiti((byte)ortenResimRenk.A);
                    kirmiziBit = this.resimBiti((byte)ortenResimRenk.R);
                    yesilBit = this.resimBiti((byte)ortenResimRenk.G);
                    maviBit = this.resimBiti((byte)ortenResimRenk.B);

                    asBitler[6] = gizliBit[0];
                    asBitler[7] = gizliBit[1];

                    kirmiziBit[6] = gizliBit[2];
                    kirmiziBit[7] = gizliBit[3];

                    yesilBit[6] = gizliBit[4];
                    yesilBit[7] = gizliBit[5];

                    maviBit[6] = gizliBit[6];
                    maviBit[7] = gizliBit[7];

                    yeniAsBit = this.resimByte(asBitler);
                    yeniKirmiziBit = this.resimByte(kirmiziBit);
                    yeniYesilBit = this.resimByte(yesilBit);
                    yeniMaviBit = this.resimByte(maviBit);
                    degisken.progressBarIncrease(1);
                    //Console.WriteLine("As Bit = " + ortenResimRenk.A + " Kırmızı Bit = " + ortenResimRenk.R + " Yeşil Bit = " + ortenResimRenk.G + " Mavi Bit = " + ortenResimRenk.B + "\n");
                    ortenResimRenk = Color.FromArgb(yeniAsBit, yeniKirmiziBit, yeniYesilBit, yeniMaviBit);
                    orten.SetPixel(i, j, ortenResimRenk);
                    //Console.WriteLine("Güncellenen : As Bit = " + ortenResimRenk.A + " Kırmızı Bit = " + ortenResimRenk.R + " Yeşil Bit = " + ortenResimRenk.G + " Mavi Bit = " + ortenResimRenk.B + "\n");
                }
            }
            return orten;
        }
    }
}
