using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student (string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к бинарному файлу включая имя файла и расширение");
            Console.WriteLine(@"Например: C:\test\Students.dat");
            try
            {
                string filePath = Console.ReadLine();
                if (File.Exists(filePath))
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//находим путь к рабочему столу
                    string newDirPath = @$"{desktopPath}\Students";
                    Directory.CreateDirectory(newDirPath);
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        Student[] Students = (Student[])formatter.Deserialize(fs);
                        foreach (Student st in Students)
                        {
                            string groupFile = newDirPath + @$"\{st.Group}.txt";
                            using (StreamWriter sw = new StreamWriter(groupFile, true, System.Text.Encoding.Default))
                            {
                                sw.WriteLine($"{st.Name}, {st.DateOfBirth}");
                            }
                        }
                    }
                    Console.WriteLine($"Файл успешно считан, результаты в папке: {newDirPath}");
                }
                else Console.WriteLine("По указанному пути файл не существует или неверно указан путь к файлу");
            }
            
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка:" + e.Message);
            }
        }
    }
}
