using System;
using System.Windows;
using System.Windows.Media;

namespace VectorEditor.Control
{
    public class RectanglePrimitive : BasePrimitive
    {
        private readonly double _x1;
        private readonly double _y1;
        private readonly double _x2;
        private readonly double _y2;

        public RectanglePrimitive(Guid id, double x1, double y1, double x2, double y2)
            : base(id)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(_x1, _y1), new Point(_x2, _y2));
            drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 1), rect);

            base.Draw(drawingContext);
        }
    }
}