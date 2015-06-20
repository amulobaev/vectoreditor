using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

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
            "LineWidth", typeof(double), typeof(VectorEditorControl), new PropertyMetadata(1.0, LineWidthChanged));

        public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register(
            "LineColor", typeof(Color), typeof(VectorEditorControl), new PropertyMetadata(Colors.Black, LineColorChanged));

        public ToolType Tool
        {
            get { return (ToolType)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }

        /// <summary>
        /// Толщина линии
        /// </summary>
        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        /// <summary>
        /// Цвет линии
        /// </summary>
        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        private readonly Tool[] _tools;
        private CustomCanvas _canvas;

        /// <summary>
        /// Конструктор
        /// </summary>
        public VectorEditorControl()
        {
            InitializeComponent();

            _canvas = (CustomCanvas)Content;

            _tools = new Tool[3];
            _tools[0] = new CursorTool();
            _tools[1] = new ToolRectangle();
            _tools[2] = new ToolPolyLine();
        }

        internal Primitive this[int index]
        {
            get { return _canvas[index]; }
        }

        public VisualCollection Visuals
        {
            get { return _canvas.Visuals; }
        }

        public int Count
        {
            get { return _canvas.Count; }
        }

        /// <summary>
        /// Выбранные примитивы
        /// </summary>
        internal IEnumerable<Primitive> Selection
        {
            get { return _canvas.Visuals.OfType<Primitive>().Where(x => x.IsSelected).ToList(); }
        }

        /// <summary>
        /// Добавление примитива на рабочую область
        /// </summary>
        /// <param name="primitive"></param>
        public void Add(Primitive primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException("primitive");
            }

            _canvas.Visuals.Add(primitive);
        }

        /// <summary>
        /// Удаление примитива с рабочей области
        /// </summary>
        /// <param name="primitive"></param>
        public void Remove(Primitive primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException("primitive");
            }

            _canvas.Visuals.Remove(primitive);
        }

        public void Clear()
        {
            _canvas.Visuals.Clear();
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

        /// <summary>
        /// Обработка события изменения свойства зависимости "Толщина линии"
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e">Аргументы</param>
        private static void LineWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VectorEditorControl vectorEditor = d as VectorEditorControl;
            if (vectorEditor != null)
            {
                foreach (Primitive primitive in vectorEditor.Selection)
                {
                    primitive.LineWidth = vectorEditor.LineWidth;
                }
            }
        }

        /// <summary>
        /// Обработка события изменения свойства зависимости "Цвет линии"
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void LineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VectorEditorControl vectorEditor = d as VectorEditorControl;
            if (vectorEditor != null)
            {
                foreach (Primitive primitive in vectorEditor.Selection)
                {
                    primitive.LineColor = vectorEditor.LineColor;
                }
            }
        }

        public void SelectAll()
        {
            foreach (Primitive primitive in Visuals.OfType<Primitive>())
            {
                primitive.IsSelected = true;
            }
        }

        public void UnselectAll()
        {
            foreach (Primitive primitive in Visuals.OfType<Primitive>())
            {
                primitive.IsSelected = false;
            }
        }

        public void Load(string fileName)
        {
            SerializationHelper helper;
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationHelper));
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                helper = (SerializationHelper)serializer.Deserialize(stream);
            }

            if (helper.Properties == null)
            {
                return;
            }

            Clear();

            foreach (BasePrimitiveProperties property in helper.Properties)
            {
                Add(property.CreatePrimitive());
            }
        }

        public void Save(string fileName)
        {
            try
            {
                SerializationHelper helper = new SerializationHelper(_canvas.Visuals);
                XmlSerializer serializer = new XmlSerializer(typeof(SerializationHelper));
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(stream, helper);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}