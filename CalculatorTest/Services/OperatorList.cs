using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorTest.Services
{
    public class OperatorList : IOperatorList
    {
        private IConfiguration _configuration;

        public OperatorList(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string[] GetOperators()
        {
            var operators = _configuration.GetSection("Operators")?.Value;
            if (string.IsNullOrEmpty(operators))
                return null;
            return operators.Split(",");
        }
    }
}
