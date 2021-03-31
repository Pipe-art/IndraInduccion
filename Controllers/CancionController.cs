using Discografica.Context;
using Discografica.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Discografica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CancionController : ControllerBase
    {
        private readonly AppDbContext context;

        public CancionController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<CancionController>
        [HttpGet]
        public IEnumerable<Cancion> Get()
        {
            return context.Cancion.ToList();
        }

        // GET api/<CancionController>/5
        [HttpGet("{id}")]
        public Cancion Get(string id)
        {
            var Cancion = context.Cancion.FirstOrDefault(c=>c.can_codigo==id);
            return Cancion;
        }

        // POST api/<CancionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CancionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CancionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
