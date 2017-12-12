using System;
using System.Collections.Generic;
using Models;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Linq;
using DAO;

namespace BUS
{
    public class ShapeBUS
    {
        ShapesDAO shapesDAO;
        string filePath;
        Point marginShape;
        Point startMovePoint;

        public ShapeBUS()
        {
            shapesDAO = new ShapesDAO();
            Shapes = new ObservableCollection<ShapeBase>();
        }

        public ObservableCollection<ShapeBase> Shapes
        {
            get;
            set;
        }

        public int ChoosenShapeIndex
        {
            get;
            set;
        } = -1;

        public bool isNewCanvas
        {
            get;
            set;
        } = true;


        public void CreateEllipse()
        {
            List<Point> points = getListPoints();
            if (points.Count == 2)
            {
                Point point1 = points[0]; 
                Point point2 = points[1];

                double centerX = Math.Abs(point1.X + point2.X) / 2;
                double centerY = Math.Abs(point1.Y + point2.Y) / 2;

                double center1X = Math.Abs(point1.X + point1.X) / 2;
                double center1Y = Math.Abs(point1.Y + point2.Y) / 2;


                double center2X = Math.Abs(point1.X + point2.X) / 2;
                double center2Y = Math.Abs(point2.Y + point2.Y) / 2;

                double radius1 = Math.Sqrt(Math.Pow(centerX - center1X, 2) + Math.Pow(centerY - center1Y, 2));
                double radius2 = Math.Sqrt(Math.Pow(centerX - center2X, 2) + Math.Pow(centerY - center2Y, 2));

                double startX;
                double startY;

                CheckStartPoint(point1.X, point1.Y, point2.X, point2.Y, out startX, out startY);

                EllipseShape ellipseShape = new EllipseShape(genereteName("Ellipse"), new Point(startX, startY), radius1, radius2);
                Shapes.Add(ellipseShape);

                RemoveAllPoint();
            }
        }

        void CheckStartPoint(double point1X, double point1Y, double point2X, double point2Y, out double startX, out double startY)
        {
            if (point1X < point2X && point1Y < point2Y)
            {
                startX = point1X;
                startY = point1Y;
            }
            else if (point1X > point2X && point1Y > point2Y)
            {
                startX = point2X;
                startY = point2Y;
            }
            else if (point1X < point2X && point1Y > point2Y)
            {
                startX = point1X;
                startY = point2Y;
            }
            else if (point1X > point2X && point1Y < point2Y)
            {
                startX = point2X;
                startY = point1Y;
            }
            else
            {
                startX = 0;
                startY = 0;
            }
        }

        public void CreatePentagon()
        {
            List<Point> points = getListPoints();
            if (points.Count == 5)
            {
                PolygonShape polygonShape = new PolygonShape(genereteName("Pentagon"), points);
                Shapes.Add(polygonShape);

                RemoveAllPoint();
            }
        }

        public void CreateHexagon()
        {
            List<Point> points = getListPoints();
            if (points.Count == 6)
            {
                PolygonShape polygonShape = new PolygonShape(genereteName("Hexagon"), points);
                Shapes.Add(polygonShape);

                RemoveAllPoint();
            }
        }

        public bool isNewPolyline { get; set; }
        public void CreatePolyline()
        {
            List<Point> points = getListPoints();
            if (points.Count == 2)
            {
                PolylineShape polylineShape = new PolylineShape(genereteName("Polyline"), points);
                Shapes.Add(polylineShape);
                RemoveAllPoint();
                isNewPolyline = true;
            }
            else if (isNewPolyline)
            {
                (Shapes.Last(polyline => polyline is PolylineShape) as PolylineShape).AddPoint(points[0]);
                RemoveAllPoint();
            }
        }

        public void CreatePolygon()
        {
            List<Point> points = getListPoints();
            if (Math.Sqrt(Math.Pow(points[0].X - points[points.Count - 1].X, 2) + Math.Pow(points[0].Y - points[points.Count - 1].Y, 2)) <= 10 && points.Count > 2)
            {
                points.RemoveAt(points.Count - 1);
                PolygonShape polygonShape = new PolygonShape(genereteName("Polygon"), points);
                Shapes.Add(polygonShape);

                RemoveAllPoint();
            }
        }

        public void AddPoint(Point point)
        {
            Shapes.Add(new PointShape(point.X, point.Y));
        }

        List<Point> getListPoints() 
        {
            List<Point> points = new List<Point>();

            foreach (var item in Shapes)
            {
                if (item is PointShape pointShape)
                {
                    points.Add(new Point(pointShape.X, pointShape.Y));
                }
            }

            return points;
        }

        public void RemoveAllPoint() 
        {
            foreach (var item in Shapes.ToList())
            {
                if (item is PointShape pointShape)
                {
                    Shapes.Remove(item);
                }
            }
        }

        string genereteName(string shapeType)
        {
            return shapeType + " " + ((from polygon in Shapes where polygon.Name.StartsWith(shapeType) select polygon).Count() + 1);
        }

        public void ChooseShape(string shapeName)
        {
            ClearChoose();
            ChoosenShapeIndex = Shapes.IndexOf(Shapes.Where(shape => shape.Name == shapeName).First());

            Shapes[ChoosenShapeIndex].IsChoose = true;
        }

        public void ClearChoose()
        {
            foreach (var item in Shapes)
            {
                item.IsChoose = false;
            }
        }

        public void FillShape(Color color)
        {
            if (color != Color.FromRgb(255, 255, 255))
            {
                Shapes[ChoosenShapeIndex].Color = color;
            }
        }

        public void NewCanvas()
        {
            Shapes.Clear();
            filePath = "";
            isNewCanvas = true;
        }

        public void SaveShapes(string fileName = "")
        {
            RemoveAllPoint();
            if (isNewCanvas)
            {
                filePath = fileName;
                isNewCanvas = false;
            }
            if (fileName != "")
            {
                filePath = fileName;
            }
            ClearChoose();
            shapesDAO.saveShapes(filePath, Shapes);
        }

        public void GetShapes(string fileName)
        {
            filePath = fileName;
            ObservableCollection<ShapeBase> observableCollection = shapesDAO.getShapes(fileName);
            Shapes.Clear();
            foreach (var item in observableCollection)
            {
                Shapes.Add(item);
            }
            ChoosenShapeIndex = -1;
        }

        public void SetShapeMarginAndStartMovePoint(Point startMovePoint)
        {
            marginShape = Shapes[ChoosenShapeIndex].Margin;
            this.startMovePoint = startMovePoint;
        }

        public void MoveShape(Point mousePoint)
        {
            Point newMarginPoint = new Point(mousePoint.X - startMovePoint.X + marginShape.X, mousePoint.Y - startMovePoint.Y + marginShape.Y);
            Shapes[ChoosenShapeIndex].Margin = newMarginPoint;
        }
    }
}
