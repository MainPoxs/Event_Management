using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace EventManagement.Models;

public partial class Event : INotifyPropertyChanged
{
    private int eventId;
    public int EventId
    { 
        get => eventId; 
        set
        {
            eventId = value;
            OnPropertyChanged(nameof(EventId));
        }
    }

    private string? eventName;
    public string EventName
    { 
        get => eventName;
        set
        {
            eventName = value;
            OnPropertyChanged(nameof(EventName));
        }
    }

    private DateTime date;
    public DateTime Date
    {
        get => date; 
        set
        {             
            date = value;
            OnPropertyChanged(nameof(Date));
        }
    }
    private TimeOnly time;
    public TimeOnly Time
    {
        get => time;
        set
        {
            time = value;
            OnPropertyChanged(nameof(Time));
        }
    }
    private string eventLocation;
    public string EventLocation
    { 
        get => eventLocation;
        set
        {
            eventLocation = value;
            OnPropertyChanged(nameof(EventLocation));
        }
    }

    private string description;
    public string Description
    {
        get => description; 
        set
        {
            description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    private byte[] imageEvent;
    public byte[] ImageEvent
    {
        get => imageEvent;
        set
        {
            imageEvent = value;
            OnPropertyChanged(nameof(ImageEvent));
        }
    }
    private string? eventComment;
    public string? EventComment
    { 
        get => eventComment;
        set
        {
            if (eventComment == null)
            {
                eventComment = "";
            }
            eventComment = value;
        }
    } 
    public int UserId { get; set; } = 0;
    public virtual User? User { get; set; } = null!;  

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
