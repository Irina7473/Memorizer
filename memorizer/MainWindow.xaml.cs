using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using ClassLibrary;

namespace memorizer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Objective> ITEMS { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainViewModel();
            ITEMS = new ObservableCollection<Objective>();
            ObjectiveList.ItemsSource = ITEMS;




            Uploading_Click();

        }

        private void Uploading_Click()
        {
            ITEMS = SaveToFile.ReaderFromFail();
            ObjectiveList.Items.Refresh();
            State.Text = "Список дат загружен из файла";
        }


        private void MenuItem_Click_Change(object sender, RoutedEventArgs e)
        {
            /*
            var item = (ObjectiveList.SelectedItem as ListViewItem).Content as Objective;
            ITEMS.Remove(item);
            State.Text = "Дата изменена";
            */
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            var item = (ObjectiveList.SelectedItem as ListViewItem).Content as Objective;
            ITEMS.Remove(item);
            State.Text = "Дата удалена";
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            var number = Int32.Parse(TextBox_Line.Text);
            for (int i = 0; i < number; i++) ITEMS.Add(new Objective());
        }

        private void TextBox_Line_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Int32.Parse(TextBox_Line.Text); }
            catch { TextBox_Line.Clear(); }
        }
    }
}
