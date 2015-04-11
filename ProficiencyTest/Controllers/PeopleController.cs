using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;
using ProficiencyTest.Data;
using ProficiencyTest.Models;

namespace ProficiencyTest.Controllers
{
    public class PeopleController : ApiControllerBase
    {
        private int _defaultPageSize = 5;

        public PeopleController(DataContext db)
            :base(db)
        {
        }

        // GET api/people/page/2
        [Route("api/people/page/{pageno}")]
        public async Task<ContactsPage> GetContacts(int pageNo = 1)
        {
            int pageSize = this.GetPageSize();
            int pageCount = await this.GetPageCount();
            int pageNumber = Math.Max(Math.Min(pageNo, pageCount), 1);
            var contacts = await _db.Contacts.OrderBy(c => c.Name.First).ThenBy(c => c.Name.Last).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ContactsPage { PageNumber = pageNumber, PageCount = pageCount, Contacts = contacts };
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetPerson(long id)
        {
            Person person = await _db.Contacts.FindAsync(id);
            if (person == null)
            {
                return this.NotFound();
            }

            return this.Ok(person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(long id)
        {
            Person model = await _db.Contacts.FindAsync(id);
            if (model != null)
            {
                _db.Contacts.Remove(model);
                await _db.SaveChangesAsync();
            }
            return Ok(model);
        }

        private int GetPageSize()
        {
            int pageSize = this._defaultPageSize;
            string pageSizeSetting = WebConfigurationManager.AppSettings["ContactsPageSize"];
            if (!string.IsNullOrWhiteSpace(pageSizeSetting))
            {
                int.TryParse(pageSizeSetting, out pageSize);
            }
            return pageSize;
        }

        private async Task<int> GetPageCount()
        {
            int pageSize = this.GetPageSize();
            int contactCount = await _db.Contacts.CountAsync();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)contactCount / pageSize));
            return pageCount;
        }
    }
}