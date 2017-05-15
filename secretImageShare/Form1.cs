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
            label1.Text = "Kodlama";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //File Dialog classının değişkene aktarılması.
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg, *.bmp) | *.png; *.jpg; *.bmp";    //Filter ile seçilebilecek tipteki resimler seçiliyor.
            openDialog.InitialDirectory = @"D:\Git Repositories\Secret-Image-Sharing";        //İlk Açılacak Dosya Dizini

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
        }
    }
}
