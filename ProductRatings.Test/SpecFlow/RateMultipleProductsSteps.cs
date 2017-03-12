using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ProductRatings.Test.SpecFlow
{
    [Binding]
    public class RateMultipleProductsSteps
    {
        private Catalog _catalog;

        [BeforeScenario]
        public void Setup()
        {
            _catalog = new Catalog();
        }

        [Given(@"a product called ""(.*)""")]
        public void GivenAProductCalled(string productName)
        {
            _catalog.Add(new Product {Name = productName});
        }
        
        [When(@"I rate a ""(.*)"" (.*) stars")]
        public void WhenIRateAStars(string productName, int numberOfStars)
        {
            _catalog.Get(productName).Rate(numberOfStars);
        }
        
        [Then(@"a product called ""(.*)"" has an average rating of (.*) stars")]
        public void ThenAProductCalledHasAnAverageRatingOfStars(string productName, int numberOfStars)
        {
            _catalog.Get(productName).AverageRating.Should().Be(numberOfStars);
        }
    }
}
