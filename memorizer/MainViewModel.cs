using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace memorizer
{
    public class MainViewModel
    {
        ObservableCollection<Objective> Items { get; set; }
        RelayCommand AddItemCommand;
        public MainViewModel()
        {
            Items = new ObservableCollection<Objective>();
            AddItemCommand = new RelayCommand(AddItem);
        }

        private void AddItem()
        {
            Items.Add(new Objective());
            /*
            if (!string.IsNullOrWhiteSpace(Name) && Age > 0)
            {
                ITEMS.Add(new Item { Name = Name, Age = Age });
                Name = string.Empty; // Очистка поля ввода
                Age = 0; // Сброс возраста
            }*/
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
