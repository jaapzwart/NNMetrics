////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	C:\Users\Administrator\source\repos\NNMetrics\NNMetrics\Controllers\TeamsController.cs
//
// summary:	Implements the teams controller class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNMetrics.Data;
using NNMetrics.Models;

namespace NNMetrics.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling teams. </summary>
    ///
    /// <remarks>   Administrator, 18/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="context">  The context. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> Index()
        {
            if (SharedData.userName.Contains("admin"))
                return RedirectToAction(nameof(HomeController.Index), "Home");
            if (SharedData.userName.Equals(""))
                return RedirectToAction("Login", "Account");
            SharedData._contextGlobal = _context;
            if (SharedData.userName.Equals(""))
                SharedData.userName = User.Identity.Name;

            var signatures = from b in _context.Teams
                             where b.userName == SharedData.userName
                             orderby b.ID descending
                             select b;

            SharedData.TeamsRecordCount = signatures.Count();
            ViewBag.userName = SharedData.userName;

            // Stupid way to reset teams delete error, but not enough time for the proper solution.
            // TODO: implement better solution to check of we come here second time after detecting false team delete.
            if (SharedData.ErrorDeleteTeams.Length > 0 && SharedData.ErrorDeleteTeamsCounter < 1)
            {
                ViewBag.Error = SharedData.ErrorDeleteTeams;
                SharedData.ErrorDeleteTeamsCounter += 1;
            }
            else
            {
                ViewBag.Error = "";
                SharedData.ErrorDeleteTeams = "";
                SharedData.ErrorDeleteTeamsCounter = 0;
            }
            return View(await signatures.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Details the given identifier. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new IActionResult. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult Create()
        {
            ViewBag.userName = SharedData.userName;
            return View();
        }

        
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new Task&lt;IActionResult&gt; </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="teams">    The teams. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,userName,teamName")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teams);
                await _context.SaveChangesAsync();
                SharedData.teamName = teams.teamName;
                return RedirectToAction(nameof(Index));
            }
            return View(teams);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Edits the given identifier. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams.SingleOrDefaultAsync(m => m.ID == id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Edits. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="teams">    The teams. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,userName,teamName")] Teams teams)
        {
            if (id != teams.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(teams.ID))
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
            return View(teams);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="id">   The Identifier to delete. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .SingleOrDefaultAsync(m => m.ID == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the confirmed described by ID. </summary>
        ///
        /// <remarks>   Administrator, 18/06/2018. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teams = await _context.Teams.SingleOrDefaultAsync(m => m.ID == id);
            SharedData.teamName = teams.teamName;
            var signatures = from b in _context.Metrics
                             where b.userName == SharedData.userName && b.TeamName == SharedData.teamName
                             orderby b.ID descending
                             select b;
            if(signatures.Count() > 0)
            {
                SharedData.ErrorDeleteTeams = "Still metric records available. Delete them first.";
                return RedirectToAction("Index", "Teams");
            }
            SharedData.ErrorDeleteTeams = "";

            _context.Teams.Remove(teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
