using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation
{
    public class Rating
    {
        public int RateId { get; set; }
        public int UserId { get; set; }
        public int PlaceIdOrigin { get; set; }
        public int PlaceIdDestiny { get; set; }
        public float RatingValue {  get; set; }
    }
}