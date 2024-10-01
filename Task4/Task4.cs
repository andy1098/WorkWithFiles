using System.ComponentModel;
using System.IO.Enumeration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

[Serializable] 
internal class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public DateTime Date { get; set; }
    public int AvRating { get; set; }
    public Student()    {    }
    public Student(string name, string group, string date, int avr)
    {
        Name = name;
        Group = group;
        Date = DateTime.Parse(date); // в реальной задаче необходимо использовать DateTime.ParseExact или DateTime.TryParseExact с установкой форматов. 
        AvRating = avr;
    }
}
internal class Task4
{
    static void EnterDat(string filename)
    {
        var stud = new Student("Даниил", "103", "1977-12-31", 4);
        Console.WriteLine("Объект создан");

        using (var stream = File.Open(filename, FileMode.Append))
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                writer.Write(stud.Name);
                writer.Write(stud.Group);
                writer.Write(stud.Date.ToString());
                writer.Write(stud.AvRating);
            }
        }
        Console.WriteLine("Объект записан");
    }

    static List<Student> WriteDat(string filename)
    {
        List <Student> students = new List<Student>();
        try
        {
            using (var stream = File.Open(filename, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    while (reader.PeekChar() != -1)
                    {
                        var st = new Student();
                        st.Name = reader.ReadString();
                        st.Group = reader.ReadString();
                        st.Date = DateTime.Parse(reader.ReadString());
                        st.AvRating = reader.ReadInt32();

                        //Console.WriteLine($"Имя - {st.Name}, Группа - {st.Group}, Дата {st.Date.ToString("d")}, Средний балл - {st.AvRating}");
                        students.Add(st);
                    }
                }
            }
            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return students;
        }
    }
    private static void Main(string[] args)
    {
        string filename = "C:\\SkillFactory\\students.dat";
        //EnterDat(filename);
        var students = WriteDat(filename);
        try
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string stfolder = Path.Combine(desktop, "Students");
            if (!Directory.Exists(stfolder))
            {
                Directory.CreateDirectory(stfolder);
                Console.WriteLine($"Путь к папке - {stfolder}");
            }
            else
            {
                Console.WriteLine("Папка уже существует: " + stfolder);
            }
            
            foreach (Student st in students)
            {
                string filePath = stfolder + "\\группа " + st.Group + ".txt";
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"Имя {st.Name}, дата рождения {st.Date.ToString("d")}, средний балл {st.AvRating}");
                }
                Console.WriteLine($"Студент {st.Name} записан в файл {filePath}");
            }
        }
        catch (Exception ex) { Console.WriteLine("Произошла ошибка" + ex.ToString()); }
    }
}