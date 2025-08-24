using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace memorizer
{    
    public partial class MainWindow
    {
        public ObservableCollection<Reminder> ITEMS { get; set; }
        public ObservableCollection<Reminder> Reminders { get; set; }
        public ObservableCollection<Reminder> FIND { get; set; }
        int index;

        public MainWindow()
        {
            InitializeComponent();
            ITEMS = [];
            Reminders = [];
            FIND = [];
            DataContext = this;
            index = -1;
            Uploading_Click();
        }

        private Reminder? NewReminder()
        {
            string description = DescriptionTextBox.Text;
            if (DateOnly.TryParseExact(CalendarTextBox.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateOnly calendar)
                && Int32.TryParse(RemindTextBox.Text, out int remind))
            {
                return new Reminder(calendar, description, remind);
            }
            else
            {
                MessageBox.Show("Введите валидные дату и количество дней для напоминания");
                return null;
            }
        }

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {           
            var item = NewReminder();
            if (item is not null)
            {
                Reminders.Add(item);
                ITEMS = new ObservableCollection<Reminder>(Reminders.OrderBy(item => item.Calendar));
                ObjectiveList.ItemsSource = ITEMS;
                CalendarTextBox.Clear();
                DescriptionTextBox.Clear();
                RemindTextBox.Clear();
                State.Text = "Дата добавлена в список";
            }           
        }

        private void MenuItem_Click_Change(object sender, RoutedEventArgs e)
        {
            if (ObjectiveList.SelectedItem is Reminder selectedItem)
            {
                index = Reminders.IndexOf(selectedItem);
                if (index != -1)
                {
                    CalendarTextBox.Text = selectedItem.Date;
                    DescriptionTextBox.Text = selectedItem.Description;
                    RemindTextBox.Text = selectedItem.Remind.ToString();
                }
            }
        }

        private void UpdateReminde_Click(object sender, RoutedEventArgs e)
        {
            var item = NewReminder();
            if (index != -1 && item is not null && item != ITEMS[index])
            { 
                Reminders[index] = item;
                ITEMS = new ObservableCollection<Reminder>(Reminders.OrderBy(p => p.Calendar));
                ObjectiveList.ItemsSource = ITEMS;
                State.Text = "Дата изменена";
            }
            CalendarTextBox.Clear();
            DescriptionTextBox.Clear();
            RemindTextBox.Clear();
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ObjectiveList.SelectedItem is Reminder selectedItem)
            {
                ITEMS.Remove(selectedItem);
                State.Text = "Дата удалена";
            }
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(LineTextBox.Text, out int number))
            {
                for (int i = 0; i < number; i++) ITEMS.Add(new Reminder(DateOnly.MinValue, string.Empty, 0));
                LineTextBox.Clear();
            }
            else MessageBox.Show("Введите число добавляемых строк");
        }


        private void RecordToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile.RecordToFile(Reminders);
            State.Text = "Список дат записан в файл";
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            ITEMS = new ObservableCollection<Reminder>(Reminders.OrderBy(item => item.Calendar));
            ObjectiveList.ItemsSource = ITEMS;
        }
        
        private void Uploading_Click()
        {
            Reminders = SaveToFile.ReaderFromFail();
            if (Reminders != null && Reminders.Count > 0)
            {
                ITEMS = new ObservableCollection<Reminder>(Reminders.OrderBy(item => item.Calendar));
                ObjectiveList.ItemsSource = ITEMS;
                State.Text = "Список дат загружен из файла";
            }
            else State.Text = "Список дат пуст";
        }

        private void SortByCalendars_Click(object sender, RoutedEventArgs e)
        {         
            ITEMS = new ObservableCollection<Reminder>(ITEMS
                .OrderBy(item => item.Calendar.Month)
                .ThenBy(item => item.Calendar.Day)
                );

            ObjectiveList.ItemsSource = ITEMS;
        }

        private void ShowForCurrentMonth_Click(object sender, RoutedEventArgs e)
        {
            var currentMonth = DateOnly.FromDateTime(DateTime.Now).Month;
            FIND = new ObservableCollection<Reminder>(Reminders.Where(item => item.Calendar.Month == currentMonth));
            if (FIND.Count == 0)
                MessageBox.Show("В вашем списке за текущий месяц нет памятных дат");
            else
            {
                ITEMS = FIND;
                ObjectiveList.ItemsSource = ITEMS;
                FIND = [];
            }
            SearchPhraseTextBox.Clear();
        }

        private void FindByDescription_Click(object sender, RoutedEventArgs e)
        {
            var search = SearchPhraseTextBox.Text;
            foreach (var item in Reminders) 
            {
                bool containsIgnoreCase = item.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsIgnoreCase) FIND.Add(item);   
            }
            if (FIND.Count == 0)
                MessageBox.Show("Искомая фраза не найдена");
            else
            {
                ITEMS = new ObservableCollection<Reminder>(FIND.OrderBy(item => item.Calendar));
                ObjectiveList.ItemsSource = ITEMS;
                FIND = [];
            }
            SearchPhraseTextBox.Clear();
        }                      
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите выйти?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else SaveToFile.RecordToFile(Reminders);
            base.OnClosing(e);
        }

    }
}
