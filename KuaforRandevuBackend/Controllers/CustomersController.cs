using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuaforRandevuBackend.Context;
using KuaforRandevuBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

namespace KuaforRandevuBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ProjectContext _context;
        private UserManager<AppUser> _userManager;

        public CustomersController(ProjectContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Customers
        [HttpGet]
        [Route("getCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(string date)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            return await _context.Customers.Where(x => x.UserId == userId && x.Date==date).OrderBy(x=>x.Hour).ToListAsync();
        }
        // GET: api/Customers
        [HttpGet]
        [Route("getApprovedCustomers")]
        public async Task<ActionResult<IEnumerable<ApprovedCustomer>>> GetApprovedCustomers(string date)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var date1 = Convert.ToDateTime(date);
            var s = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy"));
            var customers=await _context.ApprovedCustomers.Where(x => x.UserId == userId && x.Date >= date1 && x.Date<=s).OrderByDescending(x => x.Date).ToListAsync();
            return customers;
        }
        


        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("updateCustomer")]
        public async Task<IActionResult> PutCustomer(Customer customer)
        {
            var _customer = _context.Customers.Where(i => i.Id == customer.Id).FirstOrDefault();
            _customer.Name = customer.Name;
            _customer.Surname = customer.Surname;
            _customer.Date = customer.Date;
            _customer.Hour = customer.Hour;
            _customer.Transactions = customer.Transactions;
            _customer.Price = customer.Price;
            _context.Entry(_customer).State = EntityState.Modified;
            _context.Customers.Update(_customer);
            _context.SaveChanges();
            return Ok(_customer);

           
        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("postCustomer")]
        public async Task<Object> PostCustomer(Customer customer)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            customer.User = user;
            customer.UserId = user.Id;
            var _customer = customer;
            
                try
                {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return Ok(_customer);
                }
                catch (DbUpdateException)
                {
                    if (CustomerExists(customer.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
              
            
            

            
        }
        [HttpPost]
        [Route("approveCustomer")]
        public async Task<Object> ApproveCustomer(Customer customer)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            customer.User = user;
            customer.UserId = user.Id;
            var _customer = customer;
            var approvedUser = new ApprovedCustomer { 
                Name=customer.Name,
               Surname=customer.Surname,
               Date=Convert.ToDateTime(customer.Date),
               Hour=customer.Hour,
               Id=customer.Id,
               Price=customer.Price,
               Transactions=customer.Transactions,
               User=customer.User,
               UserId=customer.UserId
            
            
            };

            try
            {
                var customer1 = await _context.Customers.FindAsync(customer.Id);
                if (customer1 == null)
                {
                    return NotFound();
                }

                 _context.Customers.Remove(customer1);
                await _context.ApprovedCustomers.AddAsync(approvedUser);
                await _context.SaveChangesAsync();
                return Ok(approvedUser);
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }





        }

        // DELETE: api/Customers/5
        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
