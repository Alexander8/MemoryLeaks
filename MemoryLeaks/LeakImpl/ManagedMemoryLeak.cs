using System;
using System.Collections.Generic;

namespace MemoryLeaks.LeakImpl
{
    internal sealed class ManagedMemoryLeak : IMemoryLeak
    {
        private static readonly List<Buffer> Buffers = new List<Buffer>();

        public void Leak(int megabytes = -1)
        {
            var leakedMb = 0;
            while (megabytes == -1 || leakedMb < megabytes)
            {
                Buffers.Add(new Buffer(1024 * 1024));
                ++leakedMb;
            }
        }

        private class Buffer
        {
            private readonly byte[] _buffer;

            public Buffer(int capacity)
            {
                if (capacity < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(capacity));
                }

                _buffer = new byte[capacity];

                for (var i = 0; i < _buffer.Length; ++i)
                {
                    _buffer[i] = (byte)(i % 256);
                }
            }
        }
    }
}
