using System;
using System.IO;
namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"C:\SomeDir";
            Console.WriteLine("Введите путь к папке, которую необходимо очистить");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                Clean(path);
                Console.WriteLine("Очистка папки проведена");
            }
            else Console.WriteLine($"Папка по указанному пути {path} не существует, либо неверно указан путь к папке");
        }
        static void Clean(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();  // удаляем файлы которые не использовались более 30 минут
                    }

                }
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        Clean(dir.FullName); // рекурсивно очищаем все вложенные папки которые не использовались более 30 минут
                        dir.Delete();  // и удаляем их
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка:" + e.Message);
            }
        }
    }
}
