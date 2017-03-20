using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using ProductRatings.Persistence;
using TechTalk.SpecFlow;

namespace ProductRatings.Test.SpecFlow
{
    [Binding]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class RatingAProductSteps
    {
        private Product _product;

        [Given(@"a product")]
        public void GivenAProduct()
        {
            _product = new Catalog(new MemoryBackend()).AddProductCalled("Product 1");
        }
        
        [When(@"I rate the product (.*) stars")]
        public void WhenIRateTheProductStars(int numberOfStars)
        {
            _product.Rate(numberOfStars);
        }
        
        [Then(@"the average product rating should be (.*) stars")]
        public void ThenTheAverageProductRatingShouldBeStars(int expectedAverageRating)
        {
            _product.AverageRating.Should().Be(expectedAverageRating);
        }
    }
}
