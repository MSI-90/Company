using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Вот информационное сообщение от нашего контроллера.");
            _logger.LogDebug("Вот отладочное сообщение от нашего контроллера.");
            _logger.LogWarning("Вот сообщение о предупреждении от нашего контроллера.");
            _logger.LogError("Вот сообщение об ошибке от нашего контроллера.");

            return new string[] { "value1", "value2" };
        }
    }
}
