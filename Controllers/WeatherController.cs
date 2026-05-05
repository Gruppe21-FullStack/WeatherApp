using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly WeatherService _weatherService;

        public WeatherController(ApplicationDbContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Weather
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Weathers.Include(w => w.City);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetFromApi(int cityId)
        {
            var city = await _context.Cities.FindAsync(cityId);

            if (city == null)
            {
                return NotFound();
            }

            var result = await _weatherService.GetWeatherParsed(city.Latitude, city.Longitude);

            var weather = new Weather
            {
                Temperature = result.temp,
                Description = result.description,
                Date = DateTime.Parse(result.time),
                CityId = city.Id
            };

            _context.Weathers.Add(weather);
            await _context.SaveChangesAsync();

            return View("Details", weather);
        }

        // GET: Weather/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers
                .Include(w => w.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // GET: Weather/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Weather/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Temperature,Description,Date,CityId")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", weather.CityId);
            return View(weather);
        }

        // GET: Weather/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", weather.CityId);
            return View(weather);
        }

        // POST: Weather/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Temperature,Description,Date,CityId")] Weather weather)
        {
            if (id != weather.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherExists(weather.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", weather.CityId);
            return View(weather);
        }

        // GET: Weather/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers
                .Include(w => w.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // POST: Weather/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weather = await _context.Weathers.FindAsync(id);
            if (weather != null)
            {
                _context.Weathers.Remove(weather);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherExists(int id)
        {
            return _context.Weathers.Any(e => e.Id == id);
        }
    }
}