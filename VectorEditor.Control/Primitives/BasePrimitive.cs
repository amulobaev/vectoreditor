using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый примитив
    /// </summary>
    public abstract class BasePrimitive : DrawingVisual, INotifyPropertyChanged
    {
        private readonly Guid _id;
        
        private bool _isSelected;

        private readonly List<string> _refreshDrawingProperties;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        protected BasePrimitive(Guid id)
        {
            _id = id;

            // Заполнение списка свойств при изменении которых нужно перерисовать примитив
            _refreshDrawingProperties =
                this.GetType()
                    .GetProperties()
                    .Where(x => x.GetCustomAttributes(typeof(RefreshAttribute), true).Any())
                    .Select(x => x.Name)
                    .ToList();
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }

        [Refresh]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                {
                    return;
                }
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public virtual void Draw(DrawingContext drawingContext)
        {
            if (IsSelected)
            {
                DrawSelectionRectangle(drawingContext);
            }
        }

        public virtual void DrawSelectionRectangle(DrawingContext drawingContext)
        {
            
        }

        private void RefreshDrawing()
        {
            using (DrawingContext drawingContext = RenderOpen())
            {
                Draw(drawingContext);
            }
        }

        #region Реализация INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

            // Если изменилось значение свойства из списка обновления, то перерисовать
            if (_refreshDrawingProperties.Any(x => x == propertyName))
                RefreshDrawing();
        }

        #endregion Реализация INotifyPropertyChanged

    }
}