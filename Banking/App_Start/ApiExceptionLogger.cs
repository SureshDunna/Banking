using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Banking.Models;
using Common.Logging;
using Newtonsoft.Json;

namespace Banking.App_Start
{
    /// <summary>
    /// Represents implementation of <see cref="ExceptionLogger"/>.
    /// </summary>
    public class ApiExceptionLogger : ExceptionLogger
    {
        private readonly ILog _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ApiExceptionLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<ApiExceptionLogger>();
        }

        /// <summary>
        /// Overrides <see cref="ExceptionLogger.LogAsync"/> method with custom logger implementations.
        /// </summary>
        /// <param name="context">Instance of <see cref="ExceptionLoggerContext"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var request = await CreateRequest(context.Request);

            _logger.Error($"An unknown error occured and error is {context.Exception} and the request sent when error occured is {JsonConvert.SerializeObject(request)}");
        }

        private static async Task<HttpRequestModel> CreateRequest(HttpRequestMessage message)
        {
            var request = new HttpRequestModel
            {
                Body = await message.Content.ReadAsStringAsync(),
                Method = message.Method.Method,
                Scheme = message.RequestUri.Scheme,
                Host = message.RequestUri.Host,
                Protocol = string.Empty,
                PathBase = message.RequestUri.PathAndQuery,
                Path = message.RequestUri.AbsoluteUri,
                QueryString = message.RequestUri.Query
            };

            return request;
        }
    }
}