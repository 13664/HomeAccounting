﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Home_Accounting.Data;
using Home_Accounting.Models;

namespace Home_Accounting.Controllers
{
    public class CategoryTypesController : Controller
    {
        private readonly TransactionsDbContext _context;

        public CategoryTypesController(TransactionsDbContext context)
        {
            _context = context;
        }

        // GET: CategoryTypes
        public async Task<IActionResult> Index()
        {
              return _context.Types != null ? 
                          View(await _context.Types.ToListAsync()) :
                          Problem("Entity set 'TransactionsDbContext.Types'  is null.");
        }

        // GET: CategoryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var categoryType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // GET: CategoryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] CategoryType categoryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryType);
        }

        // GET: CategoryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var categoryType = await _context.Types.FindAsync(id);
            if (categoryType == null)
            {
                return NotFound();
            }
            return View(categoryType);
        }

        // POST: CategoryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] CategoryType categoryType)
        {
            if (id != categoryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryTypeExists(categoryType.Id))
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
            return View(categoryType);
        }

        // GET: CategoryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var categoryType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // POST: CategoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Types == null)
            {
                return Problem("Entity set 'TransactionsDbContext.Types'  is null.");
            }
            var categoryType = await _context.Types.FindAsync(id);
            if (categoryType != null)
            {
                _context.Types.Remove(categoryType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryTypeExists(int id)
        {
          return (_context.Types?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
