using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class SaveToFile
    {
        public static Message Info;
        private static string FilePath = Path.Combine(Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location), "Memorizer.txt");

        public static void RecordToFile(ObservableCollection<Reminder> items)
        {
            using var file = File.CreateText(FilePath);
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    var text = item.Date + "/" + item.Description + "/" + item.Remind;
                    file.WriteLine(text);
                }
            }                
            //Info?.Invoke("Список дат записан в файл");
        }
        

        //todo
        public static ObservableCollection<Reminder> ReaderFromFail()
        {
            if (File.Exists(FilePath))
            {
                var items = new ObservableCollection<Reminder>();
                using (var reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] feld = line.Split('/');
                        var item = new Reminder(
                            DateOnly.ParseExact(feld[0], "dd.MM.yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None),
                            feld[1], Int32.Parse(feld[2]));
                        items.Add(item);
                    }
                }
                //Info?.Invoke("Список дат загружен из файла");                
                return items;
            }
            else
            {
                Info?.Invoke("Файл не существует или путь указан неверно");
                return null;
            }
        }

        public static void ClearFile()
        {
            if (File.Exists(FilePath))
                File.WriteAllText(FilePath, null);
            else Info?.Invoke("Файл не существует или путь указан неверно");
        }
    }
}