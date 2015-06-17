﻿using System;
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
using VectorEditor.Control;

namespace VectorEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO убрать, это только для теста
            RectanglePrimitive primitive = new RectanglePrimitive(Guid.NewGuid(), 10,10, 100, 100);
            VectorEditorControl.Canvas.Visuals.Add(primitive);
            primitive.IsSelected = true;
        }
    }
}
