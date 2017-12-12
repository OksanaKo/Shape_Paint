using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using BUS;
using Models;
using System.Windows.Input;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.ComponentModel;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeBUS bus;
        Action action = null;

        public MainWindow()
        {
            InitializeComponent();
            bus = new ShapeBUS();

            bus.Shapes.CollectionChanged += Shapes_CollectionChanged;
            ShapesListMenu.ItemsSource = bus.Shapes;
            ContextMenuItems.ItemsSource = bus.Shapes;

            ShapesListMenu.IsEnabled = false;
            Fill.IsEnabled = false;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewMenuItem_Click, Can));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenMenuItem_Click, Can));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveMenuItem_Click, Can));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsMenuItem_Click, Can));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, ExitMenuItem_Click, Can));
        }

        private void Can(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
  

        private void Shapes_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Color")
            {
                switch (sender)
                {
                    case EllipseShape ellipseShape:
                    case PolygonShape polygonShape:
                        (CanvasPaint.Children[bus.ChoosenShapeIndex] as Shape).Fill = new SolidColorBrush((sender as ShapeBase).Color);
                        break;
                    case PolylineShape polylineShape:
                        (CanvasPaint.Children[bus.ChoosenShapeIndex] as Shape).Stroke = new SolidColorBrush(polylineShape.Color);
                        break;
                    default:
                        break;
                }
            }
            else if (e.PropertyName == "Margin")
            {
                switch (sender)
                {
                    case EllipseShape ellipseShape:
                        (CanvasPaint.Children[bus.ChoosenShapeIndex] as Ellipse).Margin = new Thickness(ellipseShape.Point.X + ellipseShape.Margin.X, ellipseShape.Point.Y + ellipseShape.Margin.Y, 0, 0);
                        break;
                    case PolygonShape polygonShape:
                    case PolylineShape polylineShape:
                        (CanvasPaint.Children[bus.ChoosenShapeIndex] as Shape).Margin = new Thickness((sender as ShapeBase).Margin.X, (sender as ShapeBase).Margin.Y, 0, 0);
                        break;
                    default:
                        break;
                }
            }
            else if (e.PropertyName == "IsChoose")
            {
                int strokeThickness = 1;
                if ((sender as ShapeBase).IsChoose)
                {
                    strokeThickness = 2;
                }
                (CanvasPaint.Children[bus.ChoosenShapeIndex] as Shape).StrokeThickness = strokeThickness;
            }
            else if (e.PropertyName == "AddPoint")
            {
                CanvasPaint.Children.OfType<Polyline>().Last().Points.Add((sender as PolylineShape).PointList.Last());
                ShapesListMenu.IsEnabled = true;
            }
        }


        private void Shapes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    (e.NewItems[0] as INotifyPropertyChanged).PropertyChanged += Shapes_PropertyChanged;
                    ShapesListMenu.IsEnabled = true;

                    switch (e.NewItems[0])
                    {
                        case PointShape point:
                            CanvasPaint.Children.Add(CanvasBUS.NewShapeForCancas(point));
                            ShapesListMenu.IsEnabled = false;
                            break;
                        case EllipseShape ellipse:
                            CanvasPaint.Children.Add(CanvasBUS.NewShapeForCancas(ellipse));
                            break;
                        case PolygonShape polygon:
                            CanvasPaint.Children.Add(CanvasBUS.NewShapeForCancas(polygon));
                            break;
                        case PolylineShape polyline:
                            CanvasPaint.Children.Add(CanvasBUS.NewShapeForCancas(polyline));
                            break;
                        default:
                            break;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    switch (e.OldItems[0])
                    {
                        case PointShape point:
                            var points = CanvasPaint.Children.OfType<Ellipse>().Where(p => p.Name == PointShape.pointName).ToList();
                            foreach (var item in points)
                            {
                                CanvasPaint.Children.Remove(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    MessageBox.Show("Replace - 75Main - not be");
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ShapesListMenu.IsEnabled = false;
                    action = null;
                    CanvasPaint.Children.Clear();
                    Fill.IsEnabled = false;
                    break;
            }
        }

        private void ShapeItem_Click(object sender, RoutedEventArgs e)
        {
            bus.isNewPolyline = false;
            Fill.IsEnabled = false;
            switch (((MenuItem)sender).Name)
            {
                case "Ellipse":
                    action = bus.CreateEllipse;
                    bus.ClearChoose();
                    break;
                case "Pentagon":
                    action = bus.CreatePentagon;
                    bus.ClearChoose();
                    break;
                case "Hexagon":
                    action = bus.CreateHexagon;
                    bus.ClearChoose();
                    break;
                case "Polygon":
                    action = bus.CreatePolygon;
                    bus.ClearChoose();
                    break;
                case "Polyline":
                    action = bus.CreatePolyline;
                    bus.ClearChoose();
                    break;
                case "Fill":
                    bus.FillShape(GetColor());
                    Fill.IsEnabled = true;
                    break;
                default:
                    action = null;
                    bus.ClearChoose();
                    break;
            }
            CanvasPaint.MouseMove -= CanvasContainer_MouseMove;
        }

        Color GetColor()
        {
            Window1 window1 = new Window1();
            if (window1.ShowDialog() == true)
            {
                return window1.SelectedColor;
            }
            else
            {
                return Color.FromRgb(255,255,255);
            }
        }

        private void CanvasPaint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (action != null && e.LeftButton == MouseButtonState.Pressed)
            {
                bus.AddPoint(Mouse.GetPosition(CanvasPaint));
                action();
            }
            else if (action != null && e.RightButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
            }
        }

        private void MenuItem_Shapes_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            bus.ChooseShape(menuItem.Header.ToString());
            bus.isNewPolyline = false;
            Fill.IsEnabled = true;
            action = null;
            foreach (Shape item in CanvasPaint.Children)
            {
                item.MouseDown -= CanvasChildren_MouseDown;
            }
            CanvasPaint.Children[bus.ChoosenShapeIndex].MouseDown += CanvasChildren_MouseDown;
        }

        private void CanvasChildren_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bus.SetShapeMarginAndStartMovePoint(Mouse.GetPosition(CanvasPaint));
            CanvasPaint.MouseMove += CanvasContainer_MouseMove;
        }


        private void CanvasContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                bus.MoveShape(Mouse.GetPosition(CanvasPaint));
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                CanvasPaint.MouseMove -= CanvasContainer_MouseMove;
            }
        }

        private void NewMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            bus.NewCanvas();
        }

        private void SaveMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (bus.isNewCanvas)
            {
                SaveAsMenuItem_Click(sender, e);
            }
            else
            {
                bus.SaveShapes();
            }
        }
        private void SaveAsMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Untitled",
                DefaultExt = ".xaml",
                Filter = "Xmal documents (.xaml)|*.xaml"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                bus.SaveShapes(saveFileDialog.FileName);
            }
        }

        private void OpenMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                bus.GetShapes(openFileDialog.FileName);
            }
        }

        private void ExitMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
