using Tasks;

internal class Task2
{
    private static void Main(string[] args)
    {
        Console.Write("Введите путь для расчета размера файлов в папке ->");
        String dirpath = Console.ReadLine();
        //String dirpath = "C:\\SkillFactory";
        
        try
        {
            DirectoryInfo d = new DirectoryInfo(dirpath);
            if (d.Exists)
                Console.WriteLine($"Размер файлов в папке {dirpath} - {DirectorySize.DirSize(d)} байт");
            else
                Console.WriteLine("Папка не найдена");
        }
        catch (Exception e) 
        {
            Console.WriteLine($"не удалось рассчитать размер папки {dirpath}, ошибка {e.ToString()}"); 
        }    

    }
}