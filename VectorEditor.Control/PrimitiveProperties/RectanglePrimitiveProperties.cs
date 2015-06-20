using System;

namespace VectorEditor.Control
{
    public class RectanglePrimitiveProperties : BasePrimitiveProperties
    {
        public RectanglePrimitiveProperties()
        {
        }

        public RectanglePrimitiveProperties(RectanglePrimitive primitive)
            : base(primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException("primitive");
            }

            X1 = primitive.X1;
            Y1 = primitive.Y1;
            X2 = primitive.X2;
            Y2 = primitive.Y2;
        }

        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public override Primitive CreatePrimitive()
        {
            return new RectanglePrimitive(Id, X1, Y1, X2, Y2, LineWidth, LineColor);
        }
    }
}