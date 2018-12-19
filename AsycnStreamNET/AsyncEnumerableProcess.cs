using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsycnStreamNET
{
    public class AsyncEnumerableProcess
    {
        async static public IAsyncEnumerable<string> ReadLineAsync(string path)
        {
            var enumerator = new AsyncEnumerator(path);
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    await Task.Delay(100);
                    yield return enumerator.Current;
                }
            }
            finally
            {
               await enumerator.DisposeAsync();
            }            
        }
    }

    public class AsyncEnumerator : IAsyncEnumerator<string>
    {
        private readonly StreamReader _reader;

        private bool _disposed;

        public string Current { get; private set; }

        public AsyncEnumerator(string path)
        {
            _reader = File.OpenText(path);
            _disposed = false;
        }
        async public ValueTask<bool> MoveNextAsync()
        {
            var result = await _reader.ReadLineAsync();
            Current = result;
            return result != null;
        }
        async public ValueTask DisposeAsync()
        {
            await Task.Run(() => Dispose());            
        }

        private void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (_reader != null)
                {
                    _reader.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
