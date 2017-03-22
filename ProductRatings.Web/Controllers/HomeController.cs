using System;
using System.Linq;
using System.Web.Mvc;
using ProductRatings.Web.Models;

namespace ProductRatings.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Catalog _catalog;

        public HomeController(Catalog catalog)
        {
            _catalog = catalog;
        }

        public ActionResult Index()
        {
            var productOverview = new ProductOverview
            {
                Products = _catalog
                    .TopTenRatedProducts
                    .Select(ConvertToProductView)
            };

            return View(productOverview);
        }

        private static ProductOverview.ProductView ConvertToProductView(Product p)
        {
            return new ProductOverview.ProductView {AverageRating = p.AverageRating, Name = p.Name};
        }

        public ActionResult AllProducts()
        {
            throw new NotImplementedException();
        }

        public ActionResult Populate()
        {
            _catalog.RemoveAll();

            for (var rating = 1; rating < 5; rating++)
            for (var currentProduct = 0; currentProduct < 5; currentProduct++)
                _catalog.AddProductCalled($"Product {currentProduct + 1} with rating {rating}").Rate(rating);

            return RedirectToAction("Index");
        }
    }
}