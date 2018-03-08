using Common.Logging;
using System.Collections.Concurrent;

namespace Banking
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ILog GetLogger<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        private readonly ConcurrentDictionary<string, ILog> _loggerDictionary = new ConcurrentDictionary<string, ILog>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILog GetLogger<T>()
        {
            return _loggerDictionary.GetOrAdd(typeof(T).FullName, LogManager.GetLogger<T>());
        }
    }
}