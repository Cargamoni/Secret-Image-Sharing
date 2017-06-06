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
            openDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png ";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
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
            openDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png ";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openDialog.FileName.ToString();         //Seçilen dosyanın dizininin textbox içerisine aktarılması
                pictureBox2.ImageLocation = textBox2.Text;              //PictureBox üzerinde seçilen resmin gösterilmesi.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            secretShare degisken = new secretShare();
            Bitmap orten = new Bitmap(pictureBox1.Image);
            Bitmap gizli = degisken.geleniGriYap(pictureBox2.Image);
            pictureBox2.Image = gizli;
            
            pictureBox3.Image = degisken.encodeImage(orten, gizli);

            SaveFileDialog openDialog = new SaveFileDialog();
            openDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png ";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image.Save(openDialog.FileName);
            }
            System.GC.SuppressFinalize(degisken);
            System.GC.SuppressFinalize(gizli);
            System.GC.SuppressFinalize(orten);
            System.GC.SuppressFinalize(openDialog);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png ";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openDialog.FileName.ToString();         //Seçilen dosyanın dizininin textbox içerisine aktarılması
                pictureBox4.ImageLocation = textBox1.Text;              //PictureBox üzerinde seçilen resmin gösterilmesi.
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secretShare degisken = new secretShare();
            Bitmap sifrelenmisResim = (Bitmap)pictureBox4.Image;
            Bitmap desifrelenmisResim = new Bitmap(sifrelenmisResim.Width, sifrelenmisResim.Height);

            Color desifrelemePixelRengi = new Color();

            byte[] desifrelemeBitleri = new byte[8];
            byte[] asBitler;
            byte[] kirmiziBit;
            byte[] yesilBit;
            byte[] maviBit;

            byte yeniGri = 0;
            for (int i = 0; i <= sifrelenmisResim.Width - 1; i++)
            {
                for (int j = 0; j <= sifrelenmisResim.Height - 1; j++)
                {
                    desifrelemePixelRengi = sifrelenmisResim.GetPixel(i, j);

                    asBitler = degisken.resimBiti((byte)desifrelemePixelRengi.A);
                    kirmiziBit = degisken.resimBiti((byte)desifrelemePixelRengi.R);
                    yesilBit = degisken.resimBiti((byte)desifrelemePixelRengi.G);
                    maviBit = degisken.resimBiti((byte)desifrelemePixelRengi.B);

                    desifrelemeBitleri[0] = asBitler[6];
                    desifrelemeBitleri[1] = asBitler[7];
                    desifrelemeBitleri[2] = kirmiziBit[6];
                    desifrelemeBitleri[3] = kirmiziBit[7];
                    desifrelemeBitleri[4] = yesilBit[6];
                    desifrelemeBitleri[5] = yesilBit[7];
                    desifrelemeBitleri[6] = maviBit[6];
                    desifrelemeBitleri[7] = maviBit[7];

                    yeniGri = degisken.resimByte(desifrelemeBitleri);

                    desifrelemePixelRengi = Color.FromArgb(yeniGri, yeniGri, yeniGri);
                    desifrelenmisResim.SetPixel(i, j, desifrelemePixelRengi);
                }
            }
            pictureBox4.Image = desifrelenmisResim;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png ";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            saveDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";        //İlk Açılacak Dosya Dizini
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image.Save(saveDialog.FileName);
            }
            System.GC.SuppressFinalize(degisken);
            System.GC.SuppressFinalize(sifrelenmisResim);
            System.GC.SuppressFinalize(desifrelenmisResim);
            System.GC.SuppressFinalize(saveDialog);
        }
    }
}