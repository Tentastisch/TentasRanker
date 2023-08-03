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
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens up a file and selects all lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var list = File.ReadAllLines(openFileDialog.FileName);
                Sorter.list = list.ToList();
            }
            errorblock.Text = "";

            for (int i = Sorter.list.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(Sorter.list[i]))
                    Sorter.list.RemoveAt(i);
            }

            listBox.ItemsSource = Sorter.list;
        }

        /// <summary>
        /// Starts the sorting process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (Sorter.list.Count == 0)
            {
                errorblock.Text = "Please select a file";
                return;
            }

            int ignore = 0;
            if (int.TryParse(textBox.Text, out ignore))
            {
                var rounded = int.Parse(textBox.Text);
                if (rounded < 2)
                    textBox.Text = "2";
                else if (rounded > 100)
                    textBox.Text = "100";
            }
            else
                textBox.Text = "2";

            Sorter.ChangeRounds(textBox.Text);  //Change rounds in sorter to selected rounds
            MainWindow.StartSorting();          //Change page to sort page
            Sorter.StartSort();                 //Start the sorting process
        }
    }
}
