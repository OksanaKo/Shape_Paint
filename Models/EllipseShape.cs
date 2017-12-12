using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Models
{
    public class EllipseShape : ShapeBase
    {
        public Point Point
        {
            get;
            set;
        }

        public double Radius1
        {
            get;
            set;
        }

        public double Radius2
        {
            get;
            set;
        }

        public EllipseShape()
        {
        }

        public EllipseShape(string name, Point point, double radius1, double radius2, bool isChose = false) : base(name, isChose)
        {
            Point = point;
            Radius1 = radius1;
            Radius2 = radius2;
        }
    }
}
