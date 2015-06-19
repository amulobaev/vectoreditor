using System;
using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public class ToolRectangle : Tool
    {
        public override void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
            Point position = Helpers.GetPosition(vectorEditor, e);

            double x = position.X;
            double y = position.Y;

            RectanglePrimitive primitive = new RectanglePrimitive(Guid.NewGuid(), x, y, x + 100, y + 100)
            {
                IsSelected = true
            };

            vectorEditor.Add(primitive);
        }
    }
}
