using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace database_scaffold_asp_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController
    {

        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(
            [FromQuery] int? count = 10,
            [FromQuery] int? offset = 0)
        {
            return new string[] { "employee1", "employee2" };
        }

        // GET api/employees/5
        [HttpGet("{id}")]
        public ActionResult<string> Get()
        {
            return "value";
        }
    }
}