using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorTest.Services
{
    public class MinusService : IOperatorService
    {
        public string Name { get => "-"; }
        public float Calc(float param1, float param2)
        {

            return param1 - param2;
        }
    }
}
