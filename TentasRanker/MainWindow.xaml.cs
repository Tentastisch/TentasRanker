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
using TentasRanker.Pages;

namespace TentasRanker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame frames;

        public MainWindow()
        {
            InitializeComponent();
            frame.Content = new Main();
            frames = frame;
        }

        public static void StartSorting()
        {
            frames.Content = new Sort();
        }

        public static void FinishSorting()
        {
            frames.Content = new Finish();
        }
    }
}
