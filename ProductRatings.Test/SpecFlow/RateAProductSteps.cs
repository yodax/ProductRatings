using FluentAssertions;
using TechTalk.SpecFlow;

namespace ProductRatings.Test.SpecFlow
{
    [Binding]
    public class RateAProductSteps
    {
        private Product _product;

        [Given(@"a product")]
        public void GivenAProduct()
        {
            _product = new Product();
        }

        [When(@"I rate the product (.*) stars")]
        public void WhenIRateTheProductStars(int numberOfStars)
        {
            _product.Rate(numberOfStars);
        }

        [Then(@"the product should have an average (.*) star rating")]
        public void ThenTheProductShouldHaveAnAverageStarRating(int numberOfStars)
        {
            _product.AverageRating.Should().Be(numberOfStars);
        }

        [When(@"I dont rate the product")]
        public void WhenIDontRateTheProduct()
        {
            
        }

        [Then(@"the product should not have a rating")]
        public void ThenTheProductShouldNotHaveARating()
        {
            _product.Ratings.NoRatings.Should().BeTrue();
        }

        [When(@"I give the product an invalid rating of (.*)")]
        public void WhenIGiveTheProductAnInvalidRatingOf(int invalidRating)
        {
            _product.Rate(invalidRating);
        }

    }
}