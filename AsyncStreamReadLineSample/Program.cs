using System;
using System.Threading.Tasks;

namespace AsyncStreamReadLineSample
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var path = "SourceFile.txt";           

            await Run(InnerProcess.ReadLineAsync(path), "InnerProcess");
            await Run(DelegateProcess.ReadLineAsync(path, (x) => Console.WriteLine(x)), "DelegateProcess");
            await Run(Task.Run(async () =>
            {
                var r = await EnumerableProcess.ReadLineAsync(path);
                foreach (var item in r)
                {
                    Console.WriteLine(item);
                }
            }), "EnumerableProcess");
            await Run(Task.Run(async ()=>
            {
                await foreach (var item in AsyncEnumerableProcess.ReadLineAsync(path))
                {
                    Console.WriteLine(item);
                };
               
            }), "AsyncEnumerableProcess");

            Console.ReadLine();
        }
        async static Task Run(Task task , string processName)
        {
            Console.WriteLine($"==={processName} Start===");
            await task;
            Console.WriteLine($"==={processName} End===");
            Console.WriteLine();
        }

    }
}
