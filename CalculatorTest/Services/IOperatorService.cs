using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorTest.Services
{
    public interface IOperatorService
    {
        public string Name { get; }
        public float Calc(float param1, float param2);
    }
}
