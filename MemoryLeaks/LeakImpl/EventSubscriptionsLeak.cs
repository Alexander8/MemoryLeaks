using System;

namespace MemoryLeaks.LeakImpl
{
    internal sealed class EventSubscriptionsLeak : IMemoryLeak
    {
        private static readonly EventSource Source = new EventSource();

        public void Leak(int megabytes = -1)
        {
            var leakedMb = 0;
            while (megabytes == -1 || leakedMb < megabytes)
            {
                var eventHandler = new EventHandler();
                Source.Event += eventHandler.Handler;
                ++leakedMb;
            }
        }

        private sealed class EventSource
        {
            public event Action Event;
        }

        private sealed class EventHandler
        {
            private readonly byte[] _buffer = new byte[1024 * 1024];

            public EventHandler()
            {
                for (var i = 0; i < _buffer.Length; ++i)
                {
                    _buffer[i] = (byte)(i % 256);
                }
            }

            public void Handler()
            {
            }
        }
    }
}
