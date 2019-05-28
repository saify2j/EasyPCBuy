using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectDemo.Models.Product;

namespace FinalProjectDemo.Controllers
{
    public class HddFinalController : Controller
    {
        private readonly ProductEntities _context;

        public HddFinalController(ProductEntities context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index_Search(String searchkey)
        {
            HddFinal productModel = new HddFinal();

            var hdds = from s in _context.HddFinal select s;
            if (String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "Please enter a keyword!";

            }
            else if (!String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "";
                hdds = hdds.Where(s => s.Name.Contains(searchkey));
                if (hdds.Count() == 0)
                {
                    ViewBag.notfound = "No Product Found";
                }
                else ViewBag.notfound = "";
            }

            //return View(await _context.DataProcessor.ToListAsync());
            return View(await hdds.ToListAsync());
        }









        // GET: HddFinal
        public async Task<IActionResult> Index()
        {
            return View(await _context.HddFinal.ToListAsync());
        }

        // GET: HddFinal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hddFinal = await _context.HddFinal
                .SingleOrDefaultAsync(m => m.HddId == id);
            if (hddFinal == null)
            {
                return NotFound();
            }

            return View(hddFinal);
        }

        // GET: HddFinal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HddFinal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Storage,Description,HddId")] HddFinal hddFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hddFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hddFinal);
        }

        // GET: HddFinal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hddFinal = await _context.HddFinal.SingleOrDefaultAsync(m => m.HddId == id);
            if (hddFinal == null)
            {
                return NotFound();
            }
            return View(hddFinal);
        }

        // POST: HddFinal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Storage,Description,HddId")] HddFinal hddFinal)
        {
            if (id != hddFinal.HddId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hddFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HddFinalExists(hddFinal.HddId))
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
            return View(hddFinal);
        }

        // GET: HddFinal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hddFinal = await _context.HddFinal
                .SingleOrDefaultAsync(m => m.HddId == id);
            if (hddFinal == null)
            {
                return NotFound();
            }

            return View(hddFinal);
        }

        // POST: HddFinal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hddFinal = await _context.HddFinal.SingleOrDefaultAsync(m => m.HddId == id);
            _context.HddFinal.Remove(hddFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HddFinalExists(int id)
        {
            return _context.HddFinal.Any(e => e.HddId == id);
        }
    }
}
