using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using TentasRanker.Modules;

namespace TentasRanker.Pages
{
    /// <summary>
    /// Interaktionslogik für Finish.xaml
    /// </summary>
    public partial class Finish : Page
    {
        public Finish()
        {
            InitializeComponent();
            listBox.ItemsSource = Sorter.list;
        }

        /// <summary>
        /// Opens a new save dialog and saves to text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, Sorter.list);
                Application.Current.Shutdown();
            }
        }
    }
}
