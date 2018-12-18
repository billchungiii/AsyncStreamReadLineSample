using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace AsyncStreamReadLineSample
{
    public class DelegateProcess
    {
        async static public Task ReadLineAsync(string path, Action<string> action)
        {


            using (StreamReader reader = File.OpenText(path))
            {
                while (await reader.ReadLineAsync() is string result)
                {
                    action.Invoke(result);
                    await Task.Delay(100);
                }

            }
        }
    }
}
