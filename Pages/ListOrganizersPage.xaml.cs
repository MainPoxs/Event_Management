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
    /// Логика взаимодействия для ListOrganizersPage.xaml
    /// </summary>
    public partial class ListOrganizersPage : Page
    {
        private readonly EventContext _context;
        private User user;
        private Event event_;
        public ListOrganizersPage()
        {
            InitializeComponent();
            _context = new EventContext();
            user = new User();
            event_ = new Event();
        }

        private void Button_EditUser(object sender, RoutedEventArgs e)
        {
            User user = new User();

            NavigationService.Navigate(new OrganizerAddPage());
            user = new User();
            event_ = new Event();
        }
    }
}
