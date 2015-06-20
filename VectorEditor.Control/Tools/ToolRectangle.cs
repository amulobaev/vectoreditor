using System;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public class ToolRectangle : ToolRectangleBase
    {
        public override void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            Point point = Helpers.GetPosition(vectorEditor, e);

            double x = point.X;
            double y = point.Y;

            RectanglePrimitive primitive = new RectanglePrimitive(Guid.NewGuid(), x, y, x + 1, y + 1,
                vectorEditor.LineWidth, vectorEditor.LineColor);

            AddNewPrimitive(vectorEditor, primitive);
        }

        public override void OnMouseUp(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            vectorEditor.Tool = ToolType.Cursor;
            vectorEditor.Cursor = Cursors.Arrow;
            vectorEditor.ReleaseMouseCapture();
        }
    }
}
