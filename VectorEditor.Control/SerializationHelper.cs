using System;
using System.Linq;
using System.Windows.Media;
using System.Xml.Serialization;

namespace VectorEditor.Control
{
    public class SerializationHelper
    {
        public SerializationHelper()
        {
        }

        public SerializationHelper(VisualCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            Properties =
                collection.OfType<Primitive>().Select(x => x.CreateSerializedObject()).Where(x => x != null).ToArray();
        }

        [XmlArrayItem(typeof(RectanglePrimitiveProperties))]
        [XmlArrayItem(typeof(LinePrimitiveProperties))]
        public BasePrimitiveProperties[] Properties { get; set; }
    }
}
