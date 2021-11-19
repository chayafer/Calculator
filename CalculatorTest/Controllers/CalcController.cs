
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

        public CalcController(IEnumerable<IOperatorService> services,IOperatorList operatorList)
        {
            _services = services;
            _operatorList = operatorList;
        }

        

        [HttpGet]
        [Route("Calc")]
        public ActionResult<float> Calc([FromQuery][Required]float param1, [FromQuery][Required] float param2, [FromQuery][Required] string operatorName)
        {
            var service = _services.Where(s => s.Name ==operatorName).Single();

            return Ok(service.Calc(param1,param2));
        }

        [HttpGet]
        [Route("Operators")]
        public ActionResult<string[]> GetOperators()
        {
            return Ok(_operatorList.GetOperators());
        }

        
    }
}
