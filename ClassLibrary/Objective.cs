using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary
{
    public delegate void Message(string message);
    public delegate void Update();

    public class Objective : INotifyPropertyChanged
    {
        public static Message Info;
        public static event Update Creat = () => { /*Info?.Invoke("Дата создана");*/ };

        private DateOnly _calendar;
        private string _description;
        private int _remind;

        public DateOnly Calendar
        {
            get { return _calendar; }
            set { _calendar = value; OnPropertyChanged("Calendar"); }
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

        public Objective() { }
        public Objective(DateOnly calendar, string description, int remind)
        {
            Calendar = calendar;
            Description = description;
            Remind = remind;
            Creat();
        }

        public static bool operator ==(Objective left, Objective rigt)
        {
            if (left.Calendar == rigt.Calendar && left.Description == rigt.Description
                && left.Remind == rigt.Remind) return true;
            else return false;
        }

        public static bool operator !=(Objective left, Objective rigt)
        {
            if (left == rigt) return false;
            else return true;
        }
    }
}
