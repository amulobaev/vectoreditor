using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public static class Helpers
    {
        public static Point GetPosition(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
            Point position = e.GetPosition(vectorEditor);
            position.Offset(vectorEditor.HorizontalOffset, vectorEditor.VerticalOffset);
            return position;
        }

    }
}
