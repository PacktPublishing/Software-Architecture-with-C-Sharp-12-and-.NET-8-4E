using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet8Library.Evaluations.Types
{
    public class BasicUsersEvaluation: Evaluation
    { 
        public override double CalculateGrade()
        {
            return Grade;
        }
    }
}
