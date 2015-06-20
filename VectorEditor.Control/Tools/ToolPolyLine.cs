using System;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    /// <summary>
    /// Инструмент "Линия"
    /// </summary>
    public class ToolPolyLine : ToolPrimitive
    {
        private PolyLinePrimitive _line;

        public override void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            Point point = Helpers.GetPosition(vectorEditor, e);

            if (e.ChangedButton == MouseButton.Left)
            {
                // Нажата левая кнопка мыши
                if (_line == null)
                {
                    Point[] points = { point, new Point(point.X + 1, point.Y + 1) };
                    _line = new PolyLinePrimitive(Guid.NewGuid(), points, vectorEditor.LineWidth,
                        vectorEditor.LineColor);
                    AddNewPrimitive(vectorEditor, _line);
                }
                else
                {
                    _line.AddPoint(point);
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                // Нажата правая кнопка мыши
                // Закончить создание линии
                _line.RemovePoint(_line.KeyPointCount);
                vectorEditor.Tool = ToolType.Cursor;
                //vectorEditor.Cursor = Cursors.Arrow;
                vectorEditor.ReleaseMouseCapture();
            }
        }

        public override void OnMouseMove(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
            if (!vectorEditor.IsMouseCaptured)
            {
                return;
            }

            Point point = Helpers.GetPosition(vectorEditor, e);

            _line.MoveKeyPointTo(_line.KeyPointCount, point);
        }

    }
}