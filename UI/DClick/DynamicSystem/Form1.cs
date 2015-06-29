using System;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicSystem
{
    public partial class Form1 : Form
    {
        double _xmin = -1, _xmax = 1, _ymin = -1, _ymax = 1;
        int _width, _height;
        int countOfPoints = 1;
        int level = 2;
        double rplus;
        double rminus;
        Bitmap _bitmap;
        int n = 2;
        double alfa = 1.7;
        Bitmap _curBitmap;
        double rFixed = 0;
        double gFixed = 0;
        bool dinamic = false;
        public Form1()
        {
            InitializeComponent();
        }
        private double gF(double gg, double rr)
        {
            return (double)(Math.Pow(n, 2 * alfa - 3) * Math.Pow((((rr + 1) * (rr + 1) - gg) / ((rr + 1) * (rr + 1) - gg / n)), 2) * gg);
        }
        private double rF(double gg, double rr)
        {
            return (double)(Math.Pow(n, alfa - 1) * ((((rr + 1) * (rr + 1) - gg) / ((rr + 1) * (rr + 1) - gg / n)) * (rr + 1) - 1));
        }

        private double C0F(double c0, double c1, double c2)
        {
            return (c1 - c0) * (c1 - c0) + (c0 * c2 - c1 * c1) / n;
        }
        private double C1F(double c0, double c1, double c2)
        {
            return ((c1 - c0) * (c2 - c1) + (c0 * c2 - c1 * c1) / n) * Math.Pow(n, alfa - 1);
        }
        private double C2F(double c0, double c1, double c2)
        {
            return ((c2 - c1) * (c2 - c1) + (c0 * c2 - c1 * c1) / n) * Math.Pow(n, alfa - 1) * Math.Pow(n, alfa - 1);
        }

        private double C0FF(double c0, double c1, double c2)
        {
            return (c0 - c1 * Math.Pow(n, 1 - alfa)) * (c0 - c1 * Math.Pow(n, 1 - alfa)) + n * Math.Pow(n, 2 * (1 - alfa)) * (c0 * c2 - c1 * c1);
        }
        private double C1FF(double c0, double c1, double c2)
        {
            return Math.Pow(n, 1 - alfa) * (c0 - c1 * Math.Pow(n, 1 - alfa)) * (c1 - c2 * Math.Pow(n, 1 - alfa)) + n * Math.Pow(n, 2 * (1 - alfa)) * (c0 * c2 - c1 * c1);
        }
        private double C2FF(double c0, double c1, double c2)
        {
            return Math.Pow(n, 2 * (1 - alfa)) * (c1 - c2 * Math.Pow(n, 1 - alfa)) * (c1 - c2 * Math.Pow(n, 1 - alfa)) + n * Math.Pow(n, 2 * (1 - alfa)) * (c0 * c2 - c1 * c1);
        }
        private void чToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 4;
            pictureBox1.Cursor = Cursors.Arrow;

        }
        private void Pereschet()
        {
            if ((pictureBox1.Width - panel2.Width) / (_xmax - _xmin) < (pictureBox1.Height) / (_ymax - _ymin))
            {
                _width = pictureBox1.Width - panel2.Width;
                _height = (int)((pictureBox1.Width - panel2.Width) * (_ymax - _ymin) / (_xmax - _xmin));
            }
            else
            {
                _width = (int)((pictureBox1.Height) * (_xmax - _xmin) / (_ymax - _ymin));
                _height = pictureBox1.Height;
            }
        }
        private void DrawGraphics()
        {
            Pereschet();
            _bitmap = new Bitmap(_width, _height);
            rplus = (double)((Math.Sqrt(n) - Math.Pow(n, alfa - 1)) / (1 - Math.Sqrt(n)));
            rminus = (double)((-Math.Sqrt(n) - Math.Pow(n, alfa - 1)) / (1 + Math.Sqrt(n)));
            Graphics gr = Graphics.FromImage(_bitmap);
            gr.FillRectangle(Brushes.Beige, new Rectangle(0, 0, _width, _height));
            for (int i = 0; i < _width; i++)
                for (int j = _height - 1; j >= 0; j--)
                {
                    double rr = _xmin + i * (_xmax - _xmin) / _width;
                    double gg = _ymin + (_height - j) * (_ymax - _ymin) / _height;
                    double ctemp = Math.Pow(1 - (rr * rr + gg * gg), 0.5);
                    if (rr * rr + gg * gg > 1)
                    {
                        _bitmap.SetPixel(i, j, Color.Violet);

                    }
                    else if ((rr * rr) / (ctemp * ctemp) - gg / ctemp < 0)
                    {
                        double c0 = ctemp;
                        double c1 = rr;
                        double c2 = gg;
                        for (int k = 0; k < 1000; k++)
                        {
                            if (c2 > 0.99999 && c1 < 0 && c1 > -0.000001)
                            {
                                _bitmap.SetPixel(i, j, Color.Yellow);
                                break;
                            }
                            if (c2 > 0.99999 && c1 >= 0 && c1 < 0.000001)
                            {
                                _bitmap.SetPixel(i, j, Color.Green);
                                break;
                            }
                            double c01 = C0F(c0, c1, c2);
                            double c11 = C1F(c0, c1, c2);
                            double c21 = C2F(c0, c1, c2);
                            if (c01 < 0)
                            {
                                c01 = -c01;
                                c11 = -c11;
                                c21 = -c21;
                            }
                            c0 = c01 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                            c1 = c11 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                            c2 = c21 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                        }

                        //_bitmap.SetPixel(i, j, Color.Gray);
                    }
                    else
                    {
                        double c0 = ctemp;
                        double c1 = rr;
                        double c2 = gg;
                        for (int k = 0; k < 1000; k++)
                        {
                            if (c2 > 0.99999 && c1 < 0 && c1 > -0.000001)
                            {
                                _bitmap.SetPixel(i, j, Color.Red);
                                break;
                            }
                            if (c2 > 0.99999 && c1 >= 0 && c1 < 0.000001)
                            {
                                _bitmap.SetPixel(i, j, Color.Blue);
                                break;
                            }
                            double c01 = C0F(c0, c1, c2);
                            double c11 = C1F(c0, c1, c2);
                            double c21 = C2F(c0, c1, c2);
                            if (c01 < 0)
                            {
                                c01 = -c01;
                                c11 = -c11;
                                c21 = -c21;
                            }
                            c0 = c01 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                            c1 = c11 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                            c2 = c21 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                        }
                    }

                }



            if (checkBox1.Checked)
            {
                int ifix = 0, jfix = 0;
                double norma = 1000;
                for (int i = 0; i < _width; i++)
                    for (int j = _height - 1; j >= 0; j--)
                    {
                        double rr = _xmin + i * (_xmax - _xmin) / _width;
                        double gg = _ymin + (_height - j) * (_ymax - _ymin) / _height;
                        double ctemp = Math.Pow(1 - (rr * rr + gg * gg), 0.5);
                        if (rr * rr + gg * gg <= 1)
                        {
                            double c0 = ctemp;
                            double c1 = rr;
                            double c2 = gg;
                            if (c0 < 0)
                            {
                                c0 = -c0;
                                c1 = -c1;
                                c2 = -c2;
                            }
                            double cc0 = c0 / Math.Pow((c0 * c0 + c1 * c1 + c2 * c2), 0.5);
                            double cc1 = c1 / Math.Pow((c0 * c0 + c1 * c1 + c2 * c2), 0.5);
                            double cc2 = c2 / Math.Pow((c0 * c0 + c1 * c1 + c2 * c2), 0.5);
                            for (int k = 0; k < (int)numericUpDown1.Value; k++)
                            {
                                double c01 = C0F(c0, c1, c2);
                                double c11 = C1F(c0, c1, c2);
                                double c21 = C2F(c0, c1, c2);
                                if (c01 < 0)
                                {
                                    c01 = -c01;
                                    c11 = -c11;
                                    c21 = -c21;
                                }
                                c0 = c01 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                                c1 = c11 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                                c2 = c21 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
                            }
                            if (norma > Math.Sqrt((c0 - cc0) * (c0 - cc0) + (c1 - cc1) * (c1 - cc1) + (c2 - cc2) * (c2 - cc2)))
                            {
                                norma = Math.Sqrt((c0 - cc0) * (c0 - cc0) + (c1 - cc1) * (c1 - cc1) + (c2 - cc2) * (c2 - cc2));
                                ifix = i;
                                jfix = j;
                            }


                        }
                    }
                gr.DrawRectangle(Pens.Aquamarine, ifix - 2, jfix - 2, 5, 5);

            }



            double rrr = (double)((Math.Sqrt(n) - Math.Pow(n, alfa - 1)) / (1 - Math.Sqrt(n)));
            double ggr = (double)(rrr * Math.Pow((1 + rrr), 2) / (1 + rrr + Math.Pow(n, -0.5)));
            double c02;
            double c12 = -rrr;
            double c22 = rrr * rrr - ggr;
            c02 = 1;
            double temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(Pens.White, pt.X - 3, pt.Y - 3, 8, 8);
                gr.DrawLine(Pens.White, pt.X - 3, pt.Y + 1, pt.X + 4, pt.Y + 1);
                gr.DrawLine(Pens.White, pt.X + 1, pt.Y - 3, pt.X + 1, pt.Y + 4);


            }
            rrr = (double)((-Math.Sqrt(n) - Math.Pow(n, alfa - 1)) / (1 + Math.Sqrt(n)));
            ggr = (double)(rrr * Math.Pow((1 + rrr), 2) / (1 + rrr - Math.Pow(n, -0.5)));
            c12 = -rrr;
            c22 = rrr * rrr - ggr;
            c02 = 1;
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(Pens.White, pt.X - 3, pt.Y - 3, 8, 8);
                gr.DrawLine(Pens.White, pt.X - 3, pt.Y + 1, pt.X + 4, pt.Y + 1);
            }
            c12 = 1;
            c22 = 1;
            c02 = 1;
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            for (int h = 0; h < countOfPoints; h++)
            {
                if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
                {
                    Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                    gr = Graphics.FromImage(_bitmap);
                    gr.FillEllipse(Brushes.White, pt.X - 2, pt.Y - 2, 5, 5);
                }
                double c011 = C0FF(c02, c12, c22);
                double c111 = C1FF(c02, c12, c22);
                double c211 = C2FF(c02, c12, c22);
                c02 = c011;
                c12 = c111;
                c22 = c211;
                if (c02 < 0)
                {
                    c02 = -c02;
                    c12 = -c12;
                    c22 = -c22;
                }
                temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
                c12 = c12 / temp;
                c22 = c22 / temp;
                c02 = c02 / temp;

            }
            //Cycles
            double lambda = Math.Pow(n, alfa - 1);
            double t1 = Math.Sqrt(n) / lambda + lambda / Math.Sqrt(n);
            double t2 = Math.Sqrt(n) * lambda + 1 / (lambda * Math.Sqrt(n));
            double a1 = Math.Pow(t1 / t2, 0.5);
            double xxt = t1 - (Math.Sqrt(n) + 1 / Math.Sqrt(n)) * a1;
            double xx = Math.Pow(xxt, 0.5) / (Math.Sqrt(lambda) + 1 / Math.Sqrt(lambda));
            double x_ = xx / a1;
            double y_ = xx;
            x_ = x_ / Math.Pow(n, 0.25);
            y_ = y_ / Math.Pow(n, 0.25);
            double u = (x_ + y_) / (-x_ * y_ - 1);
            c02 = x_ * u + 1;
            c22 = y_ * u + 1;
            c12 = 1;
            if (c02 < 0)
            {
                c02 = -c02;
                c12 = -c12;
                c22 = -c22;
            }
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(new Pen(Color.FromArgb(255, 255, 0), 2), pt.X - 2, pt.Y - 2, 5, 5);
            }




            t1 = Math.Sqrt(n) / lambda + lambda / Math.Sqrt(n);
            t2 = Math.Sqrt(n) * lambda + 1 / (lambda * Math.Sqrt(n));
            a1 = Math.Pow(t1 / t2, 0.5);
            xxt = t1 + (Math.Sqrt(n) + 1 / Math.Sqrt(n)) * a1;
            xx = Math.Pow(xxt, 0.5) / (Math.Sqrt(lambda) + 1 / Math.Sqrt(lambda));
            x_ = xx / a1;
            y_ = -xx;
            x_ = x_ / Math.Pow(n, 0.25);
            y_ = y_ / Math.Pow(n, 0.25);
            u = (x_ + y_) / (-x_ * y_ - 1);
            c02 = x_ * u + 1;
            c22 = y_ * u + 1;
            c12 = 1;
            if (c02 < 0)
            {
                c02 = -c02;
                c12 = -c12;
                c22 = -c22;
            }
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(new Pen(Color.FromArgb(255, 255, 0), 2), pt.X - 2, pt.Y - 2, 5, 5);
            }

            //тройные циклы
            x_ = 0.022039695689733657;
            y_ = 0.5022150800549515;
            x_ = x_ / Math.Pow(n, 0.25);
            y_ = y_ / Math.Pow(n, 0.25);
            u = (x_ + y_) / (-x_ * y_ - 1);
            c02 = x_ * u + 1;
            c22 = y_ * u + 1;
            c12 = 1;
            if (c02 < 0)
            {
                c02 = -c02;
                c12 = -c12;
                c22 = -c22;
            }
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(new Pen(Color.Honeydew, 2), pt.X - 2, pt.Y - 2, 5, 5);
            }

            x_ = 0.7639494001010361;
            y_ = -0.056083352671298506;
            x_ = x_ / Math.Pow(n, 0.25);
            y_ = y_ / Math.Pow(n, 0.25);
            u = (x_ + y_) / (-x_ * y_ - 1);
            c02 = x_ * u + 1;
            c22 = y_ * u + 1;
            c12 = 1;
            if (c02 < 0)
            {
                c02 = -c02;
                c12 = -c12;
                c22 = -c22;
            }
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(new Pen(Color.Honeydew, 2), pt.X - 2, pt.Y - 2, 5, 5);
            }

            x_ = 1.175230275817973;
            y_ = -0.957625322929873;
            x_ = x_ / Math.Pow(n, 0.25);
            y_ = y_ / Math.Pow(n, 0.25);
            u = (x_ + y_) / (-x_ * y_ - 1);
            c02 = x_ * u + 1;
            c22 = y_ * u + 1;
            c12 = 1;
            if (c02 < 0)
            {
                c02 = -c02;
                c12 = -c12;
                c22 = -c22;
            }
            temp = Math.Pow(c02 * c02 + c12 * c12 + c22 * c22, 0.5);
            c12 = c12 / temp;
            c22 = c22 / temp;
            c02 = c02 / temp;
            if (c12 > _xmin && c12 < _xmax && c22 > _ymin && c22 < _ymax)
            {
                Point pt = new Point((int)(_width * (c12 - _xmin) / (_xmax - _xmin)), _height - (int)(_height * (c22 - _ymin) / (_ymax - _ymin)));
                gr = Graphics.FromImage(_bitmap);
                gr.DrawEllipse(new Pen(Color.Honeydew, 2), pt.X - 2, pt.Y - 2, 5, 5);
            }

            pictureBox1.Image = _bitmap;
            _curBitmap = new Bitmap(_bitmap.Width, _bitmap.Height);
            gr = Graphics.FromImage(_curBitmap);
            gr.DrawImage(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
        }
        private void Status()
        {
            toolStripStatusLabel1.Text = "Xmin  " + _xmin.ToString();
            toolStripStatusLabel2.Text = "Ytop  " + _ymax.ToString();
            toolStripStatusLabel3.Text = "Xmax  " + _xmax.ToString();
            toolStripStatusLabel4.Text = "Ybottom  " + _ymin.ToString();
            textBox1.Text = _xmin.ToString();
            textBox2.Text = _ymax.ToString();
            textBox3.Text = _xmax.ToString();
            textBox4.Text = _ymin.ToString();
            textBox5.Text = rFixed.ToString();
            textBox6.Text = gFixed.ToString();
            textBox7.Text = n.ToString();
            textBox8.Text = alfa.ToString();
            textBox9.Text = countOfPoints.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = _bitmap;
            Status();
            DrawGraphics();
        }

        private void хToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 2;
            pictureBox1.Cursor = Cursors.Arrow;
        }

        private void хToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            level = 8;
            pictureBox1.Cursor = Cursors.Arrow;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int x = e.Location.X;
                int y = e.Location.Y;
                rFixed = _xmin + x * (_xmax - _xmin) / _width;
                gFixed = _ymin + (_height - y) * (_ymax - _ymin) / _height;
                RePaint();
            }
            else
            {
               
            }

        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 16;
            pictureBox1.Cursor = Cursors.Arrow;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmin = -1;
            _xmax = 1;
            _ymin = -1;
            _ymax = 1;
            Status();
            DrawGraphics();
            RePaint();
            pictureBox1.Refresh();

        }

        private void перерисовкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status();
            DrawGraphics();
            pictureBox1.Refresh();

        }

        private void zoneZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (level == 0)
            {
                Graphics gr = Graphics.FromImage(_bitmap);
                gr.DrawImage(_curBitmap, 0, 0, _bitmap.Width, _bitmap.Height);
                if (dinamic)
                {
                    if (rFixed >= _xmin && rFixed <= _xmax && gFixed >= _ymin && gFixed <= _ymax)
                    {
                        int xx = (int)(_width * (rFixed - _xmin) / (_xmax - _xmin));
                        int yy = _height - (int)(_height * (gFixed - _ymin) / (_ymax - _ymin));
                        gr.FillRectangle(Brushes.Black, xx - 1, yy - 1, 3, 3);

                    }
                }
                gr.DrawRectangle(Pens.White, e.Location.X - 10, e.Location.Y - 10, 21, 21);
                gr.DrawRectangle(Pens.White, e.Location.X - 9, e.Location.Y - 9, 19, 19);
                pictureBox1.Refresh();
            }
            else
            {
                Graphics gr = Graphics.FromImage(_bitmap);
                gr.DrawImage(_curBitmap, 0, 0, _bitmap.Width, _bitmap.Height);
                if (dinamic)
                {
                    if (rFixed >= _xmin && rFixed <= _xmax && gFixed >= _ymin && gFixed <= _ymax)
                    {
                        int xx = (int)(_width * (rFixed - _xmin) / (_xmax - _xmin));
                        int yy = _height - (int)(_height * (gFixed - _ymin) / (_ymax - _ymin));
                        gr.FillRectangle(Brushes.Black, xx - 1, yy - 1, 3, 3);

                    }
                }
                pictureBox1.Refresh();
            }
        }

        private void задатьПрямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void динамикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dinamic)
                dinamic = true;
            else
                dinamic = false;
            RePaint();
        }

        private void RePaint()
        {
            Graphics gr = Graphics.FromImage(_bitmap);
            gr.DrawImage(_curBitmap, 0, 0, _bitmap.Width, _bitmap.Height);
            if (dinamic)
            {
                if (rFixed >= _xmin && rFixed <= _xmax && gFixed >= _ymin && gFixed <= _ymax)
                {
                    int xx = (int)(_width * (rFixed - _xmin) / (_xmax - _xmin));
                    int yy = _height - (int)(_height * (gFixed - _ymin) / (_ymax - _ymin));
                    gr.FillRectangle(Brushes.Black, xx - 1, yy - 1, 3, 3);

                }
            }
            Status();
            pictureBox1.Refresh();
        }

        private void применитьОтображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double c1 = rFixed;
            double c2 = gFixed;
            double c0 = Math.Pow((1 - c1 * c1 - c2 * c2), 0.5);
            double c01 = C0F(c0, c1, c2);
            double c11 = C1F(c0, c1, c2);
            double c21 = C2F(c0, c1, c2);
            if (c01 < 0)
            {
                c01 = -c01;
                c11 = -c11;
                c21 = -c21;
            }
            c0 = c01 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
            c1 = c11 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
            c2 = c21 / Math.Pow((c01 * c01 + c11 * c11 + c21 * c21), 0.5);
            rFixed = c1;
            gFixed = c2;
            RePaint();
        }

        private void уменьшитьМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double temp = (_xmax - _xmin) / 2;
            _xmin -= temp;
            _xmax += temp;
            _ymin = 0;
            _ymax += (_ymax - _ymin);
            Status();
            DrawGraphics();
            RePaint();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rFixed = double.Parse(textBox5.Text);
            gFixed = double.Parse(textBox6.Text);
            _xmin = double.Parse(textBox1.Text);
            _ymax = double.Parse(textBox2.Text);
            _xmax = double.Parse(textBox3.Text);
            _ymin = double.Parse(textBox4.Text);
            n = int.Parse(textBox7.Text);
            alfa = double.Parse(textBox8.Text);
            countOfPoints = int.Parse(textBox9.Text);
            Status();
            DrawGraphics();
            RePaint();
            pictureBox1.Refresh();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                _bitmap.Save(f.FileName);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Visible = checkBox1.Checked;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (level != 0)
            {
                int x = e.Location.X;
                int y = e.Location.Y;
                double xx = _xmin + x * (_xmax - _xmin) / _width;
                double yy = _ymin + (_height - y) * (_ymax - _ymin) / _height;
                double xminx = Math.Max(_xmin, xx - (_xmax - _xmin) / (2 * level));
                double xmaxx = Math.Min(_xmax, xx + (_xmax - _xmin) / (2 * level));
                double yminy = Math.Max(_ymin, yy - (_ymax - _ymin) / (2 * level));
                double ymaxy = Math.Min(_ymax, yy + (_ymax - _ymin) / (2 * level));
                _xmin = xminx;
                _xmax = xmaxx;
                _ymin = yminy;
                _ymax = ymaxy;
                Status();
                DrawGraphics();
                pictureBox1.Refresh();
            }
            if (level == 0)
            {
                int x = e.Location.X;
                int y = e.Location.Y;
                double xx = _xmin + x * (_xmax - _xmin) / _width;
                double yy = _ymin + (_height - y) * (_ymax - _ymin) / _height;
                double xminx = Math.Max(_xmin, xx - (_xmax - _xmin) * (10.0 / _width));
                double xmaxx = Math.Min(_xmax, xx + (_xmax - _xmin) * (10.0 / _width));
                double yminy = Math.Max(_ymin, yy - (_ymax - _ymin) * (10.0 / _height));
                double ymaxy = Math.Min(_ymax, yy + (_ymax - _ymin) * (10.0 / _height));
                _xmin = xminx;
                _xmax = xmaxx;
                _ymin = yminy;
                _ymax = ymaxy;
                Status();
                DrawGraphics();
                pictureBox1.Refresh();
            }

        }



    }
}