using AandN_Website.IntegrationServiceRef;
using AandN_Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AandN_Website.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IntegrationServiceSoapClient _client;

        public CatalogController()
        {
            _client = new IntegrationServiceSoapClient("IntegrationServiceSoap");
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new CatalogViewModel
            {
                Items = _client.GetCatalogItems(null) // Load all items by default
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string category)
        {
            var model = new CatalogViewModel
            {
                Category = category,
                Items = _client.GetCatalogItems(category)
            };
            return View(model);
        }

        public ActionResult Detail(string id)
        {

            var allItems = _client.GetCatalogItems(null); // null = get all
            var product = allItems.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return HttpNotFound();

            // Get inventory info
            var inventory = _client.GetInventoryLevels(id).FirstOrDefault();

            var model = new ProductDetailViewModel
            {
                Item = product,
            };

            return View(model);
        }


    }
}