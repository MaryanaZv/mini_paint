using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace Projekt2_Zvarych_Maryana_54558
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            SetSize();
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        Bitmap map = new Bitmap(100, 100);
        Pen p = new Pen(Color.Black, 1f);
        bool startPaint = false;
        Graphics g;
        int? initX = null;
        int? initY = null;
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        int zmienna;
        private bool buttonWasClicked = false;

        private void SetSize()
        {
            map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(map);
            g.Clear(Color.White);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            errorProvider1.Dispose();
            startPaint = true;
            if (drawSquare)
            {
                if (!int.TryParse(txt_ShapeSize.Text, out zmienna))
                {
                    errorProvider1.SetError(txt_ShapeSize, "ERROR: podaj rozmiar!");
                    return;
                }
                else if (!buttonWasClicked)
                {
                    errorProvider1.SetError(btn_KolorFigury, "ERROR: wybierz kolor!");
                    return;
                }
                SolidBrush sb = new SolidBrush(btn_KolorFigury.BackColor);
                g.FillRectangle(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                pictureBox1.Image = map;
                startPaint = false;
                drawSquare = false;
            }
            else if (drawRectangle)
            {
                if (!int.TryParse(txt_ShapeSize.Text, out zmienna))
                {
                    errorProvider1.SetError(txt_ShapeSize, "ERROR: podaj rozmiar!");
                    return;
                }
                if (!buttonWasClicked)
                {
                    errorProvider1.SetError(btn_KolorFigury, "ERROR: wybierz kolor!");
                    return;
                }
                SolidBrush sb = new SolidBrush(btn_KolorFigury.BackColor);
                g.FillRectangle(sb, e.X, e.Y, 2 * int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                pictureBox1.Image = map;
                startPaint = false;
                drawRectangle = false;
            }
            else if (drawCircle)
            {
                if (!int.TryParse(txt_ShapeSize.Text, out zmienna))
                {
                    errorProvider1.SetError(txt_ShapeSize, "ERROR: podaj rozmiar!");
                    return;
                }
                if (!buttonWasClicked)
                {
                    errorProvider1.SetError(btn_KolorFigury, "ERROR: wybierz kolor!");
                    return;
                }
                SolidBrush sb = new SolidBrush(btn_KolorFigury.BackColor);
                g.FillEllipse(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                pictureBox1.Image = map;
                startPaint = false;
                drawCircle = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint)
            {
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                pictureBox1.Image = map;
                initX = e.X;
                initY = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }

        private void btn_PenColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p.Color = colorDialog1.Color;
                btn_PenColor.BackColor = colorDialog1.Color;
            }
        }

        private void btn_DrawSquare_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void btn_DrawRectangle_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void btn_DrawCircle_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to Exit?", "Exit",MessageBoxButtons.YesNo,
                MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void formatJpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG) | *.jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    MessageBox.Show("ERROR: nie udało się otworzyć pliku w pamięci " +
                        "zewnętrznej lub wystąpił błąd przy wpisywaniu danych");
                }
            }
            MessageBox.Show("Image successfully saved!");
        }

        private void formatBmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "BMP(*.BMP)  | *.bmp";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                else
                {
                    MessageBox.Show("ERROR: nie udało się otworzyć pliku w pamięci " +
                        "zewnętrznej lub wystąpił błąd przy wpisywaniu danych");
                }
            }
            MessageBox.Show("Image successfully saved!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Color = ((Button)sender).BackColor;
        }

        private void cb_PenSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_PenSize.Text == "1")
                p.Width = 1;
            else if (cb_PenSize.Text == "2")
                p.Width = 2;
            else if (cb_PenSize.Text == "3")
                p.Width = 3;
            else if (cb_PenSize.Text == "4")
                p.Width = 4;
            else if (cb_PenSize.Text == "5")
                    p.Width = 5;
                else if (cb_PenSize.Text == "6")
                    p.Width = 6;
                else if (cb_PenSize.Text == "7")
                    p.Width = 7;
                else if (cb_PenSize.Text == "8")
                    p.Width = 8;
                else if (cb_PenSize.Text == "9")
                    p.Width = 9;
                else if (cb_PenSize.Text == "10")
                p.Width = 10;
        }

        private void btn_KolorFigury_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btn_KolorFigury.BackColor = c.Color;
            }
        }

        private void wyczyśćWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            p.Width = trackBar1.Value;
        }

        private void formatGifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "GIF(*.GIF)  | *.gif";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                }
                else
                {
                    MessageBox.Show("ERROR: nie udało się otworzyć pliku w pamięci " +
                        "zewnętrznej lub wystąpił błąd przy wpisywaniu danych");
                }
            }
            MessageBox.Show("Image successfully saved!");
        }

        private void formatPngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG(*.PNG)  | *.png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    MessageBox.Show("ERROR: nie udało się otworzyć pliku w pamięci " +
                        "zewnętrznej lub wystąpił błąd przy wpisywaniu danych");
                }
            }
            MessageBox.Show("Image successfully saved!");
        }
    }
}