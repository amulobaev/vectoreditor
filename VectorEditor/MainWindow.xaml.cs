using System.Windows;
using Microsoft.Win32;

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
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            VectorEditorControl.Clear();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "XML (*.xml)|*.xml", DefaultExt = "xml" };
            if (dialog.ShowDialog() == true)
            {
                VectorEditorControl.Load(dialog.FileName);
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "XML (*.xml)|*.xml", DefaultExt = "xml" };
            if (dialog.ShowDialog() == true)
            {
                VectorEditorControl.Save(dialog.FileName);
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}