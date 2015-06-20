using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// Логика взаимодействия для VectorEditorControl.xaml
    /// </summary>
    public partial class VectorEditorControl
    {
        public static readonly DependencyProperty ToolProperty = DependencyProperty.Register(
            "Tool", typeof(ToolType), typeof(VectorEditorControl), new PropertyMetadata(ToolType.Cursor));

        public static readonly DependencyProperty LineWidthProperty = DependencyProperty.Register(
            "LineWidth", typeof (double), typeof (VectorEditorControl), new PropertyMetadata(1d, LineWidthChanged));

        public ToolType Tool
        {
            get { return (ToolType)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }

        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        private readonly Tool[] _tools;

        /// <summary>
        /// Конструктор
        /// </summary>
        public VectorEditorControl()
        {
            InitializeComponent();

            _tools = new Tool[3];
            _tools[0] = new CursorTool();
            _tools[1] = new ToolRectangle();
            _tools[2] = new ToolLine();
        }

        internal Primitive this[int index]
        {
            get { return Canvas[index]; }
        }

        public VisualCollection Visuals
        {
            get { return Canvas.Visuals; }
        }

        public int Count
        {
            get { return Canvas.Count; }
        }

        /// <summary>
        /// Выбранные примитивы
        /// </summary>
        internal IEnumerable<Primitive> Selection
        {
            get { return Canvas.Visuals.OfType<Primitive>().Where(x => x.IsSelected).ToList(); }
        }

        public void Add(Primitive primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException("primitive");
            }

            Canvas.Visuals.Add(primitive);
        }

        public void Remove(Primitive primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException("primitive");
            }

            Canvas.Visuals.Remove(primitive);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (_tools[(int)Tool] == null)
                return;

            this.Focus();

            _tools[(int)Tool].OnMouseDown(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_tools[(int)Tool] == null)
                return;

            _tools[(int)Tool].OnMouseMove(this, e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_tools[(int)Tool] == null)
                return;

            _tools[(int)Tool].OnMouseUp(this, e);
        }

        private static void LineWidthChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
        }

        public void UnselectAll()
        {
            foreach (Primitive primitive in Visuals.OfType<Primitive>())
            {
                primitive.IsSelected = false;
            }
        }

    }
}