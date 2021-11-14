using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MQ7GIA_HFT_2021221.Logic
{
    public interface ICarsLogic
    {
        void AddCar(int id, string brand, string modell, string licensePlate, int warranty,
           double engineDisplacement, string fuelType, int horsePower, string transmission, int mileage,
           string motUntil, int leasePrice, int sellingPrice);
        void ChangeNumericData(int id, string valueType, int newValue); 

        void ChangeMOT(int id, string newMOT);

        void DeleteCar(int id);

        IList<Cars> GetAllCars();
        
        Cars GetCarById(int id);

        List<Customers> CustomersWithoutWarranty();
    }
}
