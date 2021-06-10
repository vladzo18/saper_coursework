using System.Windows;

namespace Saper_Coursework
{
    /// <summary>
    /// Interaction logic for SetUserNameWindow.xaml
    /// </summary>
    public partial class SetUserNameWindow : Window
    {
        private bool fieldIsFilled;

        public SetUserNameWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(userName.Text == "")
            {
                MessageBox.Show("Введіть ім'я!!!", Application.Current.MainWindow.Title);
            } else
            {
                ((MainWindow)Application.Current.MainWindow).userName = userName.Text;
                fieldIsFilled = true;
                this.Close();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!fieldIsFilled)
            {
                MessageBoxResult result = MessageBox.Show(
                   "Ви впевнені що вийти?",
                   this.Title,
                   MessageBoxButton.YesNoCancel
                );

                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                    Application.Current.MainWindow.Close();
                } else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
