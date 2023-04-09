using dotNet6Library;
using dotNet6Library.Evaluations;
using System.Collections.Generic;

namespace dotNet6Library.Evaluations.Content
{
    public class CityEvaluation : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
