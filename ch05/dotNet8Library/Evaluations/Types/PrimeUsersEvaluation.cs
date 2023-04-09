using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet8Library.Evaluations.Types
{
    public class PrimeUsersEvaluation : Evaluation
    {
        /// <summary>
        /// The business rule implemented here indicates that grades that came from prime users have 20% of increase
        /// </summary>
        /// <returns>the final grade from a prime user</returns>
        public override double CalculateGrade()
        {
            return Grade * 1.2;
        }
    }
}
