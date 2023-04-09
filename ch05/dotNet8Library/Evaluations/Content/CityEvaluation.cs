using dotNet8Library;
using dotNet8Library.Evaluations;
using System.Collections.Generic;

namespace dotNet8Library.Evaluations.Content
{
    public class CityEvaluation : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
