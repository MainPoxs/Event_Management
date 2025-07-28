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
    /// Логика взаимодействия для NewEventPage.xaml
    /// </summary>
    public partial class NewEventPage : Page
    {
        private readonly EventContext _context;
        public NewEventPage()
        {
            InitializeComponent();
            _context = new EventContext();
            User user = new User();
        }

        public void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new AddChangeEventPage()); 
        }

        public void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new AddChangeEventPage());
        }      
        public void DataGrid_OpenDetails(object sender, EventArgs e)
        {
            User user = new User();
            NavigationService.Navigate(new DetailsEventPage());
        }
       
    }
}
