using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public class CursorTool : BaseTool
    {
        private SelectionMode _selectionMode = SelectionMode.None;

        public override void OnMouseDown(CustomCanvas canvas, MouseButtonEventArgs e)
        {
            Point currentPoint = e.GetPosition(canvas);

            _selectionMode = SelectionMode.None;
        }

        public override void OnMouseMove(CustomCanvas canvas, MouseEventArgs e)
        {
            base.OnMouseMove(canvas, e);
        }

        public override void OnMouseUp(CustomCanvas canvas, MouseButtonEventArgs e)
        {
            base.OnMouseUp(canvas, e);
        }
    }
}