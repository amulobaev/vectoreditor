using System.Windows;
using System.Windows.Input;

namespace VectorEditor.Control
{
    public abstract class ToolRectangleBase : ToolPrimitive
    {
        /// <summary>
        /// Обработка перемещения курсора мыши
        /// </summary>
        /// <param name="vectorEditor"></param>
        /// <param name="e">Аргументы события</param>
        public override void OnMouseMove(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
            if (!vectorEditor.IsMouseCaptured)
            {
                return;
            }

            Point point = Helpers.GetPosition(vectorEditor, e);

            vectorEditor[vectorEditor.Visuals.Count - 1].MoveKeyPointTo(5, point);
        }
    }
}