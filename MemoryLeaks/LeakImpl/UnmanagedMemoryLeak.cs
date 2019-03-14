using System.Runtime.InteropServices;

namespace MemoryLeaks.LeakImpl
{
    internal sealed class UnmanagedMemoryLeak : IMemoryLeak
    {
        public void Leak(int megabytes = -1)
        {
            var leakedMb = 0;
            while (megabytes == -1 || leakedMb < megabytes)
            {
                var ptr = Marshal.AllocHGlobal(1024 * 1024);

                for (var i = 0; i < 1024 * 1024 / 64; ++i)
                {
                    Marshal.WriteInt64(ptr, i * 64, i);
                }

                ++leakedMb;
            }
        }
    }
}
