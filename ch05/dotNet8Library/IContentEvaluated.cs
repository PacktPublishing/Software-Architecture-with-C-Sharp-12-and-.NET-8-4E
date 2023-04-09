using dotNet8Library.Evaluations;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet8Library
{
    public interface IContentEvaluated
    {
        List<Evaluation> Evaluations { get; set; }
    }
}
