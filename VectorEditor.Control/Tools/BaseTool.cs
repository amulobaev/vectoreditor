using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый инструмент
    /// </summary>
    public abstract class BaseTool : ITool
    {
        public virtual void OnMouseDown(CustomCanvas canvas, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseMove(CustomCanvas canvas, MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(CustomCanvas canvas, MouseButtonEventArgs e)
        {
        }
    }
}