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
    public class SupplierController : ApiControllerBase
    {
        public SupplierController(DataContext db)
            : base(db)
        {
        }

        // PUT: api/Supplier/5
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> PutSupplier(long id, Supplier model)
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

        // POST: api/Supplier
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> PostSupplier(Supplier model)
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