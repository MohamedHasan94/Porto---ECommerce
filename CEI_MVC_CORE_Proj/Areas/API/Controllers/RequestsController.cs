using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Identity;
using ITI.MVC.Core.Day2.Core;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CEI_MVC_CORE_Proj.Areas.API.Controllers
{
    /// <summary>
    /// API Controller For the requests entity, 
    /// It allows you to list and view the requests that the users sent for the admin to review
    /// you can change status (approve/ reject / delete ) the requests
    /// user: admin
    /// pass: P@ssw0rd
    /// To View Documentation Go To : localhost:"port"/swagger
    /// Made By: Abdelrahman Osama
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "JwtBearer")]
    public class RequestsController : ControllerBase
    {
        private RoleManager<AppRole> roleManager;


        private IUnitOfWork unit;
        private ApplicationDbContext context;
        private UserManager<AppUser> userManager;


        public RequestsController(IUnitOfWork _unit, UserManager<AppUser> _userManager , ApplicationDbContext _context)
        {
            unit = _unit;
            userManager = _userManager;
            context = _context;

        }

        /// <summary>
        /// Retrieves the requests for the admin (Users requesting to be vendors, or Vendors Requesting to add new category)
        /// </summary>
        /// <param name="sortOrder"> Optional </param>
        /// <param name="currentFilter">Optional </param>
        /// <param name="searchString"> Optional </param>
        /// <param name="pageNumber">Optional</param>
        /// <returns> Paged List of Requests </returns>
        [HttpGet]
        public PaginatedList<RequestToAdmin> GetRequests(string sortOrder,
               string currentFilter,
               string searchString,
               int? pageNumber)
        {
            
            PaginatedList<RequestToAdmin> pgRequests = unit.Requests.GetPaged(sortOrder, currentFilter, searchString, pageNumber);
            //IEnumerable<RequestToAdmin> pgRequests = unit.Requests.GetPaged(null, null, null, pageNumber);
            Response.StatusCode = StatusCodes.Status200OK;
            return pgRequests;
        }

        /// <summary>
        /// Retrieves Specific Request, you have to provide the id of the request in the route
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Requests/1
        [HttpGet("{id}")]
        public IActionResult GetRequests([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var req = unit.Requests.GetById(id);
            if (req == null) return NotFound();
            return Ok(req);
        }
        /// <summary>
        /// Creates a new request in the database
        /// </summary>
        /// <param name="request"> Don't provide the Id , only the data, fk_userid, type parameters</param>
        /// <returns></returns>
        // POST: api/Requests
        [HttpPost]
        public IActionResult PostRequests([FromBody] RequestToAdmin request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                unit.Requests.Add(request);
            }
            catch (Exception e) {
                return BadRequest(e);
            }
            return CreatedAtAction("GetRequests", new { id = request.Id }, request);
        }

        /// <summary>
        ///  Edit an existing request
        /// </summary>
        /// <param name="id"> From the route  api/Requests/{id}</param>
        /// <param name="requestToAdmin"> the data of the request</param>
        /// <returns></returns>
        // PUT: api/Requests/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests([FromRoute] int id, [FromBody] RequestToAdmin requestToAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestToAdmin.Id)
            {
                return BadRequest();
            }

            context.Entry(requestToAdmin).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Requests.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes Specific Request, you have to provide the id of the request in the route
        /// </summary>
        /// <param name="id">the id of the request to be deleted</param>
        /// <returns></returns>
        // DELETE: api/Requests/1
        [HttpDelete("{id}")]
        public IActionResult DeleteRequestToAdmin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = unit.Requests.RemoveById(id);
            return Ok(new { Success = result });
        }


    }
}