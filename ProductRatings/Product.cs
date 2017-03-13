using System.Linq;

namespace ProductRatings
{
    public class Product
    {
        public RatingList Ratings { get; }

        public Product()
        {
            Ratings = new RatingList();
        }

        public void Rate(int numberOfStars)
        {
            Ratings.Add(numberOfStars);
        }

        public double AverageRating => Ratings.Average();
        public string Name { get; set; }
    }
}