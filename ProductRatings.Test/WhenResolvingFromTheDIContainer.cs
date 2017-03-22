using Autofac;
using FluentAssertions;
using NUnit.Framework;
using ProductRatings.Web;
using ProductRatings.Web.Controllers;

namespace ProductRatings.Test
{
    [TestFixture]
    public class WhenResolvingFromTheDiContainer
    {
        [Test]
        public void AllTypesShouldBeResolved()
        {
            var container = DependencyContainer.Build();

            container.Resolve<Catalog>().Should().NotBeNull();
            container.Resolve<HomeController>().Should().NotBeNull();
        }

        [Test]
        public void MvcResolverShouldBeCreated()
        {
            DependencyContainer.MvcDiResolver().ApplicationContainer.Resolve<Catalog>().Should().NotBeNull();
        }
    }
}