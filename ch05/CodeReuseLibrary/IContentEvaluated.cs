using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReuseLibrary
{
    public interface IContentEvaluated
    {
        List<Evaluation> Evaluations { get; set; }
    }
}
