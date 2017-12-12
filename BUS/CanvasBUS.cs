using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using Models;

namespace BUS
{
    public static class CanvasBUS
    {
        public static Shape NewShapeForCancas(PolygonShape polygonShape)
        {
            Polygon polygon = new Polygon();
            foreach (var item in polygonShape.PointList)
            {
                polygon.Points.Add(item);
            }
            polygon.Stroke = Brushes.Black;
            polygon.Fill = new SolidColorBrush(polygonShape.Color);
            polygon.Margin = new Thickness(polygonShape.Margin.X, polygonShape.Margin.Y, 0, 0);

            return polygon;
        }

        public static Shape NewShapeForCancas(PolylineShape polylineShape)
        {
            Polyline polyline = new Polyline();
            foreach (var item in polylineShape.PointList)
            {
                polyline.Points.Add(item);
            }
            polyline.Stroke = new SolidColorBrush(polylineShape.Color);
            polyline.Margin = new Thickness(polylineShape.Margin.X, polylineShape.Margin.Y, 0, 0);

            return polyline;
        }

        public static Shape NewShapeForCancas(EllipseShape ellipseShape)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 2 * ellipseShape.Radius1,
                Height = 2 * ellipseShape.Radius2,
                Margin = new Thickness(ellipseShape.Point.X + ellipseShape.Margin.X, ellipseShape.Point.Y + ellipseShape.Margin.Y, 0, 0),
                Stroke = Brushes.Black,
                Fill = new SolidColorBrush(ellipseShape.Color)
            };

            return ellipse;
        }

        public static Shape NewShapeForCancas(PointShape pointShape)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Black,
                Height = 2,
                Width = 2,
                Name = pointShape.Name,
                Margin = new Thickness(pointShape.X, pointShape.Y, 0, 0)
            };

            return ellipse;
        }

    }
}
