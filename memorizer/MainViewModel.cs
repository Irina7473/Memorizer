using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace memorizer
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Reminder> Reminders { get; set; }
        public Reminder NewReminder { get; set; }

        public ICommand AddReminderCommand { get; }

        public MainViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
            NewReminder = new Reminder();
            AddReminderCommand = new RelayCommand(AddReminder);
        }

        private void AddReminder()
        {
            if (NewReminder.Calendar> DateOnly.MinValue && !string.IsNullOrWhiteSpace(NewReminder.Description) && NewReminder.Remind > 0)
            {
                Reminders.Add(new Reminder { Calendar = NewReminder.Calendar, Description=NewReminder.Description, Remind=NewReminder.Remind});
                NewReminder.Calendar = DateOnly.MinValue;
                NewReminder.Description = string.Empty;
                NewReminder.Remind = 0;
            }
        }
    }
    
}
