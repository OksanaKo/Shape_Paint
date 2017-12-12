using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using BUS;
using Models;
using System.IO;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        ShapeBUS shapeBUS;
        ShapesDAO shapesDAO;

        public UnitTest1()
        {

            shapesDAO = new ShapesDAO();
        }

        [TestMethod]
        public void AddPointTest()
        {
            shapeBUS = new ShapeBUS();
            Point expectedPoint = new Point(5, 5);
            shapeBUS.AddPoint(expectedPoint);

            Point actualPoint = new Point((shapeBUS.Shapes[0] as PointShape).X, (shapeBUS.Shapes[0] as PointShape).Y);

            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [TestMethod]
        public void CreateEllipseTest_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));

            shapeBUS.CreateEllipse();
        }

        [TestMethod]
        public void CreateEllipseTest2_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(-5, 5));

            shapeBUS.CreateEllipse();
        }

        [TestMethod]
        public void CreateEllipseTest3_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(-5, -5));

            shapeBUS.CreateEllipse();
        }


        [TestMethod]
        public void CreateEllipseTest5_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreateEllipse();
        }



        [TestMethod]
        public void CreateEllipseTest4_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, -5));

            shapeBUS.CreateEllipse();
        }

        [TestMethod]
        public void CreateEllipseTest_With1Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreateEllipse();
        }

        [TestMethod]
        public void CreatePentagonTest_With5Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.AddPoint(new Point(6, 5));
            shapeBUS.AddPoint(new Point(9, 5));
            shapeBUS.AddPoint(new Point(15, 5));

            shapeBUS.CreatePentagon();
        }

        [TestMethod]
        public void CreatePentagonTest_With1Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePentagon();
        }

        [TestMethod]
        public void CreateHexagonTest_With6Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.AddPoint(new Point(6, 5));
            shapeBUS.AddPoint(new Point(9, 5));
            shapeBUS.AddPoint(new Point(15, 5));
            shapeBUS.AddPoint(new Point(12, 5));

            shapeBUS.CreateHexagon();
        }

        [TestMethod]
        public void CreateHexagonTest_With1Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreateHexagon();
        }

        [TestMethod]
        public void CreatePolygonTest_With6Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.AddPoint(new Point(6, 5));
            shapeBUS.AddPoint(new Point(9, 5));
            shapeBUS.AddPoint(new Point(15, 5));
            shapeBUS.AddPoint(new Point(12, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();
        }

        [TestMethod]
        public void CreatePolygonTest2_With6Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.AddPoint(new Point(6, 5));
            shapeBUS.AddPoint(new Point(9, 5));
            shapeBUS.AddPoint(new Point(15, 5));
            shapeBUS.AddPoint(new Point(12, 4));
            shapeBUS.AddPoint(new Point(15, 15));

            shapeBUS.CreatePolygon();
        }

        [TestMethod]
        public void CreatePolygonTest_With3Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();
        }

        public void CreatePolylineTest_With3Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.AddPoint(new Point(6, 5));

            shapeBUS.CreatePolyline();
        }

        [TestMethod]
        public void CreatePolylineTest_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));

            shapeBUS.CreatePolyline();
        }

        [TestMethod]
        public void CreatePolylineTest_With1Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolyline();
        }

        [TestMethod]
        public void CreatePolylineTest2_With2Points()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(3, 50));

            shapeBUS.CreatePolyline();

            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.CreatePolyline();
        }


        [TestMethod]
        public void RemoveAllPointTest()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 6));
            shapeBUS.AddPoint(new Point(4, 0));

            shapeBUS.RemoveAllPoint();

            int expected = 0;
            int actual = shapeBUS.Shapes.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChooseShapeTest()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 6));

            shapeBUS.CreateEllipse();

            shapeBUS.ChooseShape("Ellipse 1");

            int expected = 0;
            int actual = shapeBUS.ChoosenShapeIndex;

            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void FillShapeTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();

            shapeBUS.ChooseShape("Polygon 1");

            Color color = Color.FromRgb(0, 0, 0);
            shapeBUS.FillShape(color);
        }

        [TestMethod]
        public void FillShapeTest2()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();

            Color color = Color.FromRgb(255, 255, 255);
            shapeBUS.FillShape(color);
        }


        [TestMethod]
        public void NewCanvasTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();

            Color color = Color.FromRgb(255, 255, 255);
            shapeBUS.FillShape(color);

            shapeBUS.NewCanvas();
        }
        [TestMethod]
        public void MoveShapeTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();
            shapeBUS.ChooseShape("Polygon 1");

            shapeBUS.SetShapeMarginAndStartMovePoint(new Point(0, 1));
            shapeBUS.MoveShape(new Point(2, 3));
        }

        [TestMethod]
        public void NewShapeForCancasTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));

            shapeBUS.CreatePolygon();

            CanvasBUS.NewShapeForCancas(shapeBUS.Shapes[0] as PolygonShape);
        }

        [TestMethod]
        public void NewShapeForCancasTest2()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));

            shapeBUS.CreateEllipse();

            CanvasBUS.NewShapeForCancas(shapeBUS.Shapes[0] as EllipseShape);
        }

        [TestMethod]
        public void NewShapeForCancasTest3()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(3, 50));

            shapeBUS.CreatePolyline();

            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.CreatePolyline();

            CanvasBUS.NewShapeForCancas(shapeBUS.Shapes[0] as PolylineShape);
        }

        [TestMethod]
        public void NewShapeForCancasTest4()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            
            CanvasBUS.NewShapeForCancas(shapeBUS.Shapes[0] as PointShape);
        }


        [TestMethod]
        public void SaveShapesTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.CreateEllipse();

            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 8));
            shapeBUS.AddPoint(new Point(5, 4));
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.CreatePolygon();

            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(3, 50));
            shapeBUS.CreatePolyline();

            shapeBUS.AddPoint(new Point(5, 5));
            shapeBUS.CreatePolyline();

            shapeBUS.SaveShapes("test.xaml");

            var fileText = File.ReadLines("test.xaml");
            Assert.IsTrue(fileText.ToString().Length > 1);
        }

        [TestMethod]
        public void GetShapesTest1()
        {
            shapeBUS = new ShapeBUS();
            shapeBUS.AddPoint(new Point(0, 0));
            shapeBUS.AddPoint(new Point(5, 5));

            shapeBUS.CreateEllipse();
            shapeBUS.GetShapes("dd.xaml");
        }

    }
}
