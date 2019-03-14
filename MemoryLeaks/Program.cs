using System;
using MemoryLeaks.LeakImpl;

namespace MemoryLeaks
{
    /// <summary>
    /// Sample usage (to leak 1 gb):
    /// dotnet MemoryLeaks.dll AccumulatingManagedMemory 1000
    /// dotnet MemoryLeaks.dll AccumulatingEventSubscriptions 1000
    /// dotnet MemoryLeaks.dll AccumulatingUnmanagedMemory 1000
    /// </summary>
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var leakType = GetChosenLeakType(args);
            var mbToLeak = GetMbToLeak(args);

            IMemoryLeak memoryLeak;

            switch (leakType)
            {             
                case LeakType.AccumulatingManagedMemory:
                    memoryLeak = new ManagedMemoryLeak();
                    break;
                case LeakType.AccumulatingEventSubscriptions:
                    memoryLeak = new EventSubscriptionsLeak();
                    break;
                case LeakType.AccumulatingUnmanagedMemory:
                    memoryLeak = new UnmanagedMemoryLeak();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            memoryLeak.Leak(mbToLeak);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static LeakType GetChosenLeakType(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return LeakType.Undefined;
            }

            return Enum.TryParse(args[0], true, out LeakType leakType) 
                ? leakType 
                : LeakType.Undefined;
        }

        private static int GetMbToLeak(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                return -1;
            }

            return int.TryParse(args[1], out var mb)
                ? mb
                : -1;
        }
    }
}
