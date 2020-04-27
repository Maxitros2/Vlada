using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zad10_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            Console.WriteLine("Введите a");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите b");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите h");
            h = Convert.ToDouble(Console.ReadLine());
            //коэффиценты
            double[] gor = new double[] { 1, 6.1d, -9, 0, 0, -4, -3, 0, 0, 2.3d };
            //прсчет точек
            for (double i = a; i <= b; i = i + h)
            {
                Points.Add(new PointF((float)i, (float)Gorner(i, gor)));
            }
            //выводим значения минимумов и максимумов            
            
            Points.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));
            Console.WriteLine("Минимум x = " + a);
            Console.WriteLine("Максимум x =  " + b);
            Console.WriteLine("Минимум y = " + Points.First().Y);
            Console.WriteLine("Максимум y = " + Points.ElementAt(Points.Count - 1).Y);
            Points.Sort((p1, p2) => p1.X.CompareTo(p2.X));
        }
         double a, b, h;
        public List<PointF> Points = new List<PointF>();
        //отображение консоли

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
           
            //рисуем точки
           
            //оси
            e.Graphics.DrawLine(Pens.Black, 0, this.Height / 2, this.Width, this.Height / 2);
            e.Graphics.DrawLine(Pens.Black, this.Width / 2, 0, this.Width / 2, this.Height);
            //подписи для осей
            for (int i = 0; i <= 20; i++)
            {
                e.Graphics.DrawString((b - a) / 20d * (i - 10) + "", Control.DefaultFont, Brushes.Black, (this.Width / 20F) * i, this.Height / 2 + 7);
            }
            for (int i = 0; i <= 20; i++)
            {
                e.Graphics.DrawString(-(i - 10) + "", Control.DefaultFont, Brushes.Black, this.Width / 2 + 7, (this.Height / 20F) * i);
            }
            PointF prp = new PointF();
            //корректируем точки и объединяем их в линии
            foreach (PointF p in Points.Select(x => new PointF(x.X * this.Width / (float)(b - a) + this.Width / 2, -x.Y * this.Height / 20f + this.Height / 2)).ToArray())
            {
                if (prp.IsEmpty)
                {
                    prp = p;
                    continue;
                }
                e.Graphics.DrawLine(Pens.Red, prp, p);
                prp = p;
            }

            
          
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
           

           
        }

        
                     
            //схема горнера
            static double Gorner(double x, double[] a, int i = 0)
            {
                if (i >= a.Length)
                    return 0;
                return a[i] + x * Gorner(x, a, i + 1);
            }
        
    }
}
