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

namespace EventManagement
{
    /// <summary>
    /// Логика взаимодействия для OrganizerAddPage.xaml
    /// </summary>
    public partial class OrganizerAddPage : Page
    {
        private readonly EventContext _context;
        public OrganizerAddPage()
        {
            InitializeComponent();
            _context = new EventContext();
            User user = new User();
        }
    }
}
