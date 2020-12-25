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
    public class EmployeesController : Controller
    {
        public EmployeesController()
        {
            this._db = new Postgresql();
        }

        Postgresql _db;

        // GET api/employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get(
            [FromQuery] int? count = 10,
            [FromQuery] int? offset = 0)
        {
            var employees = await this._db.Employees.FromSqlRaw(
                $"SELECT * FROM public.\"Employees\" LIMIT {count} OFFSET {offset}")
                .ToListAsync();
            return employees;
        }

        // GET api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int? id)
        {
            if (id == null)
                return BadRequest();
            var employees = await this._db.Employees.FromSqlRaw(
                $"SELECT * FROM public.\"Employees\" WHERE \"Id\" = {id}")
                .ToListAsync();
            if (employees.Count() == 0)
                return NotFound();
            return employees[0];
        }

        // POST api/employees/new
        [HttpPost("new")]
        public async Task<ActionResult> New([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var id = employee.Id;
            var firstName = employee.FirstName;
            var lastName = employee.LastName;
            var positionId = employee.PositionId;
            await this._db.Employees.AddAsync(employee);
            await this._db.SaveChangesAsync();
            return Ok();
        }
    }
}