using Microsoft.AspNetCore.Mvc;

namespace proyectoCursoDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProxyController: ControllerBase
    {
        private readonly IProxyService _proxyService;

        public ProxyController(IProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        // GET: proxyController
        [HttpGet]
        public async Task<string> Get()
        {
            return await _proxyService.GetAuthorAsync();
        }
        
    }
}
