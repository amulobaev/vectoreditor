using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VectorEditor.Control
{
    /// <summary>
    /// Базовый примитив
    /// </summary>
    public abstract class Primitive : DrawingVisual, INotifyPropertyChanged
    {
        #region Константы

        protected const double HitTestWidth = 8.0;

        protected const double HandleSize = 12.0;

        #endregion Константы

        #region Поля

        private readonly Guid _id;

        private bool _isSelected;

        private readonly List<string> _refreshDrawingProperties;

        private readonly SolidColorBrush _keyPointBrush1 = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        private readonly SolidColorBrush _keyPointBrush2 = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        private readonly SolidColorBrush _keyPointBrush3 = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        private double _lineWidth;
        private Color _lineColor;

        #endregion Поля

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        protected Primitive(Guid id)
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

        protected double LineHitTestWidth
        {
            get
            {
                // Ensure that hit test area is not too narrow
                return Math.Max(HitTestWidth, LineWidth);
            }
        }

        /// <summary>
        /// Толщина линии
        /// </summary>
        [Refresh]
        public double LineWidth
        {
            get { return _lineWidth; }
            set
            {
                if (value == _lineWidth)
                {
                    return;
                }
                _lineWidth = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Цвет линии
        /// </summary>
        [Refresh]
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                if (value == _lineColor)
                {
                    return;
                }
                _lineColor = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Количество ключеных точек
        /// </summary>
        public abstract int KeyPointCount { get; }

        /// <summary>
        /// Hit test, should be overwritten in derived classes.
        /// </summary>
        public abstract bool Contains(Point point);

        public abstract Point GetKeyPoint(int number);

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        public abstract int MakeHitTest(Point point);

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        public abstract bool IntersectsWith(Rect rectangle);

        public abstract void Move(double deltaX, double deltaY);
        
        /// <summary>
        /// Move handle to the point
        /// </summary>
        public abstract void MoveKeyPointTo(int number, Point point);

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        public abstract Cursor GetHandleCursor(int handleNumber);

        /// <summary>
        /// Получение ключевой точки по номеру
        /// </summary>
        public Rect GetKeyPointRectangle(int handleNumber)
        {
            Point point = GetKeyPoint(handleNumber);
            double size = Math.Max(HandleSize, LineWidth * 1.1);
            return new Rect(point.X - size / 2, point.Y - size / 2,
                size, size);
        }

        public virtual void Draw(DrawingContext drawingContext)
        {
            if (IsSelected)
            {
                for (int i = 1; i <= KeyPointCount; i++)
                {
                    DrawKeyPoint(drawingContext, GetKeyPointRectangle(i));
                }
            }
        }

        /// <summary>
        /// Отрисовка ключевой точки
        /// </summary>
        /// <param name="drawingContext"></param>
        /// <param name="rectangle"></param>
        public void DrawKeyPoint(DrawingContext drawingContext, Rect rectangle)
        {
            //
            drawingContext.DrawRectangle(_keyPointBrush1, null, rectangle);

            //
            drawingContext.DrawRectangle(_keyPointBrush2, null,
                new Rect(rectangle.Left + rectangle.Width / 8,
                         rectangle.Top + rectangle.Height / 8,
                         rectangle.Width * 6 / 8,
                         rectangle.Height * 6 / 8));

            //
            drawingContext.DrawRectangle(_keyPointBrush3, null,
                new Rect(rectangle.Left + rectangle.Width / 4,
                 rectangle.Top + rectangle.Height / 4,
                 rectangle.Width / 2,
                 rectangle.Height / 2));
        }

        public void RefreshDrawing()
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