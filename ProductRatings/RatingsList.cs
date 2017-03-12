using System.Collections.Generic;

namespace ProductRatings
{
    public class RatingsList : List<int>
    {
        public new void Add(int ratingToAdd)
        {
            if (ratingToAdd < 1 || ratingToAdd > 5)
                return;

            base.Add(ratingToAdd);  
        }
        
        public bool NoRatings => Count == 0;
    }
}