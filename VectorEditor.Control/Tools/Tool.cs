﻿using System.Windows.Input;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый инструмент
    /// </summary>
    public abstract class Tool
    {
        public virtual void OnMouseDown(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseMove(VectorEditorControl vectorEditor, MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(VectorEditorControl vectorEditor, MouseButtonEventArgs e)
        {
        }
    }
}