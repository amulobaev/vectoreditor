using System;
using System.Windows;

namespace VectorEditor.Control
{
    public class LinePrimitiveProperties : BasePrimitiveProperties
    {
        public LinePrimitiveProperties()
        {
        }

        public LinePrimitiveProperties(PolyLinePrimitive primitive)
            : base(primitive)
        {
            Points = primitive.Points.ToArray();
        }

        public Point[] Points { get; set; }
        
        public override Primitive CreatePrimitive()
        {
            return new PolyLinePrimitive(Id, Points, LineWidth, LineColor);
        }
    }
}