using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221.Data;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Repository
{
    public class CarsRepository : Repository<Cars>, ICarsRepository
    {
        public CarsRepository(CarDealershipContext cd_ctx) : base(cd_ctx) { /*Empty on purpose*/ }

        public void AddCar(int id, string brand, string modell, string licensePlate, int? warranty,
            double? engineDisplacement, string fuelType, int horsePower, string transmission, int mileage,
            string motUntil, int leasePrice, int sellingPrice)
        {
            if (fuelType.ToUpper() == "Petrol".ToUpper() || fuelType.ToUpper() == "Diesel".ToUpper() || fuelType.ToUpper() == "Electric".ToUpper())
            {
                if (transmission.ToUpper() == "Automatic".ToUpper() || transmission.ToUpper() == "Manual".ToUpper())
                {
                    if (leasePrice < sellingPrice)
                    {
                        cd_ctx.Cars.Add(new Cars
                        {
                            CarID = id,
                            CarBrand = brand,
                            CarModell = modell,
                            LicensePlate = licensePlate,
                            Warranty = warranty,
                            EngineDisplacement = engineDisplacement,
                            FuelType = fuelType,
                            HorsePower = horsePower,
                            Transmission = transmission,
                            Mileage = mileage,
                            MOTUntil = DateTime.Parse(motUntil),
                            LeasePrice = leasePrice,
                            SellingPrice = sellingPrice
                        });
                    }
                    
                    else
                    {
                        throw new Exception("The mounthly LeasePrice can't be higher than the SellingPrice, thats irrational.");
                    }  
                }
                
                else
                {
                    throw new Exception("The inputed transmission is not valid or misspelled.");
                }
            }
            
            else
            {
                throw new Exception("The inputed fuel type is not valid or misspelled.");
            } 
            
            cd_ctx.SaveChanges();
        }

        public void ChangeMOT(int id, string newMOT)
        {
            var Car = GetOne(id);
            Car.MOTUntil = DateTime.Parse(newMOT);
            
            cd_ctx.SaveChanges();
        }

        public void ChangeNumericData(int id, string valueType, int newValue)
        {
            //CarID and numeric car specs (Horsepower etc.) should not be changed
            
            var Car = GetOne(id);
            
            switch (valueType.ToLower()) //To avoid capitalization errors
            {
                case "warranty":
                    Car.Warranty = newValue;
                    cd_ctx.SaveChanges();
                    break;
                
                case "mileage":
                    Car.Mileage = newValue;
                    cd_ctx.SaveChanges();
                    break;
                
                case "leaseprice":
                    Car.LeasePrice = newValue;
                    cd_ctx.SaveChanges();
                    break;
                
                case "sellingprice":
                    Car.SellingPrice = newValue;
                    cd_ctx.SaveChanges();
                    break;
                
                default:
                    throw new Exception("The given numeric field can't be changed or does not exist");        
            }
        }

        public void DeleteCar(int id)
        {
            var CarToDelete = cd_ctx.Cars.FirstOrDefault(x => x.CarID == id);
            cd_ctx.Cars.Remove(CarToDelete);
            
            cd_ctx.SaveChanges();
        }

        public override Cars GetOne(int id)
        {
            return GetAll().SingleOrDefault(item => item.CarID == id);
        }
    }
}
