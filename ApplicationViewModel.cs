using EventManagement.Command;
using EventManagement.Models;
using EventManagement.Pages;
using EventManagement.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace EventManagement
{
    enum TitleUser
    {
        Admin = 1,
        Organizer = 2,
        Visitor = 3
    }
    internal class ApplicationViewModel : INotifyPropertyChanged
    {        
        static public bool Loaded { get; set; }
        private EventContext _context;

        private User user;
        public User User
        {
            get => user; 
            set
            {                
                user = value;
                OnPropertyChanged(nameof(User));
            } 
        }        

        private Event event_;
        public Event Event_
        {
            get => event_;
            set
            {
                event_ = value;
                OnPropertyChanged(nameof(Event_));
            }
        }
        private Role role;
        public Role Role
        {
            get => role;
            set
            {
                role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
        
        private ObservableCollection<Event> events;
        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }
        private ObservableCollection<Role> roles;
        public ObservableCollection<Role> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        private BitmapImage bitmapImage;
        public BitmapImage BitmapImage
        {
            get => bitmapImage;
            set
            {
                bitmapImage = value;
                OnPropertyChanged(nameof(BitmapImage));
            }
        }
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        
        public ApplicationViewModel()
        {
            user = new User();           
            event_ = new Event();          
            role = new Role();
          

            events = new ObservableCollection<Event>();
            users = new ObservableCollection<User>();                           
            roles = new ObservableCollection<Role>();

            _context = new EventContext();
            Event_.Date = DateTime.Today;
            Event_.Time = TimeOnly.FromDateTime(Event_.Date);
        
            LoadAsync();
            GetBytes();

            Event_.Date = DateTime.SpecifyKind((DateTime)Event_.Date, DateTimeKind.Utc);          
        }
        public async Task LoadAsync()
        {
            
            var ev = await _context.Events.ToListAsync();      
            if (ev != null) 
                Events = new ObservableCollection<Event>(ev);

            var us = await _context.Users.ToListAsync();
            if (us != null)
                Users = new ObservableCollection<User>(us);        

            var r = await _context.Roles.ToListAsync();
            if (r != null)
                Roles = new ObservableCollection<Role>(r);      
            
        }
        public void GetBytes()
        {
            if (_context != null)
            {
                Event_.ImageEvent = new EventContext().Events.FirstOrDefault()?.ImageEvent;
                ConverterToImage();
            }        
         
        }
        public void ConverterToImage()
        {
            if (bitmapImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(Event_.ImageEvent))
                {
                    var b = new BitmapImage();
                    b.BeginInit();
                    b.CacheOption = BitmapCacheOption.OnLoad;
                    b.StreamSource = memoryStream;
                    b.EndInit();
                    bitmapImage = b;
                }
            }
        }
        public byte[] ConverterToByte(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
                return null;

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                Event_.ImageEvent = stream.ToArray();
            }
            return Event_.ImageEvent;
        }
        private RelayCommand loadUser;
        public RelayCommand LoadUser
        {
            get
            {
                return loadUser ??
                   (loadUser = new RelayCommand(async obj =>
                   {
                       await LoadAsync();
                   }
                   ));
            }
        }
        private RelayCommand addNewEvent;
        public RelayCommand AddNewEvent
        {
            get
            {
                return addNewEvent ??
                    (addNewEvent = new RelayCommand(async obj =>
                    {                      

                        if (event_ == null)
                            event_ = new Event();
                        
                        Event_.Date = DateTime.SpecifyKind((DateTime)Event_.Date, DateTimeKind.Utc);
                        event_.EventComment = "";
                        ConverterToByte(bitmapImage);

                        User = _context.Users.FirstOrDefault(
                            u => u.Login == User.Login && u.Password == User.Password);

                        event_.UserId = user.UserId;

                        if (string.IsNullOrEmpty(Event_.EventName) ||
                           string.IsNullOrEmpty(Event_.EventLocation) ||
                           string.IsNullOrEmpty(Event_.Description) ||
                           (Event_.ImageEvent == null))
                        {
                            MessageBox.Show("Поле не заполнено!");
                        }
                        else
                        {                       

                            await _context.Events.AddAsync(event_);
                            await _context.SaveChangesAsync();
                            await LoadAsync();                          

                            MessageBox.Show("Мероприятие добавлено");                           
                        }
                        event_ = new Event();

                    }));
            }
        }
   

        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ??
                    (addNewUser = new RelayCommand(async obj =>
                    {                  

                        if (user == null)                       
                            user = new User();
                    
                        if (Loaded == true)                      
                            user.RoleId = (int)TitleUser.Visitor;
                        else
                            user.RoleId = (int)TitleUser.Organizer;

                        if (string.IsNullOrEmpty(User.FirstName) ||
                           string.IsNullOrEmpty(User.LastName) ||
                           string.IsNullOrEmpty(User.Login) ||
                           string.IsNullOrEmpty(User.Password))
                        {
                            MessageBox.Show("Поле не заполнено!");
                        }
                        else if (await _context.Users.AnyAsync(u => u.Login == user.Login ||
                        u.Password == user.Password))
                        {
                            MessageBox.Show("Выберите другой логин или пароль!"); 
                        }                        
                        else
                        {
                            await _context.Users.AddAsync(user);
                            await _context.SaveChangesAsync();                                                 

                            MessageBox.Show("Пользователь добавлен");                            
                        }
                        user = new User();
                         await LoadAsync();

                    }));
            }
        }
        private RelayCommand addNewVisitor;
        public RelayCommand AddNewVisitor
        {
            get
            {
                return addNewVisitor ??
                    (addNewVisitor = new RelayCommand(async obj =>
                    {

                        if (user == null)
                            user = new User();

                        if (Loaded == true)
                            user.RoleId = (int)TitleUser.Visitor;
                        
                        if (string.IsNullOrEmpty(User.FirstName) ||
                           string.IsNullOrEmpty(User.LastName) ||
                           string.IsNullOrEmpty(User.Login) ||
                           string.IsNullOrEmpty(User.Password))
                        {
                            MessageBox.Show("Поле не заполнено!");
                        }
                        else if (await _context.Users.AnyAsync(v => v.Login == user.Login ||
                        v.Password == user.Password))
                        {
                            MessageBox.Show("Выберите другой логин или пароль!");
                        }
                        else
                        {
                            await _context.Users.AddAsync(user);
                            await _context.SaveChangesAsync();

                            MessageBox.Show("Пользователь добавлен");                            
                        }
                        await LoadAsync();

                    }));
            }
        }


        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ??
                    (deleteUser = new RelayCommand(async obj =>
                    {
                        user = obj as User;
                        if (user != null)
                            _context.Users.Remove(user);
                        await _context.SaveChangesAsync();

                        await LoadAsync(); 
                    },
                    (obj) => Users.Count > 0));
            }
        }
        private RelayCommand deleteEvent;
        public RelayCommand DeleteEvent
        {
            get
            {
                return deleteEvent ??
                    (deleteEvent = new RelayCommand(async obj =>
                    {
                        event_ = obj as Event;
                        if (event_ != null)
                            _context.Events.Remove(event_);
                        await _context.SaveChangesAsync();

                        await LoadAsync();
                    },
                    (obj) => Events.Count > 0));
            }
        }
        private RelayCommand saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return saveFile ??
                    (saveFile = new RelayCommand(obj =>
                    {
                        var result = MessageBox.Show("Сформировать билет?", "", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.No)
                            return;                         

                        if (result == MessageBoxResult.Yes)
                        {
                            User = EventContext.DbContext.Users.FirstOrDefault(
                            v => v.Login == User.Login && v.Password == User.Password);

                            Event_.UserId = User.UserId;                         
                        }

                        SaveFileDialog save_ = new SaveFileDialog
                        {
                            Filter = "RTF файл (*.rtf)|*.rtf",
                            FileName = "Ticket.rtf"                            
                        };
                        if(save_.ShowDialog() == true)
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine($"Электронный билет");
                            sb.AppendLine($"{Event_.EventName}");
                            sb.AppendLine($"Дата: {Event_.Date}");
                            sb.AppendLine($"Место проведения: {Event_.EventLocation}");
                            sb.AppendLine($"Заказчик: {User.FullName}");
                            File.WriteAllText(save_.FileName, sb.ToString(), Encoding.UTF8);
                            MessageBox.Show("Билет сохранен!");
                        }
                    }));
            }
        }
        private RelayCommand saveFileStatistic;
        public RelayCommand SaveFileStatistic
        {
            get
            {
                return saveFileStatistic ??
                    (saveFileStatistic = new RelayCommand(obj =>
                    {
                        var result = MessageBox.Show("Сформировать отчет?", "", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.No)
                            return;

                        if (result == MessageBoxResult.Yes)
                        {
                            User = EventContext.DbContext.Users.FirstOrDefault(
                            v => v.Login == User.Login && v.Password == User.Password);

                            Event_.UserId = User.UserId;
                         
                        }

                        SaveFileDialog save_ = new SaveFileDialog
                        {
                            Filter = "RTF файл (*.rtf)|*.rtf",
                            FileName = "Ticket.rtf"
                        };
                        if (save_.ShowDialog() == true)
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine($"Отчет");
                            sb.AppendLine($"Название мероприятия: {Event_.EventName}");
                            sb.AppendLine($"Дата: {Event_.Date}");
                            sb.AppendLine($"Место проведения: {Event_.EventLocation}");
                            sb.AppendLine($"Посетитель: {user.FullName}");
                            File.WriteAllText(save_.FileName, sb.ToString(), Encoding.UTF8);
                            MessageBox.Show("Отчет сохранен!");
                        }
                    }));
            }
        }
        private RelayCommand saveUserEvent;
        public RelayCommand SaveUserEvent
        {
            get
            {
                return saveUserEvent ??
                   (saveUserEvent = new RelayCommand(async obj =>
                   {
                       User = EventContext.DbContext.Users.FirstOrDefault(
                            v => v.Login == User.Login && v.Password == User.Password);
                       Event_.Date = DateTime.SpecifyKind((DateTime)Event_.Date, DateTimeKind.Utc);                     
                       event_.UserId = user.UserId;
                       await _context.SaveChangesAsync();
                       await LoadAsync();
                   }
                   ));
            }
        }
        private RelayCommand download;
        public RelayCommand Download
        {
            get
            {
                return download ??
                   (download = new RelayCommand(obj =>
                   {
                       OpenFileDialog openFile = new OpenFileDialog();
                       if (openFile.ShowDialog() == true)
                       {
                           BitmapImage = new BitmapImage(new Uri(openFile.FileName));
                       }                            
                   }
                   ));
            }
        }
       private RelayCommand addComment;
       public RelayCommand AddComment
        {
            get
            {
                return addComment ??
                   (addComment = new RelayCommand(obj =>
                   {
                       Window_Comment showCom = new Window_Comment();
                    
                       showCom.ShowDialog();
                   }
                   ));
            }
        }       
       
        public void SelectionEvent()
        {
           using(var con = new EventContext())
            {
                ;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
       
    }
}
