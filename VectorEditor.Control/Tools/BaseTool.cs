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
    public abstract class BaseTool
    {
        public virtual void OnMouseDown(CustomCanvas canvas, MouseButtonEventArgs args)
        {
        }

        public virtual void OnMouseMove(CustomCanvas canvas, MouseEventArgs args)
        {
        }

        public virtual void OnMouseUp(CustomCanvas canvas, MouseButtonEventArgs args)
        {
        }
    }
}