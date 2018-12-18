using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsycnStreamNET
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var path = "SourceFile.txt";
            await foreach (var item in AsyncEnumerableProcess.ReadLineAsync(path))
            {
                Console.WriteLine(item);
            };
            Console.ReadLine();
        }
    }
}
