using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreUseMailKit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task Get()
        {
            await _emailSender.SendAsync("bidianqing@qq.com", "test", "body");
        }
    }
}
