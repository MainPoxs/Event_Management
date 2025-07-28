using System;
using EventManagement.Pages;
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
using EventManagement.Models;

namespace EventManagement.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {       
        private readonly EventContext _context;     
        public AdminPage()
        {
            InitializeComponent();
            _context = new EventContext();
            User user = new User();
        }

        public void Button_AddUser(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new OrganizerAddPage());
        }

        public void Button_AllUsers(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new ListOrganizersPage());
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new StatisticPage());
        }
    }
}
