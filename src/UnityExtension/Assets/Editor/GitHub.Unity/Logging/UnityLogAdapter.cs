using GitHub.Api;
using System;
using System.Linq;

namespace GitHub.Unity
{
    class UnityLogAdapter : ILogging
    {
        private readonly string contextPrefix;

        private string Prefix
        {
            get
            {
                var time = DateTime.Now.ToString("HH:mm:ss tt");
                var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                return string.Format("{0} [{1}] <{2}> ", time, threadId, contextPrefix);
            }
        }

        public UnityLogAdapter(string context)
        {
            contextPrefix = string.IsNullOrEmpty(context) 
                ? "GitHub" 
                : string.Format("GitHub:{0}", context);
        }

        public UnityLogAdapter() : this(string.Empty)
        {
        }

        public void Info(string message)
        {
            UnityEngine.Debug.Log(Prefix + message);
        }

        public void Info(string format, params object[] objects)
        {
            UnityEngine.Debug.LogFormat(Prefix + format, objects);
        }

        public void Debug(string message)
        {
#if DEBUG
            UnityEngine.Debug.Log(Prefix + message);
#endif
        }

        public void Debug(string format, params object[] objects)
        {
#if DEBUG
            UnityEngine.Debug.LogFormat(Prefix + format, objects);
#endif
        }
        public void Debug(Exception ex)
        {
            var caller = Environment.StackTrace;
            var stack = caller.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (stack.Length > 2)
                caller = String.Join(Environment.NewLine, stack.Skip(2).ToArray());
            UnityEngine.Debug.Log(caller);
            UnityEngine.Debug.LogException(ex);
        }

        public void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(Prefix + message);
        }

        public void Warning(string format, params object[] objects)
        {
            UnityEngine.Debug.LogWarningFormat(Prefix + format, objects);
        }

        public void Error(string message)
        {
            UnityEngine.Debug.LogError(Prefix + message);
        }

        public void Error(string format, params object[] objects)
        {
            UnityEngine.Debug.LogErrorFormat(Prefix + format, objects);
        }
    }
}