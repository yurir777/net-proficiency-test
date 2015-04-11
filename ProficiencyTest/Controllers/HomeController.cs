using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProficiencyTest.Data;
using ProficiencyTest.Models;
using ProficiencyTest.Utility;

namespace ProficiencyTest.Controllers
{
    public class HomeController : Controller
    {
        private ServiceHelper _serviceHelper;

        public HomeController(ServiceHelper serviceHelper)
        {
            this._serviceHelper = serviceHelper;
        }

        [Route("")]
        [Route("{pageId}")]
        public ActionResult Index(int pageId = 1)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public async Task<ActionResult> Contacts(int id = 1, string toPage = "")
        {
            int pageNo = id;
            switch (toPage.ToLower())
            {
                case "prev":
                    pageNo--;
                    break;
                case "next":
                    pageNo++;
                    break;
            }
            string pageUrl = "/people/page/" + pageNo.ToString();
            var contactsPage = await this._serviceHelper.GetServiceAsync<ContactsPage>(HttpVerbs.Get, pageUrl);
            return View(contactsPage);
        }

        public async Task<ActionResult> Customer(long? id = null)
        {
            var model = new Customer();
            if (id.HasValue && id.Value > 0) 
            {
                string url = "/people/" + id.Value.ToString();
                model = await this._serviceHelper.GetServiceAsync<Customer>(HttpVerbs.Get, url);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Customer(Customer model, bool delete = false)
        {
            if (delete || ModelState.IsValid)
            {
                if (delete) 
                {
                    string url = "/people/" + model.Id.ToString();
                    model = await this._serviceHelper.GetServiceAsync<Customer>(HttpVerbs.Delete, url);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Customer Deleted!";
                }
                else if (model.Id == 0) 
                { 
                    // Create
                    string url = "/customer";
                    model = await this._serviceHelper.GetServiceAsync<Customer>(HttpVerbs.Post, url, model);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Customer Created!";
                }
                else 
                { 
                    // Update
                    string url = "/customer/" + model.Id.ToString();
                    model = await this._serviceHelper.GetServiceAsync<Customer>(HttpVerbs.Put, url, model);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Customer Updated!";
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Supplier(long? id = null)
        {
            var model = new Supplier();
            if (id.HasValue && id.Value > 0)
            {
                string url = "/people/" + id.Value.ToString();
                model = await this._serviceHelper.GetServiceAsync<Supplier>(HttpVerbs.Get, url);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Supplier(Supplier model, bool delete = false)
        {
            if (delete || ModelState.IsValid)
            {
                if (delete)
                {
                    string url = "/people/" + model.Id.ToString();
                    model = await this._serviceHelper.GetServiceAsync<Supplier>(HttpVerbs.Delete, url);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Supplier Deleted!";
                }
                else if (model.Id == 0)
                {
                    // Create
                    string url = "/supplier";
                    model = await this._serviceHelper.GetServiceAsync<Supplier>(HttpVerbs.Post, url, model);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Supplier Created!";
                }
                else
                {
                    // Update
                    string url = "/supplier/" + model.Id.ToString();
                    model = await this._serviceHelper.GetServiceAsync<Supplier>(HttpVerbs.Put, url, model);
                    this.ModelState.Clear();
                    this.ViewBag.DetailsState = "Supplier Updated!";
                }
            }

            return View(model);
        }
    }
}
