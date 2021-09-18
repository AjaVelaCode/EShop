using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private static readonly string fileName = "C:/Users/Andrija/Desktop/TaskDocu/TietoEvry/JsonComputers.json";

        [HttpGet]
        public async Task<Computers> GetData()
        {
            Computers items = await ParseJsonFile();

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerData>> Get(int id)
        {
            Computers items = await ParseJsonFile();

            try
            {
                var result = items.computersList.Where(computer => computer.Id == id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from file");
            }
        }

        // POST api/<ComputersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ComputersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComputersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private static async Task<Computers> ParseJsonFile()
        {
            Computers items = new Computers();

            try
            {
                using StreamReader r = new StreamReader(fileName);
                string json = await r.ReadToEndAsync();
                items.computersList = JsonConvert.DeserializeObject<List<ComputerData>>(json);
                return items;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
