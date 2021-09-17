using EShop.Data;
using EShop.DTOs;
using EShop.Entities;
using EShop.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public CarsController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            try
            {
                return Ok(await _context.Cars.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            try
            {
                var result = await _context.Cars.FindAsync(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("search-by-name")]
        public async Task<ActionResult<List<Car>>> GetCar(string name)
        {
            try
            {
                List<Car> result = await _context.Cars.QueryByName(name).ToListAsync();
                if (result.Count < 1) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody]Car car)
        {
            try
            {
                if (car == null) return BadRequest();
                var createdCar = _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriveing data from the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, [FromBody] CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest();
            }

            var carItem = await _context.Cars.FindAsync(id);

            if (carItem == null) return NotFound();

            carItem.Name = carDto.Name;
            carItem.Price = carDto.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id)) return NotFound();
            }
            return Ok();
        }

        private bool CarExists(int id) => _context.Cars.Any(e => e.Id == id);

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            Car car = await FindCar(id);
            
            if (car != null)
            {
                _context.Remove(_context.Cars.Single(car => car.Id == id));
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }

        private async Task<Car> FindCar(int carId)
        {
            if (carId == default) return null;
            return await _context.Cars.Where(x => x.Id == carId).FirstOrDefaultAsync();
        }
    }
}
