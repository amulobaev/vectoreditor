using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    /// <summary>
    /// Реализация инструмента "Курсор"
    /// </summary>
    public class CursorTool : Tool
    {
        private SelectionMode _mode = SelectionMode.None;

        private Point _prevPoint = new Point();

        private Primitive _modifiedPrimitive;
        private int _modifiedPrimitiveKeyPoint;

        public override void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(vectorEditor);

            Point pointWithOffset = point;
            pointWithOffset.Offset(vectorEditor.HorizontalOffset, vectorEditor.VerticalOffset);

            _mode = SelectionMode.None;

            // Обработка изменения примитива
            for (int i = vectorEditor.Count - 1; i >= 0; i--)
            {
                Primitive primitive = vectorEditor[i];
                if (primitive.IsSelected)
                {
                    int keyPointNumber = primitive.MakeHitTest(pointWithOffset);
                    if (keyPointNumber > 0)
                    {
                        _mode = SelectionMode.Modification;
                        _modifiedPrimitive = primitive;
                        _modifiedPrimitiveKeyPoint = keyPointNumber;
                        vectorEditor.UnselectAll();
                        primitive.IsSelected = true;
                        break;
                    }
                }
            }

            // Обработка перемещения примитива
            if (_mode == SelectionMode.None)
            {
                Primitive primitive = null;
                for (int i = vectorEditor.Count - 1; i >= 0; i--)
                {
                    if (vectorEditor[i].MakeHitTest(pointWithOffset) == 0)
                    {
                        primitive = vectorEditor[i];
                    }
                }

                if (primitive != null)
                {
                    _mode = SelectionMode.MovePrimitive;
                    vectorEditor.UnselectAll();
                    primitive.IsSelected = true;
                    vectorEditor.Cursor = Cursors.SizeAll;
                }
            }

            // Обработка нажатия по фону
            if (_mode == SelectionMode.None)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    // Выделение нескольких примитивов
                    SelectionRectanglePrimitive primitive = new SelectionRectanglePrimitive(pointWithOffset.X,
                        pointWithOffset.Y, pointWithOffset.X + 1, pointWithOffset.Y + 1);
                    vectorEditor.Add(primitive);
                    _mode = SelectionMode.Selection;
                }
                else
                {
                    // Перемещение рабочей области
                    _mode = SelectionMode.MoveWorkspace;
                    vectorEditor.Cursor = Cursors.ScrollAll;
                }

                vectorEditor.UnselectAll();
            }

            _prevPoint = point;

            vectorEditor.CaptureMouse();
        }

        public override void OnMouseMove(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
            Point point = e.GetPosition(vectorEditor);
            Point pointWithOffset = point;
            pointWithOffset.Offset(vectorEditor.HorizontalOffset, vectorEditor.VerticalOffset);

            if (e.LeftButton == MouseButtonState.Released)
            {
                Cursor cursor = null;
                for (int i = 0; i < vectorEditor.Count; i++)
                {
                    Primitive primitive = vectorEditor[i];
                    int keyPointNumber = primitive.MakeHitTest(pointWithOffset);
                    if (keyPointNumber > 0)
                    {
                        cursor = primitive.GetHandleCursor(keyPointNumber);
                        break;
                    }
                }

                vectorEditor.Cursor = cursor ?? Cursors.Arrow;
            }

            if (!vectorEditor.IsMouseCaptured)
            {
                return;
            }

            double dx = point.X - _prevPoint.X;
            double dy = point.Y - _prevPoint.Y;

            _prevPoint = point;

            switch (_mode)
            {
                case SelectionMode.Modification:
                    if (_modifiedPrimitive != null)
                    {
                        _modifiedPrimitive.MoveKeyPointTo(_modifiedPrimitiveKeyPoint, pointWithOffset);
                    }
                    break;
                case SelectionMode.MovePrimitive:
                    foreach (Primitive primitive in vectorEditor.Selection)
                    {
                        primitive.Move(dx, dy);
                    }
                    break;
                case SelectionMode.MoveWorkspace:
                    // Перемещение рабочей области
                    vectorEditor.ScrollToHorizontalOffset(vectorEditor.HorizontalOffset - dx);
                    vectorEditor.ScrollToVerticalOffset(vectorEditor.VerticalOffset - dy);
                    break;
                case SelectionMode.Selection:
                    vectorEditor[vectorEditor.Count - 1].MoveKeyPointTo(5, pointWithOffset);
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

            switch (_mode)
            {
                case SelectionMode.Modification:
                    _modifiedPrimitive = null;
                    break;
                case SelectionMode.Selection:
                    SelectionRectanglePrimitive selectionRectanglePrimitive =
                        vectorEditor[vectorEditor.Count - 1] as SelectionRectanglePrimitive;
                    if (selectionRectanglePrimitive == null)
                        break;
                    Rect rect = selectionRectanglePrimitive.Rectangle;
                    vectorEditor.Remove(selectionRectanglePrimitive);
                    foreach (Primitive primitive in vectorEditor.Visuals.OfType<Primitive>())
                    {
                        if (primitive.IntersectsWith(rect))
                        {
                            primitive.IsSelected = true;
                        }
                    }
                    break;
            }

            vectorEditor.ReleaseMouseCapture();

            vectorEditor.Cursor = Cursors.Arrow;
        }

    }
}