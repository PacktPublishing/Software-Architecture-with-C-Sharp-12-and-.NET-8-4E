using dotNet6Library;
using dotNet6Library.Evaluations;
using System.Collections.Generic;

namespace dotNet6Library.Evaluations.Content
{
    public class Comments : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; }
    }
}
