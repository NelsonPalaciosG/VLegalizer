using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VLegalizer.Web.Data;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Models;

namespace VLegalizer.Web.Controllers
{
    public class TripEntitiesController : Controller
    {
        private readonly DataContext _context;

        public TripEntitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: TripEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips
                .Include(t => t.Employee)
                .Include(t => t.TripDetails)
                .Select(t => new TripViewModel()
            {
                Id = t.Id,
                City = t.City,
                EndDate = t.EndDate,
                StartDate = t.StartDate,
                EmployeeName = t.Employee.FullName,
                TotalAmount = t.TripDetails.Sum(td => td.Amount)
            }).ToListAsync());
        }

        // GET: TripEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripEntity = await _context.Trips
            .Include(t => t.TripDetails)
            .ThenInclude(td => td.ExpenseType)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            return View(tripEntity);
        }

        // GET: TripEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,TotalAmount,City")] TripEntity tripEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripEntity);
        }

        // GET: TripEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripEntity = await _context.Trips.FindAsync(id);
            if (tripEntity == null)
            {
                return NotFound();
            }
            return View(tripEntity);
        }

        // POST: TripEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,TotalAmount,City")] TripEntity tripEntity)
        {
            if (id != tripEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripEntityExists(tripEntity.Id))
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
            return View(tripEntity);
        }

        // GET: TripEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripEntity = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(tripEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
