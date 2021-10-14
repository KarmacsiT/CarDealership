using MQ7GIA_HFT_2021221.Data;
using System;
using System.Linq;

namespace MQ7GIA_HFT_2021221.Client
{
    class Program
    {
        //Don't forget to untick .Data dependency on production build
        static void Main(string[] args)
        {
            CarDealershipContext ctx = new CarDealershipContext();
            var Customers = ctx.Customers.Select(x => x.LastName);
            var cars = ctx.Cars.Select(y => y.CarModell);
            var contracts = ctx.Contracts.Select(c => c.ContractID);
            var contractName = ctx.Contracts.Select(c => c.ContractType);
            var depName = ctx.Departments.Select(d => d.DepartmentName);

            foreach (var contract in contracts)
            {
                Console.WriteLine(contract);
            }
            foreach (var contracttype in contractName)
            {
                Console.WriteLine(contracttype);
            }
            foreach (var customer in Customers)
            {
                Console.WriteLine(customer);
            }
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
            foreach (var dep in depName)
            {
                Console.WriteLine(dep);
            }
            Console.ReadKey();
        }
    }
}
