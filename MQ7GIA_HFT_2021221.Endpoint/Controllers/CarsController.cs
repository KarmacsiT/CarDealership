using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MQ7GIA_HFT_2021221.Endpoint.Services;
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
        ICarsLogic _carsLogic;

        public CarsController(ICarsLogic carsLogic)
        {
            _carsLogic = carsLogic;
        }

        [HttpGet] //works  Originally was [HttpGet("AllCars")] 
        public IList<Cars> GetAllCarsResult()
        {
            return _carsLogic.GetAllCars();
        }

        [HttpGet("{id}")] //works
        public Cars GetCarByIDResult(int id)
        {
            return _carsLogic.GetCarById(id);
        }

        [HttpGet("CustomersWithoutWarranty")] //works
        public List<Customers> CustomersWithoutWarrantyResult()
        {
            return _carsLogic.CustomersWithoutWarranty();
        }

        [HttpPost] //works Originally was [HttpPost]("AddCar")
        public void CreateCarResult([FromBody] Cars car)
        {
            _carsLogic.AddCar(car.CarID, car.CarBrand, car.CarModell, car.LicensePlate, (int)car.Warranty, (double)car.EngineDisplacement, car.FuelType, car.HorsePower, car.Transmission, car.Mileage, car.MOTUntil.ToString(), car.LeasePrice, car.SellingPrice);
        }

        [HttpPut("ChangeMOTID")] //works
        public void ChangeMOTResult(Cars car)
        {
            _carsLogic.ChangeMOT(car.CarID, car.MOTUntil.ToString());
        }


        [HttpPut("ChangeNumericData")] ////Modelsbe tenni egy helper objectet és úgy átvinni egy objectbe a három parametert
        public void ChangeNumericDataActionResult(ChangeNumericDataHelper changeNumericDataHelper)
        {
            _carsLogic.ChangeNumericData(changeNumericDataHelper.searchedID, changeNumericDataHelper.valueType, changeNumericDataHelper.newValue);
        }

        [HttpDelete("{id}")] //works
        public void DeleteCarResult(int id)
        {
            _carsLogic.DeleteCar(id);
        }
    }
}
