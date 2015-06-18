using System;
using System.Windows;

namespace VectorEditor.Control
{
    public abstract class BaseRectanglePrimitive : BasePrimitive
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

        public Rect Rectangle
        {
            get { return new Rect(new Point(_x1, _y1), new Point(_x2, _y2)); }
        }
    }
}