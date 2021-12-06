using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoContact.Models;
using AutoContact.Helpers;

namespace AutoContact.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly AutoContactContext _context;

        public PurchaseOrdersController(AutoContactContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders 
        public async Task<IActionResult> Index()
        {
            var autoContactContext = _context.PurchaseOrders.Include(p => p.Vendor);

            return View(await autoContactContext.ToListAsync());
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(e => e.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            TempData["CurrentPurchaseOrderId"] = purchaseOrder.PurchaseOrderId.ToString();
            TempData["poid"] = purchaseOrder.PurchaseOrderId.ToString();

            return View(purchaseOrder);
        }


    
        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorId", "Name");

            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,Amount,PODate,CancelledDate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();

                TempData["CurrentPurchaseOrderId"] = purchaseOrder.PurchaseOrderId.ToString();
                TempData["poid"] = purchaseOrder.PurchaseOrderId.ToString();

                var items = _context.PurchaseOrderLineItems.ToList();
                List<PurchaseOrderLineItem> poItems = new List<PurchaseOrderLineItem>();
                poItems.Add(new PurchaseOrderLineItem());
                List<Part> poParts = new List<Part>();
                poParts.Add(new Part());

                foreach (var item in items)
                {
                    if (purchaseOrder.PurchaseOrderId == item.PurchaseOrderId)
                    {
                        poItems.Add(item);
                        poParts.Add(item.Part);
                    }
                }

                ViewBag.PurchaseOrderLineItems = new SelectList(poItems, "PurchaseOrderLineItemId");
                ViewBag.Parts = new SelectList(poParts, "PartId", "Name");

                long? id = purchaseOrder.PurchaseOrderId;
                return RedirectToAction("Details", new { id = purchaseOrder.PurchaseOrderId });
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorId", "Name");
            var purchaseOrder = await _context.PurchaseOrders.Include(x => x.Vendor).FirstOrDefaultAsync(p => p.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PurchaseOrderId,VendorId,Amount,PODate,CancelledDate")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PurchaseOrderId))
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
            return View(purchaseOrder);
        }



        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(x => x.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(long? id)
        {
            return _context.PurchaseOrders.Any(e => e.PurchaseOrderId == id);
        }

    }
}
