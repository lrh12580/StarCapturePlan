using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Star
{
    public class Planet
    {
        /* 功能：获取行星在太阳系中的位置
         * 使用指南：
         * 1.构造Planet对象
         * 2.setTime() MUST!!!!!!!!!
         * 3.getLocation()调用后
         * 4.通过三个访问器来获取行星位置
         */
        const double RAD = Math.PI / 180;
        private double T;//day 公转周期
        private double r;//轨道半径KM
        private double a;//角度制  轨道倾斜角度
        private int yy;//
        private int mm;
        private int dd;
        private int hh;
        private int currenty;
        private int currentm;
        private int currentd;
        private int currenth;
        double x, y, z;//行星坐标！！该类的心脏！
        public Planet(double T, double r, double a, int yy, int mm, int dd, int hh)
        {
            this.T = T;
            this.r = r;
            this.a = a * RAD;
            this.yy = yy;
            this.mm = mm;
            this.dd = dd;
            this.hh = hh;
        }
        public Planet(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void setTime(int yy, int mm, int dd, int hh)
        {
            this.currenty = yy;
            this.currentm = mm;
            this.currentd = dd;
            this.currenth = hh;
        }
        public double getTimeDis(int year1, int month1, int day1, int hour1, int year2, int month2, int day2, int hour2)
        {//算冲日与当前时间差时大显神威！！！！！！！！！！！！！！！！
            double result = 0;
            result += (365.25 * (year1 - year2) + (month1 - month2) * 365.25 / 12 + day1 - day2);
            return Math.Abs(result * 24 + hour1 - hour2);
        }//返回值为小时

        public double getAngel(int month, int day)
        {//返回值为角度制！冲！日！的！时！候！
            if (month > 6) return ((month - 6) * 365.25 / 12 + day) * 360 / 365.25;//
            else return ((12 - month) * 365.25 / 12 + day) * 360 / 365.25;
        }

        public void getLocation()
        {
            double angel = this.getAngel(mm, dd);
            double dt = getTimeDis(yy, mm, dd, hh, currenty, currentm, currentd, currenth);//小时为单位
            double change_angel = dt / (T * 24) * 360;
            angel += change_angel;//当前行星角度
            angel *= RAD;//!!!!现在angel已经变身成了弧度！！！！
            //华丽地求出了行星X,Y,Z值
            x = -r * Math.Sin(angel);
            y = r * Math.Cos(a) * Math.Cos(angel);
            z = r * Math.Sin(a) * Math.Cos(angel);
        }
        public double getx()
        {
            return x;
        }
        public double gety()
        {
            return y;
        }
        public double getz()
        {
            return z;
        }
    }




    public class Earth
    {
        /* 使用指南
         * 1.通过构造EARTH类的对象，时间经纬等参数可通过修改器来修改
         * 2.getEarthLocation()获取地球坐标
         * 3.getLocalVertical()把当地上的x y z轴在太阳系中的单位向量求出
         */
        const double RAD = Math.PI / 180;
        const double R = 149600000;//地球公转半径 KM
        const double T = 525600;//地球公转周期  s
        const double DAY = 23 * 60 + 56;//分钟
        private int mm;//月份 当前时间
        private int dd;//天
        private int hour;//小时
        private int min;//分钟
        private double JingDu, WeiDu;//经度 纬度
        private double x, y;//地球坐标
        private double x1, y1, z1;//当地水平面法向量	z轴
        private double x2, y2, z2;//指向北极的向量	x轴
        private double x3, y3, z3;//指向正东的向量         y轴
        public Earth(int mm, int dd, int hour, int min, double WeiDu, double JingDu)
        {
            this.mm = mm;
            this.dd = dd;
            this.hour = hour;
            this.min = min;
            this.WeiDu = WeiDu;
            this.JingDu = JingDu;
        }
        public long getTime()
        {
            return min + 60 * hour + 3600 * dd + 3600 * 60 * mm;
        }
        public int getBJtime()
        {//返回值为分钟
            int res = ((int)JingDu - 116) / 15;
            return (12 + res) * 60;
        }
        public void getEarthLocation()
        {
            double angel;
            if (mm > 6) angel = ((mm - 6) * 365.2 / 12 + dd) * 360 / 365.25;
            else angel = ((12 - mm) * 365.2 / 12 + dd) * 360 / 365.25;
            x = -R * Math.Sin(angel);
            y = R * Math.Cos(angel);
        }
        public void getLocalVertical()
        {//a为纬度 r为公转半径
            long t = getTime();
            int t1 = getBJtime();//当地艳阳高照是BJ世界
            long tt = t - t1;//经过的时间
            double b = (tt % DAY) * 2 * Math.PI / DAY;
            double b1 = 66.5 * RAD;
            double w = WeiDu;
            double ox, oy, oz;
            ox = 0;
            oy = Math.Sin(w) * Math.Cos(b1);
            oz = Math.Sin(w) * Math.Sin(b1);
            //法向量！神他喵难求！
            x1 = -Math.Cos(w) * Math.Sin(b);
            y1 = Math.Sin(w) * Math.Cos(b1) + Math.Cos(b) * Math.Sin(b1) * Math.Cos(w);
            z1 = Math.Sin(w) * Math.Sin(b1) - Math.Cos(b) * Math.Cos(b1) * Math.Cos(w);
            //为求指向北极的向量埋下伏笔！
            double lo = Math.Sqrt(ox * ox + oy * oy + oz * oz);
            double k = 1 / Math.Sin(w);
            ox = ox * k / lo;
            oy = oy * k / lo;
            oz = oz * k / lo;
            //搞定指向北极的向量
            x2 = ox - x1;
            y2 = oy - y1;
            z2 = oz - z1;
            //用向量叉乘 求y轴
            x3 = z1 * y2 - y1 * z2;
            y3 = x1 * z2 - x2 * z1;
            z3 = y1 * x2 - x1 * y2;
            double l1 = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            double l2 = Math.Sqrt(x2 * x2 + y2 * y2 + z2 * z2);
            double l3 = Math.Sqrt(x3 * x3 + y3 * y3 + z3 * z3);
            x1 /= l1; y1 /= l1; z1 /= l1;
            x2 /= l2; y2 /= l2; z2 /= l2;
            x3 /= l1; y3 /= l3; z3 /= l3;
        }
        public void setTime(int mm, int dd, int hour)
        {
            this.mm = mm;
            this.dd = dd;
            this.hour = hour;
        }
        public void setWeiDu(double k)
        {
            this.WeiDu = k;
        }
        public void setJingDu(double k)
        {
            this.JingDu = k;
        }
        public double getx()
        {
            return x;
        }
        public double gety()
        {
            return y;
        }
        public double getx1()
        {
            return x1;
        }
        public double gety1()
        {
            return y1;
        }
        public double getz1()
        {
            return z1;
        }
        public double getx2()
        {
            return x2;
        }
        public double gety2()
        {
            return y2;
        }
        public double getz2()
        {
            return z2;
        }
        public double getx3()
        {
            return x3;
        }
        public double gety3()
        {
            return y3;
        }
        public double getz3()
        {
            return z3;
        }
    }

}

