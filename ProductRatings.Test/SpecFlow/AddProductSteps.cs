using System.Diagnostics.CodeAnalysis;
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
    public class AddProductSteps
    {
        private string _productNameToAdd;
        private Catalog _catalog;

        [Given(@"a new product called ""(.*)""")]
        public void GivenANewProductCalled(string productName)
        {
            _productNameToAdd = productName;
        }

        [When(@"I add the product")]
        public void WhenIAddTheProduct()
        {
            _catalog = new Catalog(new MemoryBackend());

            new HomeController(_catalog).AddProductToDatabase(new AddProductModel{Name = _productNameToAdd});
        }

        [Then(@"the product is added")]
        public void ThenTheProductIsAdded()
        {
            _catalog.Get(_productNameToAdd).Should().NotBeNull();
        }
    }
}