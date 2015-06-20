using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorEditor.Control
{
    public class CustomCanvas : Canvas
    {
        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register("Owner",
            typeof(VectorEditorControl), typeof(CustomCanvas), new PropertyMetadata(callback));

        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.NewValue);
        }

        private readonly VisualCollection _visuals;

        /// <summary>
        /// Конструктор экземпляра
        /// </summary>
        public CustomCanvas()
        {
            _visuals = new VisualCollection(this);
        }

        internal Primitive this[int index]
        {
            get { return index >= 0 && index < Count ? (Primitive)_visuals[index] : null; }
        }

        public VectorEditorControl Owner
        {
            get { return (VectorEditorControl)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        public int Count
        {
            get { return _visuals.Count; }
        }

        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        /// <summary>
        /// Коллекция объектов Visual
        /// </summary>
        public VisualCollection Visuals
        {
            get { return _visuals; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

    }
}