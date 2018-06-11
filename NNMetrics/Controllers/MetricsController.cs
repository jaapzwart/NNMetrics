using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNMetrics.Data;
using NNMetrics.Models;
using System;

namespace NNMetrics.Controllers
{
    public class MetricsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// The metrics db context to be used.
        /// </summary>
        /// <param name="context">The context given to the contorller.</param>
        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the chart for the MTTR.
        /// </summary>
        /// <returns>View with the MTTR chart.</returns>
        public IActionResult UseDataFromServerMTTR()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        /// <summary>
        /// Get the chart for the PO satisfaction.
        /// </summary>
        /// <returns>View with the PO satisfaction chart.</returns>
        public IActionResult UseDataFromServerPO()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        public IActionResult UseDataFromServerCompleted()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        /// <summary>
        /// Get Json data for PO satisfaction.
        /// </summary>
        /// <returns>Json data for PO satisfaction.</returns>
        public JsonResult JsonDataMTTR()
        {
            var data = ModelHelper.MultiLineDataMTTR();
            return Json(data);
        }

        /// <summary>
        /// Get json data for PO satisfaction.
        /// </summary>
        /// <returns>Json data for PO satisfaction.</returns>
        public JsonResult JsonDataPO()
        {
            var data = ModelHelper.MultiLineDataPOSatisfaction();
            return Json(data);
        }

        /// <summary>
        /// Get Json data for completed from planned.
        /// </summary>
        /// <returns>Json data for completed from planned.</returns>
        public JsonResult JsonDataCompleted()
        {
            var data = ModelHelper.MultiLineDataCompletedFromPlanned();
            return Json(data);
        }

        /// <summary>
        /// Get the metrics table based on current user.
        /// </summary>
        /// <returns>Returns the table with Metrics for the current user.</returns>
        public IActionResult Index()
        {
            SharedData._contextGlobal = _context;
            SharedData.userName = User.Identity.Name;
            var signatures = from b in _context.Metrics
                                       where b.userName == User.Identity.Name
                                       select b;
            SharedData.MetricsRecordCount = signatures.Count();

            //-------------------------------------------------------------------------------------------
            // Down iterations are needed to get the data used for the json charts data.
            // Dynamic getting data in the model and view built up every call puts some difficulties 
            // in the freedom of getting data in the view where if statements will fail.
            // Although probably better solution are possible than the one down here, it is effective
            // and only makes the different built up of data in the model helper still a bit clumsy.
            // Time did not allowed for a better solution, cause we had 2 weeks to deliver.
            // Now you have:
            // Index --> SharedData class --> Model.Helper --> controller methods --> View
            //-------------------------------------------------------------------------------------------

            // Get MTTR in an array.
            SharedData.db_mttr.Clear();
            var mttr = from b in _context.Metrics
                       where b.userName == SharedData.userName
                       select b.MTTR;
            foreach(var item in mttr)
            {
                SharedData.db_mttr.Add(item);
            }

            // Get PO Satisfaction in an array.
            SharedData.db_PO.Clear();
            var po = from b in _context.Metrics
                       where b.userName == SharedData.userName
                       select b.POSatisfaction;
            foreach (var item in po)
            {
                SharedData.db_PO.Add(item);
            }

            // Get Completed from planned in an array.
            SharedData.db_Completed.Clear();
            var completed = from b in _context.Metrics
                     where b.userName == SharedData.userName
                     select b.CompletedForecast;
            foreach (var item in completed)
            {
                SharedData.db_Completed.Add(item);
            }

            // Get Title in an array
            SharedData.db_title.Clear();
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

        /// <summary>
        /// Create new petrics.
        /// </summary>
        /// <param name="metrics">Metrics to be added.</param>
        /// <returns>New record with metrics.</returns>
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

        /// <summary>
        /// Edit the metrics.
        /// </summary>
        /// <param name="id">Edit given id.</param>
        /// <returns>Return the record which must be editted.</returns>
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

        /// <summary>
        /// Edit the given record.
        /// </summary>
        /// <param name="id">Edit for the given id.</param>
        /// <param name="metrics">The metrics to be edit.</param>
        /// <returns>The editted record.</returns>
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

        /// <summary>
        /// Delete the given metrics.
        /// </summary>
        /// <param name="id">The id to be deleted.</param>
        /// <returns>The record to be deleted.</returns>
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

        /// <summary>
        /// Conforms the deletion of the given record.
        /// </summary>
        /// <param name="id">The id of the record to be deleted.</param>
        /// <returns>The case that the record is deleted.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metrics = await _context.Metrics.SingleOrDefaultAsync(m => m.ID == id);
            _context.Metrics.Remove(metrics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteAll()
        {
            ViewBag.Error = "";
            try
            {
                // delete Metrics for given user.
                _context.RemoveRange(_context.Metrics.Where(x => x.userName.Contains(SharedData.userName)));
                _context.SaveChanges();
                ViewBag.error = "Records succesful deleted.";
                
            }
            catch (Exception e)
            {
                ViewBag.error = "Delete not successful:" + e.Message;
            }
            return View();
        }

        private bool MetricsExists(int id)
        {
            return _context.Metrics.Any(e => e.ID == id);
        }
    }
}
