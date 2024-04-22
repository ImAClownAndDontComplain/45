using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace LW4_5Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private bool isMouse = false;
        private class ArrPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrPoints(int size)
            {
                if (size <= 0) size = 2;
                points = new Point[size];
            }
            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index++] = new Point(x, y);
            }
            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        private ArrPoints arrPoints = new ArrPoints(2);
        Bitmap bmp;
        Bitmap bmp28;
        Graphics g;
        Pen pen = new Pen(Color.White, 15f);
        Process app;
        private void SetSize()
        {
            Rectangle rec = new Rectangle(pictureBox1.Location, pictureBox1.Size);
            bmp = new Bitmap(280, 280);
            g = Graphics.FromImage(bmp);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) { return; }
            arrPoints.SetPoint(e.X, e.Y);
            if (arrPoints.GetCountPoints() >= 2)
            {
                g.DrawLines(pen, arrPoints.GetPoints());
                pictureBox1.Image = bmp;
                arrPoints.SetPoint(e.X, e.Y);
            }
        }


        private void button1_Click(object sender, EventArgs e)//кнопка очищения
        {
                g.Clear(pictureBox1.BackColor);
                pictureBox1.Image = bmp;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_recognition_Click(object sender, EventArgs e)// кнопка начала распознавания
        {
            pictureBox1.Image.Save(@"D:/circus/45/digit.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Bitmap image = new Bitmap("D:/circus/45/digit.Jpeg");

            Color color = Color.Black;
                bmp28 = new Bitmap(28, 28);
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 28; j++)
                    {
                        int sum = 0;
                        for (int ii = 0; ii < 10; ii++)
                        {
                            for (int jj = 0; jj < 10; jj++)
                            {
                                byte s = image.GetPixel(i * 10 + ii, j * 10 + jj).R;
                                sum += s;
                            }
                        }
                        if (sum != 0)
                        {
                            Console.WriteLine("rr");
                        }
                        int color_average = sum * 3 / 100;
                        if (color_average > 255) color_average = 255;
                        color = Color.FromArgb(color_average, color_average, color_average);
                        bmp28.SetPixel(i, j, color);
                    }
                }
                bmp28.Save(@"D:/circus/45/digit28.bmp");
        }

 
    }
}
