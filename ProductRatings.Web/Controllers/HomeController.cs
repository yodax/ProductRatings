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
            return View(new ProductOverview
            {
                Products = _catalog
                    .TopTenRatedProducts
                    .Select(ConvertToProductView)
            });
        }

        private static ProductOverview.ProductView ConvertToProductView(Product p)
        {
            return new ProductOverview.ProductView {AverageRating = p.AverageRating, Name = p.Name};
        }

        public ActionResult AllProducts()
        {
            return View(new ProductOverview
            {
                Products = _catalog
                    .AllProducts
                    .OrderBy(p => p.Name)
                    .Select(ConvertToProductView)
            });
        }

        public ActionResult Populate()
        {
            _catalog.RemoveAll();

            for (var rating = 1; rating < 5; rating++)
            for (var currentProduct = 0; currentProduct < 5; currentProduct++)
                _catalog.AddProductCalled($"Product {currentProduct + 1} with rating {rating}").Rate(rating);

            return RedirectToAction("Index");
        }

        public ActionResult AddProduct()
        {
            return View(new AddProductModel());
        }

        public ActionResult AddProductToDatabase(AddProductModel productToAdd)
        {
            _catalog.AddProductCalled(productToAdd.Name);
            return RedirectToAction("Index");
        }
    }
}