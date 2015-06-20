using System;
using System.Windows.Media;

namespace VectorEditor.Control
{
    public abstract class BasePrimitiveProperties
    {
        protected BasePrimitiveProperties()
        {
        }

        protected BasePrimitiveProperties(Primitive primitive)
        {
            Id = primitive.Id;
            LineWidth = primitive.LineWidth;
            LineColor = primitive.LineColor;
        }

        public Guid Id { get; set; }

        public double LineWidth { get; set; }

        public Color LineColor { get; set; }

        public abstract Primitive CreatePrimitive();
    }
}
