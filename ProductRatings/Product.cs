using System.Linq;

namespace ProductRatings
{
    public class Product
    {
        [PetaPoco.Ignore]
        public RatingList Ratings { get; }

        public Product()
        {
            Ratings = new RatingList();
        }

        public void Rate(int numberOfStars)
        {
            Ratings.Add(numberOfStars);
        }
        [PetaPoco.Ignore]
        public double AverageRating => Ratings.Count == 0 ? 0 : Ratings.Average();
        public string Name { get; set; }
    }
}