using api.fernflowers.com.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosesController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public DosesController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var doses = await _vaccineDBContext.Doses.ToListAsync();
            return Ok(doses);
        }
    }
}
