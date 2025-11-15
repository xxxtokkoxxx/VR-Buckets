namespace _VRBuckets.CodeBase.Debug
{
    public static class AppLogger
    {
        public static void Log(LogCategory category, string message)
        {
            UnityEngine.Debug.Log($"[{category}]: {message}");
        }

        public static void LogWarning(LogCategory category, string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public static void LogError(LogCategory category, string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}