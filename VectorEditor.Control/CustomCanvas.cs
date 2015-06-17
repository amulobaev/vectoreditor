using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorEditor.Control
{
    public class CustomCanvas : Canvas
    {
        private readonly VisualCollection _visuals;

        /// <summary>
        /// Конструктор экземпляра
        /// </summary>
        public CustomCanvas()
        {
            _visuals = new VisualCollection(this);
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
