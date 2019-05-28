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
    public class MoboFinalController : Controller
    {
        private readonly ProductEntities _context;

        public MoboFinalController(ProductEntities context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Index_Search(String searchkey)
        {
            MoboFinal productModel = new MoboFinal();

            var mainboards = from s in _context.MoboFinal select s;
            if (String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "Please enter a keyword!";

            }
            else if (!String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "";
                mainboards = mainboards.Where(s => s.Name.Contains(searchkey));
                if (mainboards.Count() == 0)
                {
                    ViewBag.notfound = "No Product Found";
                }
                else ViewBag.notfound = "";
            }

            //return View(await _context.DataProcessor.ToListAsync());
            return View(await mainboards.ToListAsync());
        }
        // GET: MoboFinals
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoboFinal.ToListAsync());
        }

        // GET: MoboFinals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboFinal = await _context.MoboFinal
                .SingleOrDefaultAsync(m => m.MoboId == id);
            if (moboFinal == null)
            {
                return NotFound();
            }

            return View(moboFinal);
        }

        // GET: MoboFinals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoboFinals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Description,MoboId")] MoboFinal moboFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moboFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moboFinal);
        }

        // GET: MoboFinals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboFinal = await _context.MoboFinal.SingleOrDefaultAsync(m => m.MoboId == id);
            if (moboFinal == null)
            {
                return NotFound();
            }
            return View(moboFinal);
        }

        // POST: MoboFinals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Description,MoboId")] MoboFinal moboFinal)
        {
            if (id != moboFinal.MoboId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moboFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoboFinalExists(moboFinal.MoboId))
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
            return View(moboFinal);
        }

        // GET: MoboFinals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboFinal = await _context.MoboFinal
                .SingleOrDefaultAsync(m => m.MoboId == id);
            if (moboFinal == null)
            {
                return NotFound();
            }

            return View(moboFinal);
        }

        // POST: MoboFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moboFinal = await _context.MoboFinal.SingleOrDefaultAsync(m => m.MoboId == id);
            _context.MoboFinal.Remove(moboFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoboFinalExists(int id)
        {
            return _context.MoboFinal.Any(e => e.MoboId == id);
        }
    }
}
