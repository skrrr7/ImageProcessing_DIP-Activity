using WebCamLib;
using ImageProcess2;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        Bitmap imageB, imageA, colorgreen, resultImage;
        Device[] device;
        public Form1()
        {
            InitializeComponent();
        }

        private void pixelCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            pictureBox2.Image = processed;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int ave;
            for (int x = 0; x < loaded.Width; x++)
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    ave = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    Color gray = Color.FromArgb(ave, ave, ave);
                    processed.SetPixel(x, y, gray);
                }
            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int ave;
            for (int x = 0; x < loaded.Width; x++)
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color inv = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    processed.SetPixel(x, y, inv);
                }
            pictureBox2.Image = processed;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color sep = Color.FromArgb(
                        Math.Min(255, (int)((0.393 * pixel.R) + (0.769 * pixel.G) + (0.189 * pixel.B))),
                        Math.Min(255, (int)((0.349 * pixel.R) + (0.686 * pixel.G) + (0.168 * pixel.B))),
                        Math.Min(255, (int)((0.272 * pixel.R) + (0.534 * pixel.G) + (0.131 * pixel.B)))
                    );
                    processed.SetPixel(x, y, sep);
                }
            }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Hist(ref loaded, ref processed);
            pictureBox2.Image = processed;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = DeviceManager.GetAllDevices();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            resultImage = new Bitmap(imageB.Width, imageB.Height);
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 150;
            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);
                    if (pixel.G > threshold && pixel.R < threshold / 2 && pixel.B < threshold / 2)
                        resultImage.SetPixel(x, y, backpixel);
                    else
                        resultImage.SetPixel(x, y, pixel);
                }
            }
            pictureBox5.Image = resultImage;
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox3.Image = imageB;
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox4.Image = imageA;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            resultImage.Save(saveFileDialog2.FileName);
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacian(loaded);

            pictureBox2.Image = loaded;
        }

        private void dIPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.Smooth(loaded, 1);

            pictureBox2.Image = loaded;
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.GaussianBlur(loaded, 4);

            pictureBox2.Image = loaded;
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.Sharpen(loaded, 11);

            pictureBox2.Image = loaded;
        }

        private void meanRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.GaussianBlur(loaded, 9);

            pictureBox2.Image = loaded;
        }

        private void embossingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacian(loaded);

            pictureBox2.Image = loaded;
        }

        private void edgeDeteckQuickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EdgeDetectQuick(loaded);

            pictureBox2.Image = loaded;
        }

        private void allDirectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacianAllDir(loaded);

            pictureBox2.Image = loaded;
        }

        private void lossyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacianLossy(loaded);

            pictureBox2.Image = loaded;
        }

        private void verticalOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacianVertical(loaded);

            pictureBox2.Image = loaded;
        }

        private void horzVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool succes = BitmapFilter.EmbossLaplacianHorzVert(loaded);

            pictureBox2.Image = loaded;
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            device[0].ShowWindow(pictureBox1);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            device[0].Stop();
        }

        private void greyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data;
            Image bmap;
            device[0].Sendmessage();
            data = Clipboard.GetDataObject();
            bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));
            Bitmap b = new Bitmap(bmap);
            BitmapFilter.GrayScale(b);
            pictureBox2.Image = b;
        }

        private void mirrorVertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < (int)loaded.Height / 2; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, loaded.Height - y - 1, pixel);
                    pixel = loaded.GetPixel(x, loaded.Height - y - 1);
                    processed.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void mirrorHorizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width / 2; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(loaded.Width - x - 1, y, pixel);
                    pixel = loaded.GetPixel(loaded.Width - x - 1, y);
                    processed.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processed;
        }
    }
}
