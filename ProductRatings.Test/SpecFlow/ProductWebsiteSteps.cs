using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using ProductRatings.Persistence;
using ProductRatings.Web.Controllers;
using ProductRatings.Web.Models;
using TechTalk.SpecFlow;

namespace ProductRatings.Test.SpecFlow
{
    [Binding]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ProductWebsiteSteps
    {
        private Catalog _catalog;
        private MemoryBackend _persistenceBackend;

        [BeforeScenario]
        public void SetUp()
        {
            _persistenceBackend = new MemoryBackend();
            _catalog = new Catalog(_persistenceBackend);
        }

        [Given(@"a catalog")]
        public void GivenACatalog()
        {
            
        }

        [When(@"I populate the database")]
        public void WhenIPopulateTheDatabase()
        {
            new HomeController(new Catalog(_persistenceBackend))
                .Populate();
        }

        [Then(@"there should be products present")]
        public void ThenThereShouldBeProductsPresent()
        {
            _catalog.AllProducts.Should().NotBeEmpty();
        }

        [Given(@"a list of (.*) products rated (.*) stars")]
        public void GivenAListOfProductsRatedStars(int numberOfProducts, int rating)
        {
            for (var currentProduct = 0; currentProduct < numberOfProducts; currentProduct++)
            {
                _catalog.AddProductCalled($"Product {currentProduct} with rating {rating}").Rate(rating);
            }
        }

        [Given(@"an existing product called ""(.*)""")]
        public void GivenAnExistingProductCalled(string productName)
        {
            _catalog.AddProductCalled(productName);
        }

        [Then(@"it should not contain a ""(.*)""")]
        public void ThenItShouldNotContainA(string productName)
        {
            _catalog.Contains(productName).Should().BeFalse();
        }


        [When(@"I request an overview")]
        public void WhenIRequestAnOverview()
        {
        }

        [Then(@"the overview should show the first 10 products ordered by rating")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void ThenTheOverviewShouldShowTheFirstProductsOrderedByRating()
        {
            var productOverview = (new HomeController(new Catalog(_persistenceBackend))
                                        .Index() as ViewResult)
                                        .Model as ProductOverview;

            productOverview.Should().NotBeNull();
            productOverview.Products.Should().NotBeNull();
            productOverview.Products.Count().Should().Be(10);
            productOverview.Products.ElementAt(0).AverageRating.Should().Be(5);
            productOverview.Products.ElementAt(1).AverageRating.Should().Be(5);
            productOverview.Products.ElementAt(2).AverageRating.Should().Be(5);
            productOverview.Products.ElementAt(3).AverageRating.Should().Be(5);
            productOverview.Products.ElementAt(4).AverageRating.Should().Be(5);
            productOverview.Products.ElementAt(5).AverageRating.Should().Be(4);
            productOverview.Products.ElementAt(6).AverageRating.Should().Be(4);
            productOverview.Products.ElementAt(7).AverageRating.Should().Be(4);
            productOverview.Products.ElementAt(8).AverageRating.Should().Be(4);
            productOverview.Products.ElementAt(9).AverageRating.Should().Be(4);
        }
    }
}