using CodeMetricsBadCode.CouplingSample.Execution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeMetricsBadCode.CouplingSample
{
    class MasterClass
    {
        private ExecutionTypeA executionTypeA = new ExecutionTypeA();

        private ExecutionTypeB executionTypeB = new ExecutionTypeB();

        private ExecutionTypeC executionTypeC = new ExecutionTypeC();
        public void ProcessData()
        {
            executionTypeA.ExecuteTypeA();
            executionTypeB.ExecuteTypeB();
            executionTypeC.ExecuteTypeC();

        }
    }
}
