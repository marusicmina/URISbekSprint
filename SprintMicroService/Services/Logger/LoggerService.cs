using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using SprintMicroService.Entites;
using SprintMicroService.Services.Logger;
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace SprintMicroService.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration _configuration;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Log(LogLevel level, string method, string message, Exception error = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    string url = _configuration.GetValue<string>("Services:LoggerMicroService");

                    var log = new Log
                    {
                        Level = level,
                        Message = message,
                        Error = error ?? new Exception("UnknownError"),
                        Service = "SprintMicroService",
                        Method = method
                    };

                    string logJson = JsonConvert.SerializeObject(log);

                    HttpContent content = new StringContent(logJson);
                    Console.WriteLine(content);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Content: {response.Content.ReadAsStringAsync().Result}");
                }
            }
            catch (AggregateException e)
            {
                foreach (var innerException in e.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }
    }
}
