using System;

namespace VectorEditor.Control
{
    /// <summary>
    /// Прямоугольник выделения (примитив)
    /// </summary>
    public class SelectionRectanglePrimitive : AuxiliaryPrimitive
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public SelectionRectanglePrimitive(Guid id)
            : base(id)
        {
        }
    }
}