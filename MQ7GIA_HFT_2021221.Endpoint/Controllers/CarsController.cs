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
    [Route("cars")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsLogic _carsLogic;

        public CarsController(ICarsLogic carsLogic)
        {
            _carsLogic = carsLogic;
        }

        [HttpGet("AllCars")] //works
        public IList<Cars> GetAllCarsResult()
        {
            return _carsLogic.GetAllCars();
        }

        [HttpGet("{id}")] //works
        public Cars GetCarByIDResult(int id)
        {
            return _carsLogic.GetCarById(id);
        }

        [HttpGet("CustomersWithoutWarranty")] //LINQ Exception becasue of navigation property
        public List<Customers> CustomersWithoutWarrantyResult()
        {
            return _carsLogic.CustomersWithoutWarranty();
        }

        [HttpPost("AddCar")] //works
        public void CreateCarResult(Cars car)
        {
            _carsLogic.AddCar(car.CarID, car.CarBrand, car.CarModell, car.LicensePlate, (int)car.Warranty, (double)car.EngineDisplacement, car.FuelType, car.HorsePower, car.Transmission, car.Mileage, car.MOTUntil.ToString(), car.LeasePrice, car.SellingPrice);
        }

        [HttpPut("ChangeMOTID")] //works
        public void ChangeMOTResult(Cars car)
        {
            _carsLogic.ChangeMOT(car.CarID, car.MOTUntil.ToString());
        }


        [HttpPut("ChangeNumericData")] //kinda works
        public void ChangeNumericDataActionResult(Cars car)
        {
            string valuetype = "mileage";
            int resultInt = 5000;
            _carsLogic.ChangeNumericData(car.CarID, valuetype, resultInt);
        }

        [HttpDelete("{id}")] //works
        public void DeleteCarResult(int id)
        {
            _carsLogic.DeleteCar(id);
        }
    }
}
