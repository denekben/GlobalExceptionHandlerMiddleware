using GlobalErrorApp.Exceptions;
using GlobalErrorApp.Interfaces;
using GlobalErrorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorApp.Controllers {
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : Controller {
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService, ILogger<DriverController> logger) {
            _driverService = driverService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> DriverList() {
            var drivers = await _driverService.GetDrivers();
            return Ok(drivers);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(Driver driver) {
            var result = await _driverService.AddDriver(driver);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> GetDriverById(int id) {
            var driver = await _driverService.GetDriverById(id);

            if(driver==null) {
                // return NotFound();
                throw new NotFoundException("This id is not valid");
            }
            return Ok(driver);
        }
    }
}
