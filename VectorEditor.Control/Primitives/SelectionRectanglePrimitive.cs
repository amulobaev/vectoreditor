using System;
using System.Windows;

namespace VectorEditor.Control
{
    /// <summary>
    /// Прямоугольник выделения (примитив)
    /// </summary>
    public class SelectionRectanglePrimitive : BaseRectanglePrimitive
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SelectionRectanglePrimitive(Guid id , double x, double y)
            : base(id, x, y, x, y)
        {
        }

    }
}