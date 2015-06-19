using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public class CursorTool : Tool
    {
        private SelectionMode _mode = SelectionMode.None;

        private Point _prevPoint = new Point();

        public override void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            Point currentPoint = e.GetPosition(vectorEditor);

            _mode = SelectionMode.None;

            // Обработка изменения примитива
            for (int i = vectorEditor.Canvas.Visuals.Count - 1; i >= 0; i--)
            {

            }

            // Обработка перемещения примитива
            if (_mode == SelectionMode.None)
            {
                for (int i = vectorEditor.Canvas.Visuals.Count - 1; i >= 0; i--)
                {

                }
            }

            // Обработка нажатия по фону
            if (_mode == SelectionMode.None)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    // Выделение нескольких примитивов
                }
                else
                {
                    // Перемещение рабочей области
                    vectorEditor.UnselectAll();
                    _mode = SelectionMode.MoveWorkspace;
                    vectorEditor.Cursor = Cursors.ScrollAll;
                }

            }

            _prevPoint = currentPoint;

            vectorEditor.CaptureMouse();
        }

        public override void OnMouseMove(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
            if (!vectorEditor.IsMouseCaptured)
            {
                return;
            }

            Point currentPoint = e.GetPosition(vectorEditor);

            double dx = currentPoint.X - _prevPoint.X;
            double dy = currentPoint.Y - _prevPoint.Y;

            _prevPoint = currentPoint;

            switch (_mode)
            {
                case SelectionMode.MovePrimitive:
                    break;
                case SelectionMode.MoveWorkspace:
                    // Перемещение рабочей области
                vectorEditor.ScrollToHorizontalOffset(vectorEditor.HorizontalOffset - dx);
                vectorEditor.ScrollToVerticalOffset(vectorEditor.VerticalOffset - dy);
                    break;
            }

        }

        public override void OnMouseUp(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            if (!vectorEditor.IsMouseCaptured)
            {
                _mode = SelectionMode.None;
                return;
            }

            vectorEditor.ReleaseMouseCapture();

            vectorEditor.Cursor = Cursors.Arrow;
        }

    }
}