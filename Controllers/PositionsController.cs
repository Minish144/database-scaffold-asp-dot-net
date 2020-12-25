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
    public class PositionsController : Controller
    {
        public PositionsController()
        {
            this._db = new Postgresql();
        }
        Postgresql _db;

        // GET api/positions
        [HttpGet]
        public async Task<ActionResult<List<Position>>> Get(
            [FromQuery] int? count = 10,
            [FromQuery] int? offset = 0)
        {
            var positions = await this._db.Positions.FromSqlRaw(
                $"SELECT * FROM public.\"Positions\" LIMIT {count} OFFSET {offset}")
                .ToListAsync();
            return positions;
        }

        // GET api/positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> Get(int? id)
        {
            if (id == null)
                return NotFound();
            var positions = await this._db.Positions.FromSqlRaw(
                $"SELECT * FROM public.\"Positions\" WHERE \"Id\" = {id}")
                .ToListAsync();
            if (positions.Count() == 0)
                return NotFound();
            return positions[0];
        }

        // POST api/positions/new
        [HttpPost("new")]
        public async Task<ActionResult> New([FromBody] Position position)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await this._db.Positions.AddAsync(position);
            await this._db.SaveChangesAsync();
            return Ok();
        }

        // Delete api/positions/1/delete
        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Position position = this._db.Positions
                .Where(o => o.Id == id)
                .FirstOrDefault();

            this._db.Positions.Remove(position);
            await this._db.SaveChangesAsync();
            return Ok();
        }
    }
}