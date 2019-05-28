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
    public class ProcessorFinalController : Controller
    {
        private readonly ProductEntities _context;

        public ProcessorFinalController(ProductEntities context)
        {
            _context = context;
        }




        [HttpPost]
        public async Task<IActionResult> Index_Search(String searchkey)
        {
            ProcessorFinal productModel = new ProcessorFinal();

            var processors = from s in _context.ProcessorFinal select s;
            if (String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "Please enter a keyword!";
                
            }
            else if (!String.IsNullOrEmpty(searchkey))
            {
                ViewBag.showMsg = "";
                processors = processors.Where(s => s.Name.Contains(searchkey));
            }

            //return View(await _context.DataProcessor.ToListAsync());
            return View(await processors.ToListAsync());
        }
        // GET: ProcessorFinal
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProcessorFinal.ToListAsync());
        }

        // GET: ProcessorFinal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processorFinal = await _context.ProcessorFinal
                .SingleOrDefaultAsync(m => m.ProId == id);
            if (processorFinal == null)
            {
                return NotFound();
            }

            return View(processorFinal);
        }

        // GET: ProcessorFinal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProcessorFinal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Description,ProId")] ProcessorFinal processorFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processorFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processorFinal);
        }

        // GET: ProcessorFinal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processorFinal = await _context.ProcessorFinal.SingleOrDefaultAsync(m => m.ProId == id);
            if (processorFinal == null)
            {
                return NotFound();
            }
            return View(processorFinal);
        }

        // POST: ProcessorFinal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Brand,Name,RyansPrice,StarPrice,StarLink,RyansLink,PicLink,Description,ProId")] ProcessorFinal processorFinal)
        {
            if (id != processorFinal.ProId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processorFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessorFinalExists(processorFinal.ProId))
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
            return View(processorFinal);
        }

        // GET: ProcessorFinal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processorFinal = await _context.ProcessorFinal
                .SingleOrDefaultAsync(m => m.ProId == id);
            if (processorFinal == null)
            {
                return NotFound();
            }

            return View(processorFinal);
        }

        // POST: ProcessorFinal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processorFinal = await _context.ProcessorFinal.SingleOrDefaultAsync(m => m.ProId == id);
            _context.ProcessorFinal.Remove(processorFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessorFinalExists(int id)
        {
            return _context.ProcessorFinal.Any(e => e.ProId == id);
        }
    }
}
