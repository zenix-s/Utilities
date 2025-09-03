internal enum NameOrigin
{
    creation,
    modification
};

class Program
{

    private static NameOrigin nameOrigin = NameOrigin.creation;
    private const string outputPath = "./output";
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Se necesita el directio");
            Console.WriteLine("Pulsa cualquier tecla para continuar");
            Console.ReadKey();
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Directio no existe");
            Console.WriteLine("Pulsa cualquier tecla para continuar");
            Console.ReadKey();
            return;
        }

        Console.Write("M-Modificaion / C-Creacion: ");
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
        Console.WriteLine();
        if (consoleKeyInfo.KeyChar == 'M')
            nameOrigin = NameOrigin.modification;
        else 
            nameOrigin = NameOrigin.creation;

        string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            Console.WriteLine($"El directorio {folderPath} no tiene ficheros");
            Console.WriteLine("Pulsa cualquier tecla para continuar");
            Console.ReadKey();
            return;
        }

        string outPath = Path.Combine(folderPath, outputPath);

        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }

        foreach (string file in files)
        {
            FileInfo fileInfo = new(file);
            DateTime fileDate = nameOrigin switch{
                NameOrigin.creation => fileInfo.CreationTime,
                NameOrigin.modification => fileInfo.LastWriteTime,
                _ => fileInfo.CreationTime
            } ;
            string isoDateTime = fileDate.ToString("yyyy-MM-ddTHH-mm-ss");
            string extension = fileInfo.Extension;
            string newFileName = $"{isoDateTime}{extension}";
            string fileoutPath = Path.Combine(outPath, newFileName);

            int counter = 1;
            while (File.Exists(fileoutPath))
            {
                newFileName = $"{isoDateTime}-{counter}{extension}";
                fileoutPath = Path.Combine(outPath, newFileName);
                counter++;
            }

            File.Copy(file, fileoutPath);
            Console.WriteLine($"{file} -> {fileoutPath}");
        }
    }
}
