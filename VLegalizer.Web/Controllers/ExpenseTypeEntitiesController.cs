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
    public class ExpenseTypeEntitiesController : Controller
    {
        private readonly DataContext _context;

        public ExpenseTypeEntitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: ExpenseTypeEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseTypeEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes.FindAsync(id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }

            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypeEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseTypeEntity expenseTypeEntity)
        {
            if (ModelState.IsValid)
            {
                expenseTypeEntity.ExpenseNames = expenseTypeEntity.ExpenseNames.ToUpper();
                _context.Add(expenseTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypeEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes.FindAsync(id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }
            return View(expenseTypeEntity);
        }

        // POST: ExpenseTypeEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseTypeEntity expenseTypeEntity)
        {
            if (id != expenseTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                expenseTypeEntity.ExpenseNames = expenseTypeEntity.ExpenseNames.ToUpper();
                _context.Update(expenseTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypeEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }

            _context.ExpenseTypes.Remove(expenseTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
