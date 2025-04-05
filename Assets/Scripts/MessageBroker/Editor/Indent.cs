using System;

namespace MessageBroker.Editor
{
    /// <summary>
    /// Manage indentation in dynamic code generation.
    /// </summary>
    internal static class Indent
    {
        private static int _count = 0;
        private static readonly char IndChar = '\t';

        public static void Init() => _count = 0;

        public static string Get() => new string(IndChar, _count);
        public static string Push() { _count++; return Get(); }
        public static string Pop() { _count--; _count = Math.Max(0, _count); return Get(); }
    }

}