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
    public class PersonalDataController : Controller
    {
        public PersonalDataController()
        {
            this._db = new Postgresql();
        }
        Postgresql _db;

        // GET api/personaldata
        [HttpGet]
        public async Task<ActionResult<List<PersonalData>>> Get(
            [FromQuery] int? count = 10,
            [FromQuery] int? offset = 0)
        {
            var personalData = await this._db.PersonalData.FromSqlRaw(
                $"SELECT * FROM public.\"PersonalData\" LIMIT {count} OFFSET {offset}")
                .ToListAsync();
            return personalData;
        }

        // GET api/personaldata/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalData>> Get(int? id)
        {
            if (id == null)
                return NotFound();
            var personalData = await this._db.PersonalData.FromSqlRaw(
                $"SELECT * FROM public.\"PersonalData\" WHERE \"Id\" = {id}")
                .ToListAsync();
            if (personalData.Count() == 0)
                return NotFound();
            return personalData[0];
        }

        // POST api/personaldata/new
        [HttpPost("new")]
        public async Task<ActionResult> New([FromBody] PersonalData personalData)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await this._db.PersonalData.AddAsync(personalData);
            await this._db.SaveChangesAsync();
            return Ok();
        }

        // Delete api/personaldata/1/delete
        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            PersonalData personalData = this._db.PersonalData
                .Where(o => o.Id == id)
                .FirstOrDefault();

            this._db.PersonalData.Remove(personalData);
            await this._db.SaveChangesAsync();
            return Ok();
        }
    }
}