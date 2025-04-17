using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // For DbContext and EF Core
using GoTimelyAPI;

namespace GoTimelyAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TimetableController : ControllerBase
  {
      private readonly AppDbContext _context;

      public TimetableController(AppDbContext context)
      {
          _context = context;
      }

      [HttpGet]
      public async Task<IActionResult> GetTimetable()
      {
          var timetable = await _context.TimeTables
                                        .Include(t => t.Bus) // Include Bus details if needed
                                        .ToListAsync();

          return Ok(timetable);
      }

  }
}
