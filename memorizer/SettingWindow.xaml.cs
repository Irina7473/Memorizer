using ClassLibrary;
using memorizer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()

        {
            InitializeComponent();
        }

        private void SaveChoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_ColorScheme1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_ColorScheme2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_ColorScheme3_Checked(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MainWindow entry = new();
            entry.Show();
        }
    }
}