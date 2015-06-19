using System;
using System.Windows;

namespace VectorEditor.Control
{
    /// <summary>
    /// ������������� ��������� (��������)
    /// </summary>
    public class SelectionRectanglePrimitive : BaseRectanglePrimitive
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="id">�������������</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SelectionRectanglePrimitive(Guid id , double x, double y)
            : base(id, x, y, x, y)
        {
        }

    }
}