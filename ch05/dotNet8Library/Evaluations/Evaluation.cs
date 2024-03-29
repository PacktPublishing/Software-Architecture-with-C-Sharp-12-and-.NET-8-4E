﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet8Library.Evaluations
{
    public class Evaluation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public int Grade { get; set; }

        public virtual double CalculateGrade()
        {
            return 0;
        }
    }
}
