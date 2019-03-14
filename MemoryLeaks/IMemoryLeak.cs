namespace MemoryLeaks
{
    internal interface IMemoryLeak
    {
        /// <summary>
        /// Method produces memory leak
        /// </summary>
        /// <param name="megabytes">Number of megabytes to leak (-1 means all accessible memory)</param>
        void Leak(int megabytes = -1);
    }
}
