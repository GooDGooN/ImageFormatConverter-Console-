using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageFormatConverterConsole;
public enum TargetImageFormat
{
    png,
    gif,
    jpeg,
    bmp,
    webp,
    icon,
}
public class ImageManager
{
    private static ImageFormat[] formats = [
        ImageFormat.Png,
        ImageFormat.Gif,
        ImageFormat.Jpeg,
        ImageFormat.Bmp,
        ImageFormat.Webp,
        ImageFormat.Icon,
    ];

    public static bool IsFileImageFormat(string directory)
    {
        try
        {
            using (var image = Image.FromFile(directory))
            {
                if (image != null)
                {
                    return true;
                }

                return false;
            }
        }
        catch 
        {
            return false;
        }
    }

    public static void ExportFiles(IEnumerable<string> directorys, TargetImageFormat targetFormat)
    {
        var folderDiretory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConvertedImage");
        if (!Directory.Exists(folderDiretory))
        {
            Directory.CreateDirectory(folderDiretory);
        }

        var formatStr = targetFormat.ToString();
        var dupCount = 0;
        var createdCount = 0;

        foreach (var file in directorys)
        {
            var fileName = Path.Combine(folderDiretory, Path.GetFileName(file));
            fileName = Path.ChangeExtension(fileName, formatStr);

            try
            {
                using (var target = Image.FromFile(file))
                {
                    if (!Path.Exists(fileName))
                    {
                        target.Save(fileName, formats[(int)targetFormat]);
                        createdCount++;
                    }
                    else
                    {
                        dupCount++;
                    }
                }
            }
            catch
            {
                Console.WriteLine("\n!!!WARNING!!!");
                Console.WriteLine($"{fileName}\nThere is no Folder!\ndid you delete folder while converting?\n");
            }
        }

        if (dupCount > 0)
        {
            Console.WriteLine($"\nignored {dupCount} file because overlap");
        }

        Console.WriteLine($"Successfully exported {createdCount} Files!");
    }
}
