using Microsoft.AspNetCore.Mvc;
using MQ7GIA_HFT_2021221.Logic;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    [Route("departments_extra")]
    public class CarsOnThisDepartmentController_Non_Crud_: ControllerBase
    {
        private readonly IDepartmentsLogic _departmentsLogic;

        public CarsOnThisDepartmentController_Non_Crud_(IDepartmentsLogic departmentsLogic) 
        {
            _departmentsLogic = departmentsLogic;
        }

        [HttpGet("{id}")] //works
        public List<Cars> CarsOnThisDeparmentResult(int id)
        {
           return _departmentsLogic.CarsOnThisDeparment(id);
        }

    }
}
