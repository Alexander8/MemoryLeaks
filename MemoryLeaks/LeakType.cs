namespace MemoryLeaks
{
    internal enum LeakType
    {
        Undefined = 0,
        AccumulatingManagedMemory = 1,
        AccumulatingEventSubscriptions = 2,
        AccumulatingUnmanagedMemory = 3
    }
}