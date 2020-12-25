using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using database_scaffold_asp_dot_net.Models;
using database_scaffold_asp_dot_net.Database;

namespace database_scaffold_asp_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessTripsController : Controller
    {
        public BusinessTripsController()
        {
            this._db = new Postgresql();
        }
        Postgresql _db;

        // GET api/businesstrips
        [HttpGet]
        public async Task<ActionResult<List<BusinessTrip>>> Get(
            [FromQuery] int? count = 10,
            [FromQuery] int? offset = 0)
        {
            var businessTrips = await this._db.BusinessTrips.FromSqlRaw(
                $"SELECT * FROM public.\"BusinessTrips\" LIMIT {count} OFFSET {offset}")
                .ToListAsync();
            return businessTrips;
        }

        // GET api/businesstrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessTrip>> Get(int? id)
        {
            if (id == null)
                return NotFound();
            var businessTrips = await this._db.BusinessTrips.FromSqlRaw(
                $"SELECT * FROM public.\"BusinessTrips\" WHERE \"Id\" = {id}")
                .ToListAsync();
            if (businessTrips.Count() == 0)
                return NotFound();
            return businessTrips[0];
        }

        // POST api/businesstrips/new
        [HttpPost("new")]
        public async Task<ActionResult> New([FromBody] BusinessTrip businessTrip)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await this._db.BusinessTrips.AddAsync(businessTrip);
            await this._db.SaveChangesAsync();
            return Ok();
        }

        // Delete api/businesstrips/1/delete
        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            Console.WriteLine(id);
            if (id == null)
                return NotFound();
            BusinessTrip businessTrip = this._db.BusinessTrips
                .Where(o => o.Id == id)
                .FirstOrDefault();

            this._db.BusinessTrips.Remove(businessTrip);
            await this._db.SaveChangesAsync();
            return Ok();
        }
    }
}