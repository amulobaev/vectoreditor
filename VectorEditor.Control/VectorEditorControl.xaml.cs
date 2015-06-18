using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VectorEditor.Control
{
    /// <summary>
    /// Логика взаимодействия для VectorEditorControl.xaml
    /// </summary>
    public partial class VectorEditorControl
    {
        private CustomCanvas _canvas;

        public VectorEditorControl()
        {
            InitializeComponent();

            _canvas = (CustomCanvas)this.Content;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}
