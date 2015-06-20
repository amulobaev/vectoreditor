using System;
using System.Windows;
using System.Windows.Media;

namespace VectorEditor.Control
{
    public class RectanglePrimitive : BaseRectanglePrimitive
    {
        public RectanglePrimitive(Guid id, double x1, double y1, double x2, double y2)
            : base(id, x1, y1, x2, y2)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 1), Rectangle);

            base.Draw(drawingContext);
        }

    }
}