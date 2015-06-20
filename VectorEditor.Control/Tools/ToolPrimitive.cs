using System.Windows.Input;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый инструмент для создания примитивов
    /// </summary>
    public abstract class ToolPrimitive : Tool
    {
        protected void AddNewPrimitive(VectorEditorControl vectorEditor, Primitive primitive)
        {
            vectorEditor.UnselectAll();
            primitive.IsSelected = true;
            vectorEditor.Add(primitive);
            vectorEditor.CaptureMouse();
        }
    }
}