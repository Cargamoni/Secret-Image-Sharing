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
using AForge.Imaging.Filters;

namespace secretImageShare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Gizli Resim Paylaşma";
            for (int i = 1; i <= 7; i++)
                comboBox1.Items.Add(i);
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;
            label1.Enabled = false;
            checkBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg, *.bmp) | *.png; *.jpg; *.bmp";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDizin.Text = openDialog.FileName.ToString();         //Seçilen dosyanın dizininin textbox içerisine aktarılması
                pictureBox1.ImageLocation = textBoxDizin.Text;              //PictureBox üzerinde seçilen resmin gösterilmesi.
            }
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg, *.bmp) | *.png; *.jpg; *.bmp";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openDialog.FileName.ToString();         //Seçilen dosyanın dizininin textbox içerisine aktarılması
                pictureBox2.ImageLocation = textBox2.Text;              //PictureBox üzerinde seçilen resmin gösterilmesi.
            }
        }

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

        private byte resimByte(byte[] bitler)
        {
            String olusanBit = "";
            for (int i = 0; i < 8; i++)
                olusanBit += bitler[i];
            byte yeniPixel = Convert.ToByte(olusanBit, 2);
            int donusturulenPixel = (int)yeniPixel;
            return (byte)donusturulenPixel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap orten = new Bitmap(textBoxDizin.Text);
            Bitmap gizli = new Bitmap(textBox2.Text);
            for (int i = 0; i <= gizli.Width - 1; i++)
                for (int j = 0; j <= gizli.Height - 1; j++)
                {
                    Color pixel = gizli.GetPixel(i, j);
                    //Bir resmin RGB değerlerinin toplamı 3e bölümü gri olur
                    gizli.SetPixel(i, j, Color.FromArgb((pixel.R + pixel.G + pixel.B) / 3, (pixel.R + pixel.G + pixel.B) / 3, (pixel.R + pixel.G + pixel.B) / 3));
                }

            pictureBox2.Image = gizli;
            Color ortenResimRenk = new Color();
            Color gizliResimRenk = new Color();

            byte[] gizliBit;
            byte[] kirmiziBit;
            byte[] yesilBit;
            byte[] maviBit;

            byte yeniGizliBit = 0;
            byte yeniKirmiziBit = 0;
            byte yeniYesilBit = 0;
            byte yeniMaviBit = 0;

            for(int i = 0; i <= orten.Height -1; i++)
            {
                for(int j = 0; j <= orten.Width -1; j++)
                {
                    ortenResimRenk = orten.GetPixel(i, j);        
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            { 
                label1.Enabled = false;
                comboBox1.Enabled = false;                
            }
            else
            {
                label1.Enabled = true;
                comboBox1.Enabled = true;
            }
        }
    }
}
