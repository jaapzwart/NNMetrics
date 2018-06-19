////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Controllers\MetricsController.cs
//
// summary:	Implements the metrics controller class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNMetrics.Data;
using NNMetrics.Models;
using System;


////////////////////////////////////////////////////////////////////////////////////////////////////
// namespace: NNMetrics.Controllers
//
// summary:	The controller for handling the metrics.
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace NNMetrics.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling metrics. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class MetricsController : Controller
    {
        
        /// <summary>   The context. </summary>
        private readonly ApplicationDbContext _context;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="context">  The context. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Use data from server mttr. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult UseDataFromServerMTTR()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Use data from server po. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult UseDataFromServerPO()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Use data from server completed. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult UseDataFromServerCompleted()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Use data from server dp. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult UseDataFromServerDeployments()
        {
            ViewBag.currentUser = SharedData.userName;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a JSON result with the given data as its content. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A JSON response stream to send to the JsonDataMTTR action. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JsonResult JsonDataMTTR()
        {
            var data = ModelHelper.MultiLineDataMTTR();
            return Json(data);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a JSON result with the given data as its content. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A JSON response stream to send to the JsonDataPO action. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JsonResult JsonDataPO()
        {
            var data = ModelHelper.MultiLineDataPOSatisfaction();
            return Json(data);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a JSON result with the given data as its content. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A JSON response stream to send to the JsonDataCompleted action. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JsonResult JsonDataCompleted()
        {
            var data = ModelHelper.MultiLineDataCompletedFromPlanned();
            return Json(data);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a JSON result with the given data as its content. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A JSON response stream to send to the JsonDataDP action. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JsonResult JsonDataDP()
        {
            var data = ModelHelper.MultiLineDataDeployments();
            return Json(data);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Index set u ser name. </summary>
        ///
        /// <remarks>   Administrator, 16/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult IndexSetUSerName(string userName)
        {
            SharedData.userName = userName;
            return RedirectToAction(nameof(MetricsController.Index), "Teams");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Index from teams. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="teamName"> Name of the team. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult IndexFromTeams(string teamName)
        {
            SharedData.teamName = teamName;
            return RedirectToAction(nameof(MetricsController.Index), "Metrics");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult Index()
        {
            if (SharedData.userName.Contains("admin"))
                return RedirectToAction(nameof(HomeController.Index), "Home");
            if (SharedData.userName.Equals(""))
                return RedirectToAction("Login", "Account");
            SharedData._contextGlobal = _context;
            if(SharedData.userName.Equals(""))
                SharedData.userName = User.Identity.Name;

            var signatures = from b in _context.Metrics                                      
                             where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                             orderby b.ID descending
                             select b;

            SharedData.MetricsRecordCount = signatures.Count(); // used to determine if account can be deleted.

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
            // TODO: create a more elegant version of down code with changes to clumsy model code.

            // Get MTTR in an array.
            SharedData.db_mttr.Clear();
            var mttr = from b in _context.Metrics
                       where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                       orderby b.ID descending
                       select b.MTTR;
            int i = 0;
            foreach(var item in mttr)
            {
                if(i < 9)
                    SharedData.db_mttr.Add(item);
                i++;
            }
            i = 0;

            // Get PO Satisfaction in an array.
            SharedData.db_PO.Clear();
            var po = from b in _context.Metrics
                     where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                     orderby b.ID descending
                     select b.POSatisfaction;
            foreach (var item in po)
            {
                if(i < 9)
                    SharedData.db_PO.Add(item);
                i++;
            }
            i = 0;

            // Get Completed from planned in an array.
            SharedData.db_Completed.Clear();
            var completed = from b in _context.Metrics
                            where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                            orderby b.ID descending
                            select b.CompletedForecast;
            foreach (var item in completed)
            {
                if(i < 9)
                    SharedData.db_Completed.Add(item);
                i++;
            }
            i = 0;

            // Get the number of deployments.
            SharedData.db_dp.Clear();
            var deployments = from b in _context.Metrics
                              where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                              orderby b.ID descending
                              select b.NumberOfDeployments;
            foreach (var item in deployments)
            {
                if(i < 9)
                    SharedData.db_dp.Add(item);
                i++;
            }
            i = 0;

            // Get Title in an array.
            SharedData.db_title.Clear();
            var title = from b in _context.Metrics
                        where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                        orderby b.ID descending
                        select b.Title;
            foreach(var item in title)
            {
                if(i < 9)
                    SharedData.db_title.Add(item);
                i++;
            }

            ViewBag.userName = SharedData.userName;
            ViewBag.teamName = SharedData.teamName;
            return View(signatures.ToList());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Details the given identifier. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="id">   Edit given id. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new IActionResult. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult Create()
        {
            ViewBag.currentUser = SharedData.userName;
            ViewBag.teamName = SharedData.teamName;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP POST requests) creates a new Task&lt;IActionResult&gt;
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="metrics">  The metrics to be edit. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductOwner,ScrumMaster,TeamName,userName,Title,MeasureDate,POSatisfaction,CompletedForecast,MTTR,NumberOfDeployments")] Metrics metrics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metrics);
                await _context.SaveChangesAsync();
                SharedData.productOwner = metrics.ProductOwner;
                SharedData.scrumMaster = metrics.ScrumMaster;
                SharedData.teamName = metrics.TeamName;
                return RedirectToAction(nameof(Index));
            }
            
            return View(model: metrics);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Edits the given identifier. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="id">   Edit given id. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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
            ViewBag.TeamName = SharedData.teamName;
            return View(metrics);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">       The id of the record to be deleted. </param>
        /// <param name="metrics">  The metrics to be edit. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,userName,Title,ProductOwner,ScrumMaster,TeamName,MeasureDate,POSatisfaction,CompletedForecast,MTTR,NumberOfDeployments")] Metrics metrics)
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="id">   Edit given id. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP POST requests) (Defines the Delete Action) deletes the confirmed
        /// described by ID.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="id">   The id of the record to be deleted. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metrics = await _context.Metrics.SingleOrDefaultAsync(m => m.ID == id);
            _context.Metrics.Remove(metrics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes all. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult DeleteAll()
        {
            ViewBag.Error = "";
            try
            {
                // delete Metrics for given user with given team.
                _context.RemoveRange(_context.Metrics.Where(x => x.userName.Contains(SharedData.userName) && x.TeamName.Equals(SharedData.teamName)));
                _context.SaveChanges();
                ViewBag.error = "Records succesful deleted.";
                
            }
            catch (Exception e)
            {
                ViewBag.error = "Delete not successful:" + e.Message;
            }
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Queries if a given metrics exists. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="id">   The id of the record to be deleted. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool MetricsExists(int id)
        {
            return _context.Metrics.Any(e => e.ID == id);
        }
    }
}
