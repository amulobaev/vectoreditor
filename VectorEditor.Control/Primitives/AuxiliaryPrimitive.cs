using System;

namespace VectorEditor.Control
{
    /// <summary>
    /// ������� ����� ��� ��������������� ����������
    /// </summary>
    public abstract class AuxiliaryPrimitive : BasePrimitive
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="id">�������������</param>
        protected AuxiliaryPrimitive(Guid id)
            : base(id)
        {
        }
    }
}