using System;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке, которую необходимо очистить");
            string path = Console.ReadLine();
            double countFiles = 0;
            double sizeFiles = 0;
            if (Directory.Exists(path))
            {
                Console.WriteLine($"Исходный размер паки: {GetSizeDir(path)} байт");
                Clean(path, ref sizeFiles, ref countFiles);  //очищаем папку, по ссылке передаем размер и количество удаляемых файлов
                Console.WriteLine("Очистка папки проведена");
                Console.WriteLine($"Освобождено: {sizeFiles} байт");
                Console.WriteLine($"Удалено: {countFiles} файлов");
                Console.WriteLine($"Текущий размер паки: {GetSizeDir(path)} байт");
            }
            else Console.WriteLine($"Папка по указанному пути {path} не существует, либо неверно указан путь к папке");
        }
        static void Clean(string path, ref double sizeFiles, ref double countFiles)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        sizeFiles += file.Length; // считаем размер файлов которые будем удалять
                        countFiles++;             // считаем количество файлов которые будем удалять
                        file.Delete();  // удаляем файлы которые не использовались более 30 минут
                    }
                }
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        Clean(dir.FullName, ref sizeFiles, ref countFiles); // рекурсивно очищаем все вложенные папки которые не использовались более 30 минут
                        dir.Delete();  // и удаляем их
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка:" + e.Message);
            }
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
