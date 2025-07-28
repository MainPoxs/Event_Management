using EventManagement.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage());  //Открытие первой страницы
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (MainFrame.Content is Page page)
            {
                HeaderTextBlock.Text = page.Title;

                if (MainFrame.CanGoBack)
                    BackButton.Visibility = Visibility.Visible;
                else
                {
                    if(MainFrame.Content is AuthorizationPage page1)
                        BackButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
            else
                BackButton.Visibility = Visibility.Collapsed;
        }
    }
}