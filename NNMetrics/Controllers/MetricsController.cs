using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NNMetrics.Data;
using NNMetrics.Models;

namespace NNMetrics.Controllers
{
    public class MetricsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult UseDataFromServer()
        {
            return View();
        }

        public JsonResult JsonData()
        {
            var data = ModelHelper.MultiLineData();
            return Json(data);
        }

        /// <summary>
        /// Get the metrics table based on current user.
        /// </summary>
        /// <returns>Returns the table with Metrics for the current user.</returns>
        public IActionResult Index()
        {
            SharedData.userName = User.Identity.Name;
            var signatures = from b in _context.Metrics
                                       where b.userName == User.Identity.Name
                                       select b;
            // Get MTTR in an array
            var mttr = from b in _context.Metrics
                       where b.userName == SharedData.userName
                       select b.MTTR;
            foreach(var item in mttr)
            {
                SharedData.db_mttr.Add(item);
            }

            // Get Title in an array
            var title = from b in _context.Metrics
                       where b.userName == SharedData.userName
                       select b.Title;
            foreach(var item in title)
            {
                SharedData.db_title.Add(item);
            }

            return View(signatures.ToList());
        }

        /// <summary>
        /// Get the details of the metrics.
        /// </summary>
        /// <param name="id">Id of the chosen metric in the table.</param>
        /// <returns>Return the chosen metric from the table.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrics = await _context.Metrics
                .SingleOrDefaultAsync(m => m.ID == id);
            if (metrics == null)
            {
                return NotFound();
            }
            
            return View(metrics);
        }

        /// <summary>
        /// Create a new record with metrics.
        /// </summary>
        /// <returns>Return the page for creating the new metrics.</returns>
        public IActionResult Create()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        // POST: Metrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,userName,Title,MeasureDate,POSatisfaction,CompletedForecast,MTTR,NumberOfDeployments")] Metrics metrics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metrics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(metrics);
        }

        // GET: Metrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrics = await _context.Metrics.SingleOrDefaultAsync(m => m.ID == id);
            if (metrics == null)
            {
                return NotFound();
            }
            return View(metrics);
        }

        // POST: Metrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,userName,Title,MeasureDate,POSatisfaction,CompletedForecast,MTTR,NumberOfDeployments")] Metrics metrics)
        {
            if (id != metrics.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metrics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricsExists(metrics.ID))
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
            return View(metrics);
        }

        // GET: Metrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrics = await _context.Metrics
                .SingleOrDefaultAsync(m => m.ID == id);
            if (metrics == null)
            {
                return NotFound();
            }

            return View(metrics);
        }

        // POST: Metrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metrics = await _context.Metrics.SingleOrDefaultAsync(m => m.ID == id);
            _context.Metrics.Remove(metrics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetricsExists(int id)
        {
            return _context.Metrics.Any(e => e.ID == id);
        }
    }
}
