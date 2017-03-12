using System.Linq;

namespace ProductRatings
{
    public class Product
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public RatingsList Ratings { get; }

        public Product()
        {
            Ratings = new RatingsList();
        }

        public double AverageRating => Ratings.Average();
        public string Name { get; set; }

        public void Rate(int numberOfStars)
        {
            Ratings.Add(numberOfStars);
        }
    }
}