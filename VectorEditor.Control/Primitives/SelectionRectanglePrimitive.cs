using System;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// ������������� ��������� (��������)
    /// </summary>
    public class SelectionRectanglePrimitive : BaseRectanglePrimitive
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public SelectionRectanglePrimitive(double x1, double y1, double x2, double y2)
            : base(Guid.Empty, x1, y1, x2, y2)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(
                null,
                new Pen(Brushes.White, 1),
                Rectangle);

            DashStyle dashStyle = new DashStyle();
            dashStyle.Dashes.Add(4);

            Pen dashedPen = new Pen(Brushes.Black, 1);
            dashedPen.DashStyle = dashStyle;


            drawingContext.DrawRectangle(
                null,
                dashedPen,
                Rectangle);
        }
    }
}