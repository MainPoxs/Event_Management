using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для OrganizerPage.xaml
    /// </summary>
    public partial class OrganizerPage : Page
    {
        private readonly EventContext _context;
        private User user;
        private Event event_;
        public OrganizerPage()
        {
            InitializeComponent();
            _context = new EventContext();
            user = new User();
            event_ = new Event();
        }

        public void Button_AllEvents(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new NewEventPage());
        }

        public void Button_AddEvent(object sender, RoutedEventArgs e)
        {
            user = new User();
            event_ = new Event();
            NavigationService.Navigate(new AddChangeEventPage());
        }

        public void Button_Statistic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StatisticPage());
        }
    }
}
