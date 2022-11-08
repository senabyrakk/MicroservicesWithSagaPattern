using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Model;
using System.Threading.Tasks;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext context;
        public StockController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GET()
        {
            return Ok(await context.Stock.ToListAsync());
        }
    }
}
