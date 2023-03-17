using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace graphic2
{
    public partial class Form1 : Form
    {
        Point p1, p2;
        public Form1()
        {
            InitializeComponent();
        }

        //Прямая
        private void Line(int I1, int J1, int I2, int J2)
        {
            Graphics g = CreateGraphics();
            if (I1 > I2)
            {
                int i = I1; I1 = I2; I2 = i;
                int j = J1; J1 = J2; J2 = j;
            }
            int dI = I2 - I1;
            int dJ = J2 - J1;
            //переменная которая хранит информацию откуда идет прямая, если она не горизонтальная
            int S = 0;
            if (dJ != 0)
                S = dJ / Math.Abs(dJ);
            //переменная выбора следующего пикселя
            int d = 2 * dJ - S * dI;
            //малый наклон к оси OX
            if (S * dJ <= dI)
            {
                int j = J1;
                for (int i = I1; i <= I2; i++)
                {
                    g.FillRectangle(Brushes.Black, i, j, 1, 1);
                    d += 2 * dJ;
                    if (S * d >= 0)
                    {
                        j += S;
                        d -= 2 * S * dI;
                    }
                }
            }
            //большой наклон к оси OX
            else
            {

                int i = I1;
                for (int j = J1; (J2 - j) * S >= 0; j += S)
                {
                    g.FillRectangle(Brushes.Black, i, j, 1, 1);
                    d += 2 * S * dI;
                    if (S * d >= 0)
                    {
                        d -= 2 * dJ;
                        i++;
                    }
                }
            }
            g.Dispose();
        }

        //Окружность
        private void Circle(int I1, int J1, int diff)
        {
            Graphics g = CreateGraphics();
            int i = 0;
            int j = diff;
            int d = 2 - 2 * diff;
            int e = 0;
            while (j >= 0)
            {
                //рисование окружности по 4 симметричным четвертям 
                g.FillRectangle(Brushes.Purple, I1 - i, J1 - j, 1, 1);
                g.FillRectangle(Brushes.Purple, I1 + i, J1 - j, 1, 1);
                g.FillRectangle(Brushes.Purple, I1 + i, J1 + j, 1, 1);
                g.FillRectangle(Brushes.Purple, I1 - i, J1 + j, 1, 1);
                //сравнение диагональный и горизонтальный пиксель
                if (d < 0)
                {
                    //разница между пикселями
                    e = 2 * (d + j) - 1;
                    //горизонтальный пиксель
                    if (e <= 0)
                        d += 2 * ++i + 1;
                    //диагональный пиксель
                    else
                        d += 2 * (++i - --j + 1);
                }
                //сравниваем диагональный и вертикальный пиксель
                else
                {
                    //разница между пикселями
                    e = 2 * (d - i) - 1;
                    //вертикальный пиксель
                    if (e > 0)
                        d -= 2 * --j + 1;
                    //диагональный пиксель
                    else
                        d += 2 * (++i - --j + 1);
                }
            }
            g.Dispose();
        }

        //Эллпис

        void Ellipse(int I1, int J1, int diffI, int diffJ)
        {
            Graphics g = CreateGraphics();
            int i = 0;
            int j = diffJ;
            int diffI2 = diffI * diffJ;
            int diffJ2 = diffJ * diffJ;
            int d = diffI2 + diffJ2 - 2 * diffI2 * diffJ;
            int e = 0;
            while (j >= 0)
            {
                //рисование окружности по 4 симметричным четвертям 
                g.FillRectangle(Brushes.Orange, I1 - i, J1 - j, 1, 1);
                g.FillRectangle(Brushes.Orange, I1 + i, J1 - j, 1, 1);
                g.FillRectangle(Brushes.Orange, I1 + i, J1 + j, 1, 1);
                g.FillRectangle(Brushes.Orange, I1 - i, J1 + j, 1, 1);
                //сравнение диагонального и горизонтального пикселя
                if (d < 0)
                {
                    //разница между пикселями
                    e = 2 * d + diffI2 * (2 + j) - 1;
                    //горизонтальный пиксель
                    if (e <= 0)
                        d += diffJ2 * (2 * ++i + 1);
                    //диагональный пиксель
                    else
                        d += diffJ2 * (2 * ++i + 1) - diffI2 * (2 * --j - 1);
                }
                //сравнение диагонального и вертикального пикселя
                else
                {
                    //разница между пикселями
                    e = 2 * d - diffJ2 * (2 - i) - 1;
                    //вертикальный пиксель
                    if (e > 0)
                        d -= diffI2 * (2 * --j - 1);
                    //диагональный пиксель
                    else
                        d += diffJ2 * (++i - --j + 1) - diffI2 * (2 * --j - 1);
                }
            }
            g.Dispose();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            p2 = e.Location;
            //разница координат p1 и p2
            int diff = (int)Math.Sqrt(Math.Pow((p2.X - p1.X), 2) +
                    Math.Pow((p2.Y - p1.Y), 2));
            if (radioButton1.Checked)
                Line(p1.X, p1.Y, p2.X, p2.Y);
            if (radioButton2.Checked)
                Circle(p1.X, p1.Y, diff);
            if (radioButton3.Checked)
                Ellipse(p1.X, p1.Y, diff, diff * 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(Color.White);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;
        }

    }
}