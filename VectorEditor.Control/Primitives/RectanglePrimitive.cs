using System;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// Примитив "Прямоугольник"
    /// </summary>
    public class RectanglePrimitive : BaseRectanglePrimitive
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="lineWidth"></param>
        /// <param name="lineColor">Цвет линии</param>
        public RectanglePrimitive(Guid id, double x1, double y1, double x2, double y2, double lineWidth, Color lineColor)
            : base(id, x1, y1, x2, y2)
        {
            LineWidth = lineWidth;
            LineColor = lineColor;
        }

        public override BasePrimitiveProperties CreateSerializedObject()
        {
            return new RectanglePrimitiveProperties(this);
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="drawingContext"></param>
        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(null, new Pen(new SolidColorBrush(LineColor), LineWidth), Rectangle);

            base.Draw(drawingContext);
        }

    }
}