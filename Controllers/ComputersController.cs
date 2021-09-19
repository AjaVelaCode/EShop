using EShop.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Extensions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private static readonly string fileName = "C:/Users/Andrija/Desktop/Task/Tieto/EShop/JsonFiles/JsonComputers.json";

        [HttpGet]
        public async Task<Computers> GetAllComputers()
        {
            Computers items = await ParseJsonFile();
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerData>> GetComputerById(int id)
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

        [HttpGet("search-by-name")]
        public async Task<ActionResult<Computers>> GetComputerByName(string name)
        {
            Computers items = await ParseJsonFile();

            try
            {
                List<ComputerData> result = items.computersList.Where(computer => computer.Name == name).ToList();
                if (result.Count < 1) return NotFound("Not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Not valid request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Computers>> CreatComputer([FromBody] ComputerData computer)
        {
            // deserialize json
            Computers items = await ParseJsonFile();

            try
            {
                items.computersList.Add(computer);
                return Ok(GetSerializedFile(items));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriveing data from the database");
            }
        }



        // PUT api/<ComputersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutComputer(int id, [FromBody] ComputerDto computerDto)
        {
            if (id != computerDto.Id)
            {
                return BadRequest("Not valid ID");
            }

            Computers items = await ParseJsonFile();
            ComputerData computerToUpdate = FindComputer(id, items);

            if (computerToUpdate == null) return NotFound();

            computerToUpdate.Name = computerDto.Name;
            computerToUpdate.Price = computerDto.Price;

            return Ok(GetSerializedFile(items));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComputer(int id)
        {
            Computers items = await ParseJsonFile();
            ComputerData computerToDelete = FindComputer(id, items);

            if (computerToDelete != null)
            {
                items.computersList.Remove(computerToDelete);
                return Ok(GetSerializedFile(items));
            }
            else return BadRequest("Not valid ID");
        }

        private static string GetSerializedFile(Computers items)
        {
            // serialize json
            var convertedJson = JsonConvert.SerializeObject(items.computersList, Formatting.Indented);
            // save json file
            System.IO.File.WriteAllText(fileName, convertedJson);
            return convertedJson;
        }

        private static ComputerData FindComputer(int id, Computers items)
        {
            return items.computersList
                            .FirstOrDefault(computer => computer.Id == id);
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
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException(e.Message);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
