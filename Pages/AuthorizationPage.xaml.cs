using EventManagement.Models;
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

namespace EventManagement.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        ApplicationViewModel viewModel; 
        private User u;
        public AuthorizationPage()
        {
            InitializeComponent();
            u = new User();
            viewModel = new ApplicationViewModel();
        }
        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            User user = EventContext.DbContext.Users.FirstOrDefault(
                u => u.Login == login && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (user.RoleId == ((int)TitleUser.Admin))
                {
                    NavigationService.Navigate(new AdminPage());

                }
                else if (user.RoleId == ((int)TitleUser.Organizer))
                    NavigationService.Navigate(new OrganizerPage());
                else
                    NavigationService.Navigate(new EventsPage());
            }
        }


        private void Button_Reg(object sender, RoutedEventArgs e)
        {
             User user = new User();
            ApplicationViewModel.Loaded = NavigationService.Navigate(new RegistrationPage());
        }
    }
}
