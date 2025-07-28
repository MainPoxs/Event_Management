using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventManagement.Models;

public partial class User : INotifyPropertyChanged
{
    private int userId;
    public int UserId
    {
        get => userId;
        set
        {
            userId = value;
            OnPropertyChanged(nameof(UserId));
        }
    }
    private string firstName;
    public string FirstName
    {
        get => firstName;
        set
        {
            firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }
    private string lastName;
    public string LastName
    {
        get => lastName;
        set
        {
            lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    } 
    private string login;
    public string Login
    {
        get => login;
        set
        {         
            login = value;
            OnPropertyChanged(nameof(Login));
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {            
        
            password = value;
            OnPropertyChanged(nameof(Password));
        }
    }    
    public string FullName { get => $"{FirstName} {LastName}"; } 
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
