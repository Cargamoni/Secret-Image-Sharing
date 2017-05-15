using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Gizli Resim Paylaşma";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //File Dialog classının değişkene aktarılması.
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
            //Seçilen Resmin Bitmap olarak ayarlanması
            Bitmap img = new Bitmap(textBoxDizin.Text);

            for(int i = 0; i <= img.Width -1; i++)
            {
                for(int j = 0; j<= img.Height -1; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if(i < 1 && j < 10)
                    {
                        Console.Write("R = [" + i + "][" + j + "] = " + pixel.R);
                        Console.Write(" G = [" + i + "][" + j + "] = " + pixel.G);
                        Console.Write(" B = [" + i + "][" + j + "] = " + pixel.B + "\n");
                    }
                    //Bir resmin RGB değerlerinin toplamı 3e bölümü gri olur
                    img.SetPixel(i, j, Color.FromArgb((pixel.R + pixel.G + pixel.B) / 3,(pixel.R + pixel.G + pixel.B) / 3,(pixel.R + pixel.G + pixel.B) / 3));
                }
            }

            //Gri tonlamalı resmin kaydedilmesi
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            saveFile.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing\";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFile.FileName.ToString();
                pictureBox2.ImageLocation = textBox2.Text;
                img.Save(textBox2.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
