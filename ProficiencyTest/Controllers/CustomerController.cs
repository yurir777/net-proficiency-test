using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProficiencyTest.Data;
using ProficiencyTest.Models;

namespace ProficiencyTest.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        public CustomerController(DataContext db)
            :base(db)
        {
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PutCustomer(long id, Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            bool personExists = await PersonExists(id);
            if (!personExists)
            {
                return NotFound();
            }

            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return this.Ok(model);
        }

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Contacts.Add(model);
            await _db.SaveChangesAsync();

            return this.Ok(model);
        }
    }
}