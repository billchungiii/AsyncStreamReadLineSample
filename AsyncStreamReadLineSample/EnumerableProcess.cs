using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreamReadLineSample
{
    public class EnumerableProcess
    {
        async static public Task<IEnumerable<string>> ReadLineAsync(string path)
        {           
            List<string> list = new List<string>();
            using (StreamReader reader = File.OpenText(path))
            {
                while (await reader.ReadLineAsync() is string result)
                {
                    list.Add(result);
                    await Task.Delay(100);
                }
            }
            return list;
        }
    }
}
