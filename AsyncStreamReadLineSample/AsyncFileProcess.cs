using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreamReadLineSample
{
    sealed class AsyncFileProcess : IAsyncEnumerable<string>, IAsyncEnumerator<string>
    {
        private readonly StreamReader _reader;

        private bool _disposed;
        public AsyncFileProcess(string path)
        {
            _reader = File.OpenText(path);
            _disposed = false;
        }

        public string Current { get; private set; }
        public IAsyncEnumerator<string> GetAsyncEnumerator()
        {
            return this;
        }
        async public ValueTask<bool> MoveNextAsync()
        {
            await Task.Delay(100);
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
            if (!_disposed)
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
