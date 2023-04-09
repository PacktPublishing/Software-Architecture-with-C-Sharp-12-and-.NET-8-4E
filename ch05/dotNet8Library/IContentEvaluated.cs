using dotNet6Library.Evaluations;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet6Library
{
    public interface IContentEvaluated
    {
        List<Evaluation> Evaluations { get; set; }
    }
}
