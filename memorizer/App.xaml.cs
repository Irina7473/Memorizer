using memorizer;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Windows;

namespace memorizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string RegKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";// Путь к ключу реестра
        private readonly string FirstRunValueName = "Memorizer";// Имя значения для первого запуска
        public const string SelectedWindowValueName = "SelectedWindow"; // Имя значения для выбора окна

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CheckFirstRun();
        }

        private void CheckFirstRun()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegKeyPath))
            {
                // Проверяем, существует ли значение FirstRun
                object firstRunValue = key.GetValue(FirstRunValueName);
                if (firstRunValue == null)
                {        
                    // Это первый запуск
                    ShowWelcomeMessage();
                    // Устанавливаем значение в реестре, чтобы отметить, что приложение уже запускалось
                    key.SetValue(FirstRunValueName, "0"); 
                    key.SetValue(SelectedWindowValueName, "SettingWindow"); 

                }
                ShowSelectedWindow();
            }
        }

        private void ShowWelcomeMessage()
        {
            MessageBox.Show("Добро пожаловать в наше приложение! 🎉", "Первый запуск", MessageBoxButton.OK, MessageBoxImage.Information);
            Thread.Sleep(5000);
            // Здесь вы можете добавить дополнительные действия, такие как открытие мастера настройки или показ инструкций.
        }

        private void ShowSelectedWindow()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegKeyPath))
            {
                // Получаем значение выбранного окна по умолчанию
                string selectedWindow = (string)key.GetValue(SelectedWindowValueName, "EnterWindow"); 
                Window windowToShow;
                if (selectedWindow == "EnterWindow") windowToShow = new EnterWindow();
                else windowToShow = new SettingWindow();
                windowToShow.Show();
            }
        }
    }
}