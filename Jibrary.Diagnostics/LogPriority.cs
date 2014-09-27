namespace Jibrary.Diagnostics
{
    /// <summary>
    /// Used to describe how important a particular log is
    /// </summary>
    public enum LogPriority : int
    {
        Lowest = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        Highest = 4
    }
}
