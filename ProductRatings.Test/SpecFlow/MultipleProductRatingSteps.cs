using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ProductRatings.Test.SpecFlow
{
    [Binding]
    public class MultipleProductRatingSteps
    {
        private Catalog _catalog;

        [BeforeScenario]
        public void SetUp()
        {
            _catalog = new Catalog(new MemoryBackend());
        }
        [Given(@"a product called ""(.*)""")]
        public void GivenAProductCalled(string productName)
        {
            _catalog.AddProductCalled(productName);
        }
        
        [When(@"I rate ""(.*)"" (.*) stars")]
        public void WhenIRateStars(string productName, int nrOfStars)
        {
            _catalog.Get(productName).Rate(nrOfStars);
        }
        
        [Then(@"the average rating for ""(.*)"" should be (.*) stars")]
        public void ThenTheAverageRatingForShouldBeStars(string productName, int expectedNrOfStars)
        {
            _catalog.Get(productName).AverageRating.Should().Be(expectedNrOfStars);
        }
    }
}
