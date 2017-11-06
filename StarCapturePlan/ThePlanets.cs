using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCapturePlan
{
    public class ThePlanets
    {
        public string name { get; set; }
        public string MusicPath { get; set; }
        public string introduction { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string onesentence { get; set; }
        public string story { get; set; }
        public int number { get; set; }
    }

    class StarClient
    {
        int mm = 0;
        int dd = 0;
        int hh = 0;
        int min = 0;
        double weidu = 1;
        double jingdu = 1;
        double a;
        double b;
        double c;
        double x = 0;
        double y = 0;
        double z = -1;
        static double x4 = 1;
        static double y4 = 1;
        static double z4 = 1;
        public static double experiment;
        public StarClient(int mm, int dd, int hh, int min, double weidu, double jingdu, double a, double b, double c)
        {
            this.mm = mm;
            this.dd = dd;
            this.hh = hh;
            this.min = min;
            this.weidu = weidu;
            this.jingdu = jingdu;
            this.a = a;
            this.b = b;
            this.c = c;
            experiment = a;
        }


        public int planetnumber()
        {
            Star.Earth earth = new Star.Earth(mm, dd, hh, min, weidu, jingdu);
            int planetnumber = 13;//检测到的行星数量
            Star.Planet[] p = new Star.Planet[19];
            p[0] = new Star.Planet(87.70, 57910000, 7.0, 2016, 5, 9, 11);//水星
            p[1] = new Star.Planet(224.701, 108208930, 3.4, 2012, 6, 6, 10);//金星
            p[2] = new Star.Planet(686.98, 227940000, 1.9, 2014, 4, 8, 21);//火星
            p[3] = new Star.Planet(4331.865, 778330000, 1.3, 2016, 3, 8, 11);//木星
            p[4] = new Star.Planet(10774.875, 1429400000, 2.5, 2015, 5, 23, 2);//土星
            p[5] = new Star.Planet(30797.88, 2870990000d, 0.8, 2015, 10, 12, 4);//天王星
            p[6] = new Star.Planet(60193.2, 4504000000d, 17.1, 2015, 9, 1, 4);//海王星
            const double MAX = 200000000000000;
            p[7] = new Star.Planet(-MAX, 0, 0);//双子座
            p[8] = new Star.Planet(-MAX * Math.Cos(30 * Math.PI / 180), -MAX * Math.Sin(30 * Math.PI / 180), 0);//金牛座
            p[9] = new Star.Planet(-MAX * Math.Sin(30 * Math.PI / 180), -MAX * Math.Cos(30 * Math.PI / 180), 0);//白羊座
            p[10] = new Star.Planet(0, MAX, 0);//双鱼座
            p[11] = new Star.Planet(MAX * Math.Cos(55 * Math.PI / 180), MAX * Math.Sin(55 * Math.PI / 180), 0);//水瓶座
            p[12] = new Star.Planet(-MAX * Math.Cos(25 * Math.PI / 180), MAX * Math.Sin(25 * Math.PI / 180), 0);//摩羯座
            p[13] = new Star.Planet(MAX, 0, 0);//人马座
            p[14] = new Star.Planet(MAX * Math.Cos(45 * Math.PI / 180), -MAX * Math.Sin(45 * Math.PI / 180), 0);//天蝎座
            p[15] = new Star.Planet(MAX * Math.Cos(60 * Math.PI / 180), -MAX * Math.Sin(60 * Math.PI / 180), 0);//天秤座
            p[16] = new Star.Planet(MAX * Math.Cos(85 * Math.PI / 180), -MAX * Math.Sin(85 * Math.PI / 180), 0);//处女座
            p[17] = new Star.Planet(-MAX * Math.Cos(58 * Math.PI / 180), -MAX * Math.Sin(58 * Math.PI / 180), 0);//狮子座
            p[18] = new Star.Planet(-MAX * Math.Cos(40 * Math.PI / 180), -MAX * Math.Sin(40 * Math.PI / 180), 0);//巨蟹座
            earth.getEarthLocation();
            earth.getLocalVertical();
            //z轴
            double x1 = earth.getx1();
            double y1 = earth.gety1();
            double z1 = earth.getz1();
            //x轴
            double x2 = earth.getx2();
            double y2 = earth.gety2();
            double z2 = earth.getz2();
            //y轴
            double x3 = earth.getx3();
            double y3 = earth.gety3();
            double z3 = earth.getz3();

            rotation(a, b, c);//获取相对于当地坐标系的方向向量 x,y,z
            change();

            double result = 0;
            double[] temp = new double[19];
            for (int i = 0; i < 19; i++)
            {
                //15,10,15,6,30,20,15,25,20,7,20,10,6
                if (i <= 6)
                {
                    p[i].setTime(2016, mm, dd, hh);
                    p[i].getLocation();
                    if (i == 2 || i == 1)
                    {
                        temp[i] = check(earth.getx() - p[i].getx(), earth.gety() - p[i].gety(), -p[i].getz(), x, y, z, 0.8);
                    }
                    else
                    {
                        temp[i] = check(earth.getx() - p[i].getx(), earth.gety() - p[i].gety(), -p[i].getz(), x, y, z, 0);
                    }
               
                }
                if (i == 7)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 8)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 9)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 10)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0.98);
                }
                if (i == 11)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 12)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 13)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 14)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 15)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 16)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0.9);
                }
                if (i == 17)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (i == 18)
                {
                    temp[i] = check(p[i].getx(), p[i].gety(), 0, x, y, z, 0);
                }
                if (temp[i] > result)
                {
                    result = temp[i];
                }

            }

            for (int k = 0; k < temp.Length; k++)
            {
                if (result == temp[k])
                {
                    planetnumber = k;
                }
            }
            Random random = new Random();
            int i1 = random.Next(0, 17);
            //return i1;
            return planetnumber;
        }
      


        public static double check(double x1, double y1, double z1, double x2, double y2, double z2, double deviation)
        {//deviation为允许的误差
            double result;
            double a = x1 * x2 + y1 * y2 + z1 * z2;
            double a1 = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            double a2 = Math.Sqrt(x2 * x2 + y2 * y2 + z2 * z2);
            result = Math.Abs(a);
            result = result / (a1 * a2);
            if (result < deviation) return 0;
            else return result;
         
        }

        //rotation方法是否准确还需在手机上小心求证！
        public void rotation(double a, double b, double c)
        {//求出相对当地水平面的向量
            double tempy = y;
            y = y * Math.Cos(a) - z * Math.Sin(a);
            z = z * Math.Cos(a) + tempy * Math.Sin(a);
            double tempx = x;
            x = x * Math.Cos(b) - z * Math.Sin(b);
            z = tempx * Math.Cos(b) + z * Math.Sin(b);
            tempx = x;
            x = x * Math.Cos(c) - y * Math.Sin(c);
            y = y * Math.Cos(c) + tempx * Math.Sin(c);
            double l = Math.Sqrt(x * x + y * y + z * z);
            x /= l;
            y /= l;
            z /= l;
        }

        public void change()
        {
            Star.Earth earth = new Star.Earth(mm, dd, hh, min, weidu, jingdu);
            double x1 = earth.getx1();
            double y1 = earth.gety1();
            double z1 = earth.getz1();
            //x轴
            double x2 = earth.getx2();
            double y2 = earth.gety2();
            double z2 = earth.getz2();
            //y轴
            double x3 = earth.getx3();
            double y3 = earth.gety3();
            double z3 = earth.getz3();
            //y轴

            double[,] matrix = {
                { x1, y1, z1, z + x1 * x4 + y1 * y4 + z1 * z4 },
                { x2, y2, z2, x + x2 * x4 + y2 * y4 + z2 * z4 },
                { x3, y3, z3, y + x3 * x4 + y3 * y4 + z3 * z4 } };
            caculate(matrix);
        }

        private static double JShls(double a11, double a12, double a13, double a21, double a22, double a23, double a31, double a32, double a33)
        {
            return a11 * a22 * a33 + a31 * a12 * a23 + a21 * a32 * a13 - a12 * a21 * a33 - a11 * a32 * a23 - a31 * a22 * a13;
        }
        private void caculate(double[,] temp)
        {
            double a11, a12, a13, a21, a22, a23, a31, a32, a33;
            double b1, b2, b3, d, d1, d2, d3;
            a11 = temp[0, 0];
            a12 = temp[1, 0];
            a13 = temp[2, 0];
            a21 = temp[0, 1];
            a22 = temp[1, 1];
            a23 = temp[2, 1];
            a31 = temp[0, 2];
            a32 = temp[1, 2];
            a33 = temp[2, 2];
            b1 = temp[0, 3];
            b2 = temp[1, 3];
            b3 = temp[2, 3];

            d = JShls(a11, a12, a13, a21, a22, a23, a31, a32, a33);
            d1 = JShls(b1, a12, a13, b2, a22, a23, b3, a32, a33);
            d2 = JShls(a11, b1, a13, a21, b2, a23, a31, b3, a33);
            d3 = JShls(a11, a12, b1, a21, a22, b2, a31, a32, b3);
            if (d == 0)
            {
                x = 0;
            }
            else
            {
                x = d1 / d - x4;
                y = d2 / d - y4;
                z = d3 / d - z4;
            }
        }
    }
}
