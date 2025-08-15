using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class SaveToFile
    {
        public static Message Info;
        private static string FilePath = Path.Combine(Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location), "Memorizer.txt");

        public static void RecordToFile(ObservableCollection<Objective> items)
        {
            var file = File.CreateText(FilePath);
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    var text = item.Calendar + "/" + item.Description + "/" + item.Remind;
                    file.WriteLine(text);
                }
            }                
            //Info?.Invoke("Список дат записан в файл");
        }
        

        //todo
        public static ObservableCollection<Objective> ReaderFromFail()
        {
            if (File.Exists(FilePath))
            {
                var items = new ObservableCollection<Objective>();
                using (var reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] feld = line.Split('/');
                        var item = new Objective(DateOnly.Parse(feld[0]), feld[1], Int32.Parse(feld[2]));
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