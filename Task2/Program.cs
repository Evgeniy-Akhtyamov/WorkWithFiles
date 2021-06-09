using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке, размер которой вы хотите получить");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                if (GetSizeDir(path) != 0)
                    Console.WriteLine($"Размер паки: {GetSizeDir(path)} байт");
                else Console.WriteLine("Папка пуста");
            }
            else Console.WriteLine($"Папка по указанному пути не существует, либо неверно указан путь к папке");
        }
        static double GetSizeDir(string path)
        {
            double SizeDir = 0;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    SizeDir += file.Length; // суммируем размер всех файлов папки
                }
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    SizeDir += GetSizeDir(Convert.ToString(dir)); // рекурсивно считаем размер всех вложенных папок и суммируем
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка:" + e.Message);
            }
            return SizeDir;
        }
    }
}
