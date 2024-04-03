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
            _logger.LogInfo("��� �������������� ��������� �� ������ �����������.");
            _logger.LogDebug("��� ���������� ��������� �� ������ �����������.");
            _logger.LogWarning("��� ��������� � �������������� �� ������ �����������.");
            _logger.LogError("��� ��������� �� ������ �� ������ �����������.");

            return new string[] { "value1", "value2" };
        }
    }
}
