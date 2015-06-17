using System;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый класс для вспомогательных примитивов
    /// </summary>
    public abstract class AuxiliaryPrimitive : BasePrimitive
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        protected AuxiliaryPrimitive(Guid id)
            : base(id)
        {
        }
    }
}