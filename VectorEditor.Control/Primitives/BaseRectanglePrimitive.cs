using System;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public abstract class BaseRectanglePrimitive : Primitive
    {
        private double _x1;

        private double _y1;

        private double _x2;

        private double _y2;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        protected BaseRectanglePrimitive(Guid id, double x1, double y1, double x2, double y2)
            : base(id)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        public override int KeyPointCount
        {
            get { return 8; }
        }

        public Rect Rectangle
        {
            get { return new Rect(new Point(_x1, _y1), new Point(_x2, _y2)); }
        }

        [Refresh]
        public double X1
        {
            get { return _x1; }
            set
            {
                if (value == _x1)
                {
                    return;
                }
                _x1 = value;
                RaisePropertyChanged();
            }
        }

        [Refresh]
        public double Y1
        {
            get { return _y1; }
            set
            {
                if (value == _y1)
                {
                    return;
                }
                _y1 = value;
                RaisePropertyChanged();
            }
        }

        [Refresh]
        public double X2
        {
            get { return _x2; }
            set
            {
                if (value == _x2)
                {
                    return;
                }
                _x2 = value;
                RaisePropertyChanged();
            }
        }

        [Refresh]
        public double Y2
        {
            get { return _y2; }
            set
            {
                if (value == _y2)
                {
                    return;
                }
                _y2 = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        public override Point GetKeyPoint(int number)
        {
            double xCenter = (_x2 + _x1) / 2;
            double yCenter = (_y2 + _y1) / 2;
            
            double x = _x1;
            double y = _y1;

            switch (number)
            {
                case 1:
                    x = _x1;
                    y = _y1;
                    break;
                case 2:
                    x = xCenter;
                    y = _y1;
                    break;
                case 3:
                    x = _x2;
                    y = _y1;
                    break;
                case 4:
                    x = _x2;
                    y = yCenter;
                    break;
                case 5:
                    x = _x2;
                    y = _y2;
                    break;
                case 6:
                    x = xCenter;
                    y = _y2;
                    break;
                case 7:
                    x = _x1;
                    y = _y2;
                    break;
                case 8:
                    x = _x1;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);
        }

        public override bool Contains(Point point)
        {
            return this.Rectangle.Contains(point);
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
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

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Arrow;
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        public override void MoveKeyPointTo(int number, Point point)
        {
            switch (number)
            {
                case 1:
                    _x1 = point.X;
                    _y1 = point.Y;
                    break;
                case 2:
                    _y1 = point.Y;
                    break;
                case 3:
                    _x2 = point.X;
                    _y1 = point.Y;
                    break;
                case 4:
                    _x2 = point.X;
                    break;
                case 5:
                    _x2 = point.X;
                    _y2 = point.Y;
                    break;
                case 6:
                    _y2 = point.Y;
                    break;
                case 7:
                    _x1 = point.X;
                    _y2 = point.Y;
                    break;
                case 8:
                    _x1 = point.X;
                    break;
            }

            RefreshDrawing();
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        public override bool IntersectsWith(Rect rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Move object
        /// </summary>
        public override void Move(double deltaX, double deltaY)
        {
            _x1 += deltaX;
            _x2 += deltaX;

            _y1 += deltaY;
            _y2 += deltaY;

            RefreshDrawing();
        }

    }
}