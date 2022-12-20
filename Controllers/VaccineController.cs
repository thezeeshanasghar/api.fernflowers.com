using api.fernflowers.com.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public VaccineController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var vaccines = await _vaccineDBContext.Vaccines.ToListAsync();
            return Ok(vaccines);
        }
    }
}
