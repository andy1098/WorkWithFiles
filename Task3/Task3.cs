using Tasks;

internal class Task3
{
    static int DeleteFiles(string path, ref long size)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fis = dir.GetFiles();
            int i = 0;
            if (fis.Length>0)
            { 
                foreach (FileInfo fi in fis)
                {
                    if (DateTime.Now - fi.LastWriteTime > TimeSpan.FromMinutes(30))
                    {
                        size += fi.Length; 
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
        long size = 0;
        try
        {
           DirectoryInfo dir = new DirectoryInfo(path);
           int numfilesdeleted = DeleteFiles(path, ref size);
           DirectoryInfo[] subdirs = dir.GetDirectories();
           if (subdirs.Length <= 0)
               Console.WriteLine("Поддиректории отсутствуют");
           else
           {
               foreach (DirectoryInfo subdir in subdirs)
               {
                   numfilesdeleted += DeleteFiles(subdir.FullName, ref size);
               }

               Console.WriteLine($"Было удалено {numfilesdeleted} файлов, размером {size} байт");
           }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void Main(string[] args)
    {
        Console.Write("Введите путь для расчета и очистки ->");
        String dirpath = Console.ReadLine();
        //String dirpath = "C:\\SkillFactory";

        try
        {
            DirectoryInfo d = new DirectoryInfo(dirpath);
            if (d.Exists)
            {
                Console.WriteLine($"Размер файлов в папке {dirpath} - {DirectorySize.DirSize(d)} байт");
                Delete30(dirpath);
                Console.WriteLine($"Размер оставшихся файлов в папке {dirpath} - {DirectorySize.DirSize(d)} байт");
            }
                
            else
                Console.WriteLine("Папка не найдена");
        }
        catch (Exception e)
        {
            Console.WriteLine($"не удалось рассчитать размер папки {dirpath}, ошибка {e.Message}");
        }

    }
}