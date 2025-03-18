using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFormatConverterConsole;

public class ConsoleManager
{

    public static void DrawList(List<string> list,int pageNumber)
    {
        for(int i = 0; i < 10; i++)
        {
            var listIndex = i + ((pageNumber - 1) * 10);
            if(listIndex >= list.Count)
            {
                Console.WriteLine("");
                continue;
            }
            Console.WriteLine($"{list[listIndex]}");
        }
        Console.WriteLine($"Current Page [{pageNumber}/{(int)Math.Ceiling(list.Count / 10.0f)}]\n");
    }

    public static void DrawFormats(int index)
    {
        var names = Enum.GetNames<TargetImageFormat>();
        Console.WriteLine("SELECT FORMAT FOR EXPORT");
        for (int i = 0; i < names.Length; i++)
        {
            if (index == i)
            {
                Console.WriteLine($"▶{names[i]}");
                continue;
            }
            Console.WriteLine($"  {names[i]}");
        }
        Console.WriteLine("");
    }
}
