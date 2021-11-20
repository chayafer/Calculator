
using CalculatorTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {
        private readonly IEnumerable<IOperatorService> _services;
        private readonly IOperatorList _operatorList;

        public CalcController(IEnumerable<IOperatorService> services, IOperatorList operatorList)
        {
            _services = services;
            _operatorList = operatorList;
        }



        [HttpGet]
        [Route("Calc")]
        public ActionResult<float> Calc([FromQuery][Required] float param1, [FromQuery][Required] float param2, [FromQuery][Required] string operatorName)
        {
            var opertators = _operatorList.GetOperators();
            if (!opertators.Contains(operatorName))
            {
                throw new Exception($"operator name {operatorName} does not exist in appsettings");
            }

            var service = _services.Where(s => s.Name == operatorName).SingleOrDefault();
            if(service == null)
            {
                throw new Exception($"There is no service with name {operatorName}");
            }
            return Ok(service.Calc(param1, param2));
        }

        [HttpGet]
        [Route("Operators")]
        public ActionResult<string[]> GetOperators()
        {
            var operators = _operatorList.GetOperators();
            if (operators == null || operators.Length == 0)
                throw new Exception("operator list in appsettings is null");
            return Ok(operators);
        }


    }
}
