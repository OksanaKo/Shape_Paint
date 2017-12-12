using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Models
{
    public class PolylineShape : ShapeBase
    {
        List<Point> pointList;
        public List<Point> PointList
        {
            get
            {
                return pointList;
            }
            set
            {

                this.pointList = value;
                if (value.Count == 1)
                {
                    NotifyPropertyChanged();
                }
            }
        }

        public PolylineShape()
        {
        }

        public PolylineShape(string name, List<Point> pointList, bool isChose = false) : base(name, isChose)
        {
            base.Color = Color.FromRgb(0, 0, 0);
            PointList = new List<Point>();
            foreach (var item in pointList)
            {
                PointList.Add(item);
            }
        }

        public void AddPoint(Point point)
        {
            PointList.Add(point);
            NotifyPropertyChanged();
        }
    }
}
