using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Смепл;



class Converttt
{
    static void Main ()
    {
        
        ConsoleKeyInfo key;
        List<Angar> ang = new List<Angar>();
        


        Console.WriteLine("Какой файл вы хотите выбрать.");
        string path = Console.ReadLine();

        if (path.EndsWith(".txt"))
        {
            string[] fileContent = File.ReadAllLines(path);

            for (int i = 0; i < fileContent.Length; i += 2)
            {

                Angar tank = new Angar();
                tank.Name = fileContent[i];
                tank.Colvo = int.Parse(fileContent[i + 1]);
                
                ang.Add(tank);


            }
            Console.Clear();
            Console.WriteLine("Чтобы перейти к выбору сохранений нажмите F1. Закрыть программу - Escape");
            Console.WriteLine("Содержимое файла:");
        }
        else if (path.EndsWith(".xml"))
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Angar>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                ang = (List<Angar>)xml.Deserialize(fs);

            Console.Clear();
            Console.WriteLine("Чтобы перейти к выбору сохранений нажмите F1. Закрыть программу - Escape");
            Console.WriteLine("Содержимое файла:");
        }
        else if (path.EndsWith(".json"))
        {
            string text = File.ReadAllText(path);
            ang = JsonConvert.DeserializeObject<List<Angar>>(text);

            Console.Clear();
            Console.WriteLine("Чтобы перейти к выбору сохранений нажмите F1. Закрыть программу - Escape");
            Console.WriteLine("Содержимое файла:");
        }
        foreach (var item in ang)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.Colvo);
        }
        
        key = Console.ReadKey();
        if (key.Key == ConsoleKey.F1)
        {
            Console.Clear();
            Console.WriteLine("В какой формат сохраняем?");
            path = Console.ReadLine();

            if (path.EndsWith(".txt"))
            {

                foreach (var item in ang)
                {
                    File.AppendAllText(path, item.Name + "\n");
                    File.AppendAllText(path, item.Colvo.ToString() + "\n");
                }

            }
            else if (path.EndsWith(".xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Angar>));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, ang);
                }

            }
            else if (path.EndsWith(".json"))
            {
                
                string json = JsonConvert.SerializeObject(ang);
                
                File.WriteAllText(path, json);  
            }
        }
        else if (key.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0); 
        }

    }
}