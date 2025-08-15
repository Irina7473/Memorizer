using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Reminder> ITEMS { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ITEMS = new ObservableCollection<Reminder>();
            //this.DataContext = ITEMS;
            DataContext = this;
            //ObjectiveList.ItemsSource = ITEMS;
            //this.DataContext = new MainViewModel();
            //ItemsSource="{Binding Reminders}"

            Uploading_Click();

        }

        

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {
            
            string description = DescriptionTextBox.Text;
            if (DateOnly.TryParseExact(CalendarTextBox.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly calendar)
                && Int32.TryParse(RemindTextBox.Text, out int remind))
            {
                ITEMS.Add(new Reminder(calendar, description, remind));
                CalendarTextBox.Clear();
                DescriptionTextBox.Clear();
                RemindTextBox.Clear();
                State.Text = "Дата добавлена в список";
            }
            else
            {
                MessageBox.Show("Введите валидные дату и количество дней для напоминания");
            }
            
        }


        private void MenuItem_Click_Change(object sender, RoutedEventArgs e)
        {
            if (ObjectiveList.SelectedItem is Reminder selectedItem)
            {
                int index = ITEMS.IndexOf(selectedItem); 
                if (index != -1)
                {
                    ITEMS[index] = selectedItem;
                }
                State.Text = "Дата изменена";
            }
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
            var number = Int32.Parse(TextBox_Line.Text);
            for (int i = 0; i < number; i++) ITEMS.Add(new Reminder(DateOnly.MinValue, string.Empty, 0));
            TextBox_Line.Clear();
        }

        private void TextBox_Line_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Int32.Parse(TextBox_Line.Text); }
            catch { 
                TextBox_Line.Clear();
                MessageBox.Show("Введите целое число");

            }
        }

        private void RecordToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile.RecordToFile(ITEMS);
            State.Text = "Список дат записан в файл";
        }

        private void ReaderFromFail_Click(object sender, RoutedEventArgs e)
        {
            Uploading_Click();
        }

        private void Uploading_Click()
        {
            var newItems = SaveToFile.ReaderFromFail();
            if (newItems != null && newItems.Count > 0)
            {
                ITEMS = newItems;
                //ObjectiveList.Items.Refresh();
                State.Text = "Список дат загружен из файла";
            }
            else State.Text = "Список дат пуст";
        }
    }
}
