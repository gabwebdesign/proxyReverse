using Microsoft.AspNetCore.Mvc;

namespace proyectoCursoDotNet.Controllers
{
    public class ProxyController : Controller
    {
        
        private readonly IProxyService _proxyService;

        public ProxyController(IProxyService service)
        {
            _proxyService = service;
        }

        // GET: proxyController
        [HttpGet]
        public async Task<string> Get()
        {
            return await _proxyService.GetAuthorAsync();
        }
        
    }
}
