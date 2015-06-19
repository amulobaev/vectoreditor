using System;
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

        public ToolType Tool
        {
            get { return (ToolType)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
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

        public VisualCollection Visuals
        {
            get { return Canvas.Visuals; }
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

        

        public void UnselectAll()
        {
            foreach (Primitive primitive in Visuals.OfType<Primitive>())
            {
                primitive.IsSelected = false;
            }
        }

    }
}