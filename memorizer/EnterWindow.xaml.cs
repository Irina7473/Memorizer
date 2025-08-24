using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace memorizer
{
    /// <summary>
    /// Логика взаимодействия для ExitWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window
    {
        public ObservableCollection<Reminder> ITEMS { get; set; }
        public EnterWindow()
        {
            InitializeComponent();
            ITEMS = [];
            DataContext = this;
            Uploading_Click();
        }

        private void Uploading_Click()
        {
            ITEMS = SaveToFile.ReaderFromFail(DateOnly.FromDateTime(DateTime.Now));
            if (ITEMS != null && ITEMS.Count > 0)
            {                
                ITEMS = new ObservableCollection<Reminder>(ITEMS
                .OrderBy(item => item.Calendar.Month)
                .ThenBy(item => item.Calendar.Day)
                );
                ObjectiveList.ItemsSource = ITEMS;
            }
            else MessageBox.Show("Ближайших событий не найдено");
        }

        
        private void ButtonListManagement_Click(object sender, RoutedEventArgs e)
        {
            MainWindow entry = new MainWindow();
            entry.Show();
            this.Close();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            CustomClose();
        }
        private void CustomClose()
        {            
            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите закрыть окно?", 
                "Подтверждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) this.Close();
        }

                
    }
}
