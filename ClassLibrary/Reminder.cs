using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClassLibrary
{
    public delegate void Message(string message);
    public delegate void Update();

    public class Reminder : INotifyPropertyChanged
    {
        public static Message Info;
        public static event Update Creat = () => { /*Info?.Invoke("Дата создана");*/ };

        private DateOnly _calendar;
        private string _date;
        private string _description ;
        private int _remind;

        public DateOnly Calendar
        {
            get { return _calendar; }
            set { _calendar = value; OnPropertyChanged("Calendar"); }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("date"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }
        public int Remind
        {
            get { return _remind; }
            set { _remind = value; OnPropertyChanged("Remind"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Reminder() { }
        public Reminder(DateOnly calendar, string description, int remind)
        {
            Calendar = calendar;
            Date = calendar.ToString();
            Description = description;
            Remind = remind;
        }

        public static bool operator ==(Reminder left, Reminder rigt)
        {
            if (left.Calendar == rigt.Calendar && left.Description == rigt.Description
                && left.Remind == rigt.Remind) return true;
            else return false;
        }

        public static bool operator !=(Reminder left, Reminder rigt)
        {
            if (left == rigt) return false;
            else return true;
        }

        public static int DateDifferenceInDays(DateOnly left, DateOnly rigt)
        {
            if (left.Month == rigt.Month && left.Day >= rigt.Day) 
                return left.Day - rigt.Day;
            else if (left.Month > rigt.Month) 
                left = new DateOnly(rigt.Year, left.Month, left.Day);
            else if (left.Month < rigt.Month || (left.Month == rigt.Month && left.Day < rigt.Day))
                left = new DateOnly(rigt.Year + 1, left.Month, left.Day);
            return left.DayNumber - rigt.DayNumber;
        }

    }
}
