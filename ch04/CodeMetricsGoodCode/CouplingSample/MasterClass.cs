using CodeMetricsGoodCode.CouplingSample.Execution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeMetricsGoodCode.CouplingSample
{
    class MasterClass
    {
        private IExecutionType executionTypeA = new ExecutionTypeA();

        private IExecutionType executionTypeB = new ExecutionTypeB();

        private IExecutionType executionTypeC = new ExecutionTypeC();
        public void ProcessData()
        {
            executionTypeA.Execute();
            executionTypeB.Execute();
            executionTypeC.Execute();

        }
    }
}
