class Program
{
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

        string[] files = Directory.GetFiles(folderPath);

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
            FileInfo fileInfo = new (file);
            DateTime creationDate = fileInfo.CreationTime;
            string isoDateTime = creationDate.ToString("yyyy-MM-ddTHH-mm-ss");
            string extension = fileInfo.Extension;
            string newFileName = $"{isoDateTime}{extension}";
            string fileoutPath = Path.Combine(outPath, newFileName);

            if (File.Exists(fileoutPath))
            {
                continue;
            }

            File.Copy(file, fileoutPath);
            Console.WriteLine($"{file} -> {fileoutPath}");
        }
    }
}
