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
using TentasRanker.Modules;

namespace TentasRanker.Pages
{
    /// <summary>
    /// Interaktionslogik für Sort.xaml
    /// </summary>
    public partial class Sort : Page
    {
        public static TextBlock leftStat;
        public static TextBlock rightStat;
        public static Label roundLabel;

        public Sort()
        {
            InitializeComponent();
            leftStat = first;
            rightStat = second;
            roundLabel = round;
        }

        /// <summary>
        /// Sets left TextBlock 
        /// </summary>
        /// <param name="text"></param>
        public static void SetLeft(string text)
        {
            leftStat.Text = text;
        }

        /// <summary>
        /// Sets right TextBlock 
        /// </summary>
        /// <param name="text"></param>
        public static void SetRight(string text)
        {
            rightStat.Text = text;
        }

        /// <summary>
        /// Change round text
        /// </summary>
        /// <param name="round">Round to set</param>
        public static void ChangeRound(int round)
        {
            roundLabel.Content = $"Round {round}";
        }

        /// <summary>
        /// Left option wins this round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void left_Click(object sender, RoutedEventArgs e)
        {
            Sorter.FinishRound(true);
        }

        /// <summary>
        /// Right option wins this round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void right_Click(object sender, RoutedEventArgs e)
        {
            Sorter.FinishRound(false);
        }
    }
}
