using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreamReadLineSample
{
    public class InnerProcess
    {
        async static public Task ReadLineAsync(string path)
        {          

            using (StreamReader reader = File.OpenText(path))
            {
                while (await reader.ReadLineAsync() is string result)
                {

                    Console.WriteLine(result);
                    await Task.Delay(100);

                }

            }
        }
    }
}
