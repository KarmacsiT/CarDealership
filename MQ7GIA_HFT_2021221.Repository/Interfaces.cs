using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T GetOne(int id); //Read One
        
        IQueryable<T> GetAll(); //Read All
        
        void Update(T entity);

        void Delete(T entity);
    }
    
//--------------------------------------------------------------------//
    
    public interface ICarsRepository : IRepository<Cars>
    {
        void AddCar(int id, string brand, string modell, string licensePlate, int warranty,
            double engineDisplacement, string fuelType, int horsePower, string transmission, int mileage,
            string motUntil, int leasePrice, int sellingPrice);
        void ChangeNumericData(int id, string valueType, int newValue); //We can use this later for more specific functionality 

        void ChangeMOT(int id, string newMOT); //In case the MOT expires we can set it to null

        void DeleteCar(int id);
    
    }

//-------------------------------------------------------------------------//
    public interface ICustomersRepository : IRepository<Customers>
    {
        void AddCustomer(int id, string firstName, string lastName, string email, long phoneNumber);
        
        void ChangeEmail(int id, string newEmail);
        
        void ChangePhonenumber(int id, long newPhonenumber);
        
        void DeleteCustomer(int id);
    
    }
 //-------------------------------------------------------------------------------//   
    
    public interface IContractsRepository : IRepository<Contracts>
    {
        void AddContract(int id, string type, string date, string expiryDate);
        
        void ChangeContractExpiryDate(int id, string newContractExpiryDate);

        void DeleteContract(int id);

    }

//----------------------------------------------------------------------------------//    

    public interface IDepartmentsRepository : IRepository<Departments>
    {
        void AddDepartment(int id, string name, string address);
        
        void ChangeAddress(int id, string newAddress);

        void DeleteDepartment(int id);
    }
}
