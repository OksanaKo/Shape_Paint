using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Models;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DAO
{
    public class ShapesDAO
    {
        public void saveShapes(string fileName, ObservableCollection<ShapeBase> MyShapesCollection)
        {
            XmlSerializer formatter = new XmlSerializer(MyShapesCollection.GetType(), new Type[] { typeof(PolygonShape), typeof(PolylineShape), typeof(EllipseShape)});

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, MyShapesCollection);
            }
        }

        public ObservableCollection<ShapeBase> getShapes(string fileName)
        {
            ObservableCollection<ShapeBase> MyShapesCollection;

            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<ShapeBase>), new Type[] { typeof(PolygonShape), typeof(PolylineShape), typeof(EllipseShape)});

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                MyShapesCollection = (ObservableCollection<ShapeBase>)formatter.Deserialize(fs);
            }

            return MyShapesCollection;
        }
    }
}
