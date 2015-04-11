using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ProficiencyTest.Data;

namespace ProficiencyTest.Controllers
{
    public class ApiControllerBase : ApiController
    {
        protected DataContext _db;

        public ApiControllerBase()
        {
        }

        public ApiControllerBase(DataContext db)
        {
            this._db = db;
        }

        protected async Task<bool> PersonExists(long id)
        {
            return await _db.Contacts.AsNoTracking().AnyAsync(c => c.Id == id);
        }
    }
}
