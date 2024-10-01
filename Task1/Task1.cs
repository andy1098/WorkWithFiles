internal class Task1
{
    static int DeleteFiles(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fis = dir.GetFiles();
            int i = 0;
            if (fis.Length > 0)
            {
                foreach (FileInfo fi in fis)
                {
                    if (DateTime.Now - fi.LastWriteTime > TimeSpan.FromMinutes(30))
                    {
                        fi.Delete();
                        i++;
                    }
                }
                return i;
            }
            else
            {
                Console.WriteLine($"В директории {path} нет файлов");
                return 0;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка {ex.Message}");
            return 0;
        }
    }

    static void Delete30(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                int numfilesdeleted = DeleteFiles(path);
                DirectoryInfo[] subdirs = dir.GetDirectories();
                if (subdirs.Length <= 0)
                    Console.WriteLine("Поддиректории отсутствуют");
                else
                {
                    foreach (DirectoryInfo subdir in subdirs)
                    {
                        numfilesdeleted += DeleteFiles(subdir.FullName);
                    }
                }

                Console.WriteLine($"Было удалено {numfilesdeleted} файлов");
            }
            else
                Console.WriteLine("Каталог не найден");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Main(string[] args)
    {
        Console.Write("Введите путь для удаления неиспользуемых файлов ->");
        String dirpath = Console.ReadLine();
        //String dirpath = "C:\\SkillFactory";
        Delete30(dirpath);

    }
}