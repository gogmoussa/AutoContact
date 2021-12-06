using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoContact.Models;
using AutoContact.Helpers;
using System.Collections;

namespace AutoContact.Controllers
{
    public class PurchaseOrderLineItemsController : Controller
    {
        private readonly AutoContactContext _context;

        public PurchaseOrderLineItemsController(AutoContactContext context)
        {
            _context = context;
        }

        // GET: POLineItems 
        public async Task<IActionResult> Index()
        {
            var autoContactContext = _context.PurchaseOrderLineItems.Include(p => p.PurchaseOrder).Include(p => p.Part);
            return View(await autoContactContext.ToListAsync());
        }

        // GET: POLineItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polineitem = await _context.PurchaseOrderLineItems
                .Include(e => e.PurchaseOrder)
                .Include(e => e.Part)
                .FirstOrDefaultAsync(m => m.PurchaseOrderLineItemId == id);
            if (polineitem == null)
            {
                return NotFound();
            }

            return View(polineitem);
        }

        // GET: POLineItems/Create
        public IActionResult Create()
        {
            ViewBag.Parts = new SelectList(_context.Parts, "PartId", "Name");

            return View();
        }

        // POST: POLineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderLineItemId,PurchaseOrderId,PartId,Qty,Price")] PurchaseOrderLineItem polineitem)
        {
            if (ModelState.IsValid)
            {
                //polineitem.PurchaseOrderId = outlong;

                var parts = _context.Parts;
                Part part = new Part();
                List<Part> partList = new List<Part>(parts);
                foreach (var p in partList)
                {
                    if (p.PartId == polineitem.PartId)
                    {
                        part = p;
                        break;
                    }
                }
                //polineitem.Part = part;
                polineitem.Price = (double)(polineitem.Qty * part.CostPrice);
                polineitem.PurchaseOrderId = long.Parse((string)TempData["poid"]);

                _context.Add(polineitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(polineitem);
        }

        // GET: POLineItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polineitem = await _context.PurchaseOrderLineItems.Include(x => x.PurchaseOrder).Include(x => x.Part).FirstOrDefaultAsync(p => p.PurchaseOrderLineItemId == id);
            if (polineitem == null)
            {
                return NotFound();
            }
            return View(polineitem);
        }

        // POST: POLineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PurchaseOrderId,PartId,Qty,Price")] PurchaseOrderLineItem polineitem)
        {
            if (id != polineitem.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polineitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!POLineItemExists(polineitem.PurchaseOrderLineItemId))
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
            return View(polineitem);
        }

        // GET: POLineItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polineitem = await _context.PurchaseOrderLineItems
                .Include(x => x.PurchaseOrder)
                .Include(x => x.Part)
                .FirstOrDefaultAsync(m => m.PurchaseOrderLineItemId == id);
            if (polineitem == null)
            {
                return NotFound();
            }

            return View(polineitem);
        }

        // POST: POLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var polineitem = await _context.PurchaseOrderLineItems.FindAsync(id);
            _context.PurchaseOrderLineItems.Remove(polineitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool POLineItemExists(long? id)
        {
            return _context.PurchaseOrderLineItems.Any(e => e.PurchaseOrderLineItemId == id);
        }
    }
}
