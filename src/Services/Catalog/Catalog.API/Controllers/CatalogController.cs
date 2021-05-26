using System;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [Controller]
    public class CatalogController : ControllerBase
    {
        ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}