using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// Примитив "Линия"
    /// </summary>
    public class PolyLinePrimitive : Primitive
    {
        private readonly List<Point> _points = new List<Point>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="points"></param>
        /// <param name="lineWidth"></param>
        /// <param name="lineColor"></param>
        public PolyLinePrimitive(Guid id, Point[] points, double lineWidth, Color lineColor)
            : base(id)
        {
            _points.AddRange(points);
            LineWidth = lineWidth;
            LineColor = lineColor;
        }

        public override int KeyPointCount
        {
            get { return _points.Count; }
        }

        public override bool Contains(Point point)
        {
            for (int i = 0; i < _points.Count - 1; i++)
            {
                Point point1 = _points[i];
                Point point2 = _points[i + 1];
                LineGeometry lg = new LineGeometry(point1, point2);
                if (lg.StrokeContains(new Pen(Brushes.Black, LineHitTestWidth), point))
                {
                    return true;
                }
            }

            return false;
        }

        public override Point GetKeyPoint(int number)
        {
            if (number < 1)
            {
                number = 1;
            }
            if (number > _points.Count)
            {
                number = _points.Count;
            }

            return _points[number - 1];
        }

        public override int MakeHitTest(Point point)
        {
            if (IsSelected)
            {
                for (int i = 1; i <= KeyPointCount; i++)
                {
                    if (GetKeyPointRectangle(i).Contains(point))
                        return i;
                }
            }

            if (Contains(point))
                return 0;

            return -1;
        }

        public override bool IntersectsWith(Rect rectangle)
        {
            throw new NotImplementedException();
        }

        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i].Offset(deltaX, deltaY);
            }

            RefreshDrawing();
        }

        public override void MoveKeyPointTo(int number, Point point)
        {
            if (number > 0 && number <= _points.Count)
            {
                _points[number - 1] = point;
                RefreshDrawing();
            }
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Arrow;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            if (drawingContext == null)
            {
                throw new ArgumentNullException("drawingContext");
            }

            for (int i = 0; i < _points.Count - 1; i++)
            {
                Point point1 = _points[i];
                Point point2 = _points[i + 1];
                drawingContext.DrawLine(new Pen(new SolidColorBrush(LineColor), LineWidth), point1, point2);
            }

            base.Draw(drawingContext);
        }

        public void AddPoint(Point point)
        {
            _points.Add(point);
            RefreshDrawing();
        }

        public void RemovePoint(int number)
        {
            _points.RemoveAt(number - 1);
            RefreshDrawing();
        }
    }
}