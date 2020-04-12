using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VLegalizer.Web.Data;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Controllers
{
    public class TripDetailEntitiesController : Controller
    {
        private readonly DataContext _context;

        public TripDetailEntitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: TripDetailEntities
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.TripDetails.Include(td => td.Trip).Where(td => td.Trip.Id == id).ToListAsync());
        }

        // GET: TripDetailEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }

            return View(tripDetailEntity);
        }

        // GET: TripDetailEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripDetailEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Description,Amount,PicturePath")] TripDetailEntity tripDetailEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripDetailEntity);
        }

        // GET: TripDetailEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails.FindAsync(id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }
            return View(tripDetailEntity);
        }

        // POST: TripDetailEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,Amount,PicturePath")] TripDetailEntity tripDetailEntity)
        {
            if (id != tripDetailEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripDetailEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripDetailEntityExists(tripDetailEntity.Id))
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
            return View(tripDetailEntity);
        }

        // GET: TripDetailEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }

            return View(tripDetailEntity);
        }

        // POST: TripDetailEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripDetailEntity = await _context.TripDetails.FindAsync(id);
            _context.TripDetails.Remove(tripDetailEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripDetailEntityExists(int id)
        {
            return _context.TripDetails.Any(e => e.Id == id);
        }
    }
}
