#if UNITY_EDITOR
namespace MessageLogger.Editor
{
    public enum Level
    {
        Assert,
        Debug,
        Information,
        Event,
        Warning,
        Exception,
        Error,
        Panic
    }
}
#endif