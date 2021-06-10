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
using System.Windows.Shapes;

namespace Saper_Coursework
{
    /// <summary>
    /// Interaction logic for SetСomplexityWindow.xaml
    /// </summary>
    public partial class SetСomplexityWindow : Window
    {
        public SetСomplexityWindow()
        {
            InitializeComponent();
        }

        private void startClilk(object sender, RoutedEventArgs e)
        {
           
            if (Int32.Parse(heightBox.Text) * Int32.Parse(widthBox.Text) <= Int32.Parse(minesAmountBox.Text))
            {
                MessageBox.Show("Мін не може бути більше чим кліточок у рівні!", Application.Current.MainWindow.Title);
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).gameHeight
                = Int32.Parse(heightBox.Text);
                ((MainWindow)Application.Current.MainWindow).gameWidth
                = Int32.Parse(widthBox.Text);
                ((MainWindow)Application.Current.MainWindow).gameMinesAmount
                = Int32.Parse(minesAmountBox.Text);

                this.DialogResult = true;
                this.Close();
            }      
        }
        private void cancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
