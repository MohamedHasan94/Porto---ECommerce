using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEI_MVC_CORE_Proj.Areas.API.Controllers
{
    /// <summary>
    /// API Controller For the Transaction entity, 
    /// It allows you to list and view the Transactions that the users made to buy from vendor
    /// you can change the Transaction status (Shipped/ Completed / Rejected )
    /// user: vendor
    /// pass: P@ssw0rd
    /// To View Documentation Go To : localhost:"port"/swagger
    /// Made By: Mohamed Gamal
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Vendor", AuthenticationSchemes = "JwtBearer")]
    public class TransactionsController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        public TransactionsController(IUnitOfWork unit)
        {
            this.unit = unit;
        }


        /// <summary>
        /// Retrieves the Transactions for the vendor (Users orders from this vendors)
        /// </summary>
        /// <param name="sortOrder"> Optional </param>
        /// <param name="searchString"> Optional </param>
        /// <param name="pageNumber">Optional</param>
        /// <returns> Paged List of Transactions </returns>
        // GET: api/Transactions/1
        [HttpGet]
        public IActionResult GetTransactions(string sortOrder, string searchString, int? pageNumber)
        {
            //string vendorId = "0bcd78db-8663-46fd-874c-3b4fa91e63e8";
            string vendorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Transaction> transactions = unit.Transactions.GetPagedVendor(vendorId, sortOrder, searchString, pageNumber ?? 1);
            return Ok(transactions);
        }


        /// <summary>
        /// Retrieves Specific Transaction, you have to provide the id of the Transaction in the route
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Transactions/1
        [HttpGet("{id}")]
        public IActionResult GetTransactions([FromRoute] int id)
        {
            Transaction transaction = unit.Transactions.GetById(id);
            if (transaction != null)
                return Ok(transaction);

            return NotFound();
        }


        /// <summary>
        /// Creates a new Transaction in the database
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        // POST: api/Transactions
        [HttpPost]
        public IActionResult PostTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unit.Transactions.Add(transaction);
            return CreatedAtAction(nameof(PostTransaction), new { Id = transaction.Id }, transaction);
        }


        /// <summary>
        ///  Edit an existing Transaction
        /// </summary>
        /// <param name="id"> From the route  api/Transaction/{id}</param>
        /// <param name="transaction"> the data of the transaction</param>
        /// <returns>Transaction</returns>
        // PUT: api/Transactions/3
        [HttpPut("{id}")]
        public IActionResult PutTransaction([FromRoute] int id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
                return BadRequest();

            if (unit.Transactions.Update(transaction))
                return CreatedAtAction(nameof(PutTransaction), new { Id = transaction.Id }, transaction);
            else
                return BadRequest("Update failed : please review transaction data");
        }


        /// <summary>
        /// Deletes Specific Transaction, you have to provide the id of the Transaction in the route
        /// </summary>
        /// <param name="id">the id of the Transaction to be deleted</param>
        /// <returns></returns>
        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction([FromRoute] int id)
        {
            Transaction transaction = unit.Transactions.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            if (unit.Transactions.RemoveById(id))
                return Ok(transaction);
            else
                return BadRequest();
        }
    }
}