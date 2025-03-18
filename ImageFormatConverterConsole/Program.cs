using ImageFormatConverterConsole;
using System.Drawing.Imaging;
public class Program
{
    private static List<string> fileDirectorys = new();
    public static void Main(params string[] args)
    {
        int currentPage = 1;
        int formatIndex = 0;
        List<string> files = args.ToList();

        if(args == null || args.Length == 0)
        {
            Console.WriteLine("There is no file, Please Drag and Drop the files or folder to Console.exe");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
            return;
        }
        
        for(int i = 0; i < files.Count; i++)
        {
            if (Directory.Exists(files[i]))
            {
                var insideFiles = Directory.GetFiles(files[i]);
                files.RemoveAt(i);
                files.AddRange(insideFiles);
                i--;
            }
        }

        foreach (var file in files)
        {
            if(ImageManager.IsFileImageFormat(file))
            {
                fileDirectorys.Add(file);
            }
        }
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("ARROW KEY FOR CHOOSE, SPACEBAR FOR SELECT");
            Console.WriteLine($"{files.Count} Files imported! \n");

            ConsoleManager.DrawList(fileDirectorys, currentPage);
            ConsoleManager.DrawFormats(formatIndex);


            var input = Console.ReadKey();
            currentPage += input.Key == ConsoleKey.RightArrow ? 1 : 0;
            currentPage -= input.Key == ConsoleKey.LeftArrow ? 1 : 0;
            var lastPage = (int)Math.Ceiling(fileDirectorys.Count / 10.0f);
            currentPage = Math.Clamp(currentPage, 1, lastPage);

            formatIndex -= input.Key == ConsoleKey.UpArrow ? 1 : 0;
            formatIndex += input.Key == ConsoleKey.DownArrow ? 1 : 0;
            var maxIndex = Enum.GetNames<TargetImageFormat>().Length - 1;
            formatIndex = Math.Clamp(formatIndex, 0, maxIndex);

            if(input.Key == ConsoleKey.Spacebar)
            {
                ImageManager.ExportFiles(fileDirectorys, (TargetImageFormat)formatIndex);

                Console.WriteLine($"Saved at {Path.Combine(Directory.GetCurrentDirectory(), "ConvertedImage")}");
                Console.WriteLine("\nPress any key to close");
                Console.ReadKey();
                return;
            }
        }
    }
}   