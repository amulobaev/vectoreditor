using System;
using System.Windows;

namespace VectorEditor.Control
{
    public abstract class BaseRectanglePrimitive : Primitive
    {
        private readonly double _x1;

        private readonly double _y1;

        private readonly double _x2;

        private readonly double _y2;

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
    }
}