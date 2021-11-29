using ConsoleTools;
using MQ7GIA_HFT_2021221.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace MQ7GIA_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Extend Menus with other functionalities

            var carsMenu = new ConsoleMenu()
                .Add("=> Add new Car", () => CreateCar())
                .Add("=> List all Cars", () => GetAllCars())
                .Add("=> Search Car By ID", () => GetCarByID())
                .Add("=> Change MOT of a Car", () => ChangeMOT())
                .Add("=> Change numeric data of a Car", () => ChangeNumericData())
                .Add("=> Delete Car", () => DeleteCar())
                .Add("<= Back to the Main Menu", ConsoleMenu.Close)

                .Configure(config =>
                {
                    config.Title = "[<] Cars Menu [>]";
                    config.EnableWriteTitle = true;
                });

            var contractsMenu = new ConsoleMenu()
                .Add("=> Add new Contract", () => CreateContract())
                .Add("=> List all contracts", () => GetAllContracts())
                .Add("=> Search Contract By ID", () => GetContractByID())
                .Add("=> Change Contract ExpiryDate of a Contract", () => ChangeContractExpiryDate())
                .Add("=> Delete Contract", () => DeleteContract())
                .Add("<= Back to the Main Menu", ConsoleMenu.Close)
             .Configure(config =>
             {
                 config.Title = "[<] Contracts Menu [>]";
                 config.EnableWriteTitle = true;
             });

            var customersMenu = new ConsoleMenu()
                .Add("=> Add new Customer", () => CreateCustomer())
                .Add("=> List all customers", () => GetAllCustomers())
                .Add("=> Search Customer By ID", () => GetCustomerByID())
                .Add("=> Change Email of a Customer", () => ChangeEmail())
                .Add("=> Change Phonenumber of Customer", () => ChangePhoneNumber())
                .Add("=> List Customers without warranty", () => GetCustomersWithoutWarranty())
                .Add("=> List Customers based on fuel type", () => CustomersBasedOnFuelType())
                .Add("=> Customer of a Car",() => CustomerOfCar())
                .Add("=> Total LeaseExpenditure of a Customer", () => TotalLeaseExpenditureOfCustomer())
                .Add("=> Delete Customer",() => DeleteCustomer())
                .Add("<= Back to the Main Menu", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Title = "[<] Customers Menu [>]";
                config.EnableWriteTitle = true;
            });

            var departmentsMenu = new ConsoleMenu()
                .Add("=> Add new Department", () => CreateDepartment())
                .Add("=> List all departments", () => GetAllDepartments())
                .Add("=> Search Department By ID", () => GetDepartmentByID())
                .Add("=> Change Address of a Department", () => ChangeAddressOfDepartment())
                .Add("=> List all cars on a specified Department", () => CarsOnThisDepartment())
                .Add("=> Delete Department", () => DeleteDepartment())
                .Add("<= Back to the Main Menu", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Title = "[<] Departments Menu [>]";
                config.EnableWriteTitle = true;
            });

            var mainMenu = new ConsoleMenu()
                .Add("[Managing/Querrying Cars]", () => carsMenu.Show())
                .Add("[Managing/Querrying Contracts]", () => contractsMenu.Show())
                .Add("[Managing/Querrying Customers]", () => customersMenu.Show())
                .Add("[Managing/Querrying Departments]", () => departmentsMenu.Show())
                .Add("[Quit]", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Title = "$$ CarDealership Database $$";
                    config.EnableBreadcrumb = true;
                });

            mainMenu.Show();
        }

        #region Cars CRUD
        static void CreateCar()
        {
            Random random = new Random();

            Console.WriteLine("\n Specify Brand:");
            string brand = Console.ReadLine();
            Console.WriteLine("\n Specify Modell:");
            string modell = Console.ReadLine();
            Console.WriteLine("\n Specify License Plate:");
            string licensePlate = Console.ReadLine();
            Console.WriteLine("\n Specify Warranty(measured in years):");
            int warranty = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Specify EngineDisplacement (Input Format: int.int => E.g.: 2,0):");
            double engineDisplacement = double.Parse(Console.ReadLine());
            Console.WriteLine("\n Specify Fuel Type (Petrol,Diesel,Electric):");
            string fuelType = Console.ReadLine();
            Console.WriteLine("\n Specify HorsePower:");
            int horsePower = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Specify Transmission type (Automatic,Manual):");
            string transmission = Console.ReadLine();
            Console.WriteLine("\n Specify Mileage (measured in km):");
            int mileage = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Specify MOT Expiery Date (Format: YYYY.MM.DD):");
            string motUntil = Console.ReadLine();
            Console.WriteLine("\n Specify Leasing Price:");
            int leasePrice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Specify Selling Price:");
            int sellingPrice = int.Parse(Console.ReadLine());

            Cars car = new Cars()
            {
                //CarID generates automatically
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
                SellingPrice = sellingPrice,
                DepartmentID = random.Next(1, 3)
            };

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(car),
                Encoding.UTF8,
                "application/json");

            var response = httpClient
                .PostAsync("http://localhost:3851/cars/AddCar",
                httpContent)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"{brand} {modell} has been added to the lot.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void GetAllCars()
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync("http://localhost:3851/cars/AllCars") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var cars = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Cars>>(responseContent);

            Console.WriteLine("List of available Cars:\n");
            foreach (var car in cars)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(car))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(car);
                    if (name != "Department")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }


        static void GetCarByID()
        {
            Console.WriteLine("Specify the searched Car's ID:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClient = new HttpClient();
            var response = httpClient
                    .GetAsync($"http://localhost:3851/cars/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCar = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Cars>(responseContent);

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(searchedCar))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(searchedCar);
                if (name != "Department")
                {
                    Console.WriteLine($"{name}: {value}");
                }

            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void ChangeMOT()
        {
            Console.WriteLine("Specify CarID where you want to change the MOT:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/cars/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCar = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Cars>(responseContent);

            Console.WriteLine("Specify new MOT (Format: YYYY.MM.DD):");
            string newMOT = Console.ReadLine();

            Console.WriteLine($"Current MOT of Vechicle: {searchedCar.MOTUntil}");
            searchedCar.MOTUntil = DateTime.Parse(newMOT);
            Console.WriteLine($"*New* MOT of Vechicle: {searchedCar.MOTUntil}");

            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(searchedCar),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();
            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/cars/ChangeMOTID", httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"\nMOT of {searchedCar.CarBrand} {searchedCar.CarModell} updated.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void ChangeNumericData() //Modelsbe tenni egy helper objectet és úgy átvinni egy objectbe a három parametert
        {
            Console.WriteLine("Specify CarID where you want to change any numeric Data:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/cars/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();


            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCar = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Cars>(responseContent);

            Console.WriteLine("You can change the following value:");
            foreach (PropertyInfo propertyInfo in searchedCar.GetType().GetProperties())
            {
                string name = propertyInfo.Name;

                if (name != "CarID" || name != null)
                {

                    if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
                    {
                        Console.WriteLine($"-{name} => Type: {propertyInfo.PropertyType.Name}");
                    }
                }


            }
           
            Console.WriteLine("Specify the field you want to change:");
            string fieldName = Console.ReadLine();
            Console.WriteLine("Specify the new value of this field :");
            string fieldValue = Console.ReadLine();
            
            ChangeNumericDataHelper changeNumericDataHelper = new ChangeNumericDataHelper(id, fieldName, int.Parse(fieldValue));
            
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(changeNumericDataHelper),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();
            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/cars/ChangeNumericData", httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"{fieldName} property of {searchedCar.CarBrand} {searchedCar.CarModell} is updated.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void DeleteCar()
        {
            Console.WriteLine("Specify the ID of the Car you want to delete:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var responseRead = httpClientRead
                    .GetAsync($"http://localhost:3851/cars/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = responseRead  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCar = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Cars>(responseContent);
            Console.WriteLine($"\nAre you sure you want to delete {searchedCar.CarBrand} {searchedCar.CarModell} from the Database? (y/n)");
            string yesorno = Console.ReadLine();
            if (yesorno == "y")
            {
                HttpClient httpClientDelete = new HttpClient();
                var responseDelete = httpClientDelete
                        .DeleteAsync($"http://localhost:3851/cars/{id}") //Sends get request
                        .GetAwaiter() //We get rid of the Task parameter in this fashion
                        .GetResult();

                Console.WriteLine("\nItem succesfully deleted.");
            }

            else
            {
                Console.WriteLine("Delete process canceled.");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        #endregion
        #region Cars Non-CRUD
        static void GetCustomersWithoutWarranty() //Not working, LINQ Exception most likely nav. prop.
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient
                    .GetAsync($"http://localhost:3851/cars/CustomersWithoutWarranty") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var customers = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<List<Customers>>(responseContent);

            Console.WriteLine("List of Customers without warranty:");
            foreach (var customer in customers)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(customer))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(customer);
                    if (name != "Contract")
                    {
                        Console.WriteLine($"{name}: {value}");
                    }

                }
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        #endregion
        static void CreateContract()
        {
            Console.WriteLine("\n Specify Type:");
            string type = Console.ReadLine();
            Console.WriteLine("\n Specify Date:");
            string date = Console.ReadLine();
            Console.WriteLine("\n Specify Expiry Date :");
            string expirydate = Console.ReadLine();
            Console.WriteLine("\nSpecify the CarID which the contract belongs to:");
            string id = Console.ReadLine();

            Contracts contract = new Contracts()
            {
                //ContractsID generates automatically
                ContractType = type,
                ContractDate = DateTime.Parse(date),
                ContractExpiryDate = DateTime.Parse(expirydate),
                CustomerID = int.Parse(id),
                CarID = int.Parse(id)
            };

            Console.WriteLine("\n You need to bind a new car for the newly printed contract:");
            CreateCar();

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(contract),
                Encoding.UTF8,
                "application/json");

            var response = httpClient
                .PostAsync("http://localhost:3851/contracts/AddContract",
                httpContent)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"The new {type} contract has been printed.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void GetAllContracts()
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync("http://localhost:3851/contracts/AllContracts") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var contracts = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Contracts>>(responseContent);

            Console.WriteLine("List of available Contracts:\n");
            foreach (var contract in contracts)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(contract))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(contract);
                    if (name !="Customer" || name !="Car")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void GetContractByID()
        {
            Console.WriteLine("Specify the searched Contracts's ID:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClient = new HttpClient();

            var response = httpClient
                    .GetAsync($"http://localhost:3851/contracts/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedContract = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Contracts>(responseContent);

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(searchedContract))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(searchedContract);
                if (name != "Customer" || name != "Car")
                {
                    Console.WriteLine($"{name}: {value}");
                }

            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void ChangeContractExpiryDate()
        {
            Console.WriteLine("Specify ContractID where you want to change the Contract ExpiryDate:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/contracts/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedContract = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Contracts>(responseContent);

            Console.WriteLine("Specify new Contract ExpiryDate (Format: YYYY.MM.DD):");
            string newContractExpiryDate = Console.ReadLine();

            Console.WriteLine($"Current Contract ExpiryDate of Contract: {searchedContract.ContractExpiryDate}");
            searchedContract.ContractExpiryDate = DateTime.Parse(newContractExpiryDate);
            Console.WriteLine($"*New* ContractExpiryDate of Contract: {searchedContract.ContractExpiryDate}");

            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(searchedContract),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();
            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/contracts/ChangeContractExpiryDate", httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"\nContractExpiryDate of Contract {searchedContract.ContractID} has been updated.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void DeleteContract()
        {
            Console.WriteLine("Specify the ID of the Contract you want to delete:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var responseRead = httpClientRead
                    .GetAsync($"http://localhost:3851/contracts/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = responseRead  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedContract = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Contracts>(responseContent);
            Console.WriteLine($"\nAre you sure you want to delete Contract {searchedContract.ContractID} ({searchedContract.ContractType}) from the Database? (y/n)");
            string yesorno = Console.ReadLine();
            if (yesorno == "y")
            {
                HttpClient httpClientDelete = new HttpClient();
                var responseDelete = httpClientDelete
                        .DeleteAsync($"http://localhost:3851/contracts/{id}") //Sends get request
                        .GetAwaiter() //We get rid of the Task parameter in this fashion
                        .GetResult();

                Console.WriteLine("\nItem succesfully deleted.");
            }
            else
            {
                Console.WriteLine("Delete process canceled.");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        #region Non-Crud Contracts
        static void CustomerOfCar()
        {
            Console.WriteLine("Specify the ID of the car who\'s customer you are intersted in:");
            int CustomerOfCarID = int.Parse(Console.ReadLine());

           HttpClient httpClientContract = new HttpClient();

            var Contractresponse = httpClientContract
                    .GetAsync($"http://localhost:3851/contracts/AllContracts") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContractContent = Contractresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var contracts = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<List<Contracts>>(responseContractContent);

            Contracts SearchedContract = new Contracts();
            foreach (var contract in contracts)
            {
                if (contract.CustomerID == CustomerOfCarID)
                {
                    SearchedContract = contract;
                    break;
                }
            }

            HttpClient httpClientCustomer = new HttpClient();
            int id = SearchedContract.CustomerID;
            
            var response = httpClientCustomer
                    .GetAsync($"http://localhost:3851/customers/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCustomer = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Customers>(responseContent);

            Console.WriteLine("The Customer of the Car:\n");
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(searchedCustomer))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(searchedCustomer);
                if (name != "Contract")
                {
                    Console.WriteLine($"{name}: {value}");
                }

            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void TotalLeaseExpenditureOfCustomer()
        {
            Console.WriteLine("Specify CustomerID:");
            int TotalLeaseExpenditureForCustomerID = int.Parse(Console.ReadLine());

            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync($"http://localhost:3851/contracts_noncrud/{TotalLeaseExpenditureForCustomerID}") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var total = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<int>(responseContent);

            Console.WriteLine($"Total Lease Expenditure of CustomerID {TotalLeaseExpenditureForCustomerID} is {total} HUF. ");
            
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();

        }
        #endregion
        static void CreateCustomer()    //internal server error 500, Auto ID not working
        {
            Console.WriteLine("\n Specify FirstName:");
            string firstname = Console.ReadLine();
            Console.WriteLine("\n Specify LastName:");
            string lastname = Console.ReadLine();
            Console.WriteLine("\n Specify Email:");
            string email = Console.ReadLine();
            Console.WriteLine("\nSpecify PhoneNumber:");
            string phonenumber = Console.ReadLine();
            Console.WriteLine("\nSpecify the contract id of customer:");
            string contractid = Console.ReadLine();

            Customers customer = new Customers()
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                PhoneNumber = long.Parse(phonenumber),
                ContractId = int.Parse(contractid)
            };

            Console.WriteLine("\nWrite a new contract for the new customer:");
            CreateContract();
            
            HttpClient httpClient = new HttpClient();
                
            HttpContent httpContent = new StringContent
                    (JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json");

                var response = httpClient
                    .PostAsync("http://localhost:3851/customers/AddCustomer",
                    httpContent)
                    .GetAwaiter()
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"The {customer.FirstName} {customer.LastName} has been added to the Customer List.");
                Console.WriteLine("\nPress any key to continue..");
                Console.ReadLine();
        }
        
        static void GetAllCustomers()
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync("http://localhost:3851/customers/AllCustomers") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var customers = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Customers>>(responseContent);

            Console.WriteLine("List of available Customers:\n");
            foreach (var customer in customers)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(customer))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(customer);
                    if (name != "Customer" || name != "Car")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void GetCustomerByID()
        {
            Console.WriteLine("Specify the searched Customers's ID:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClient = new HttpClient();

            var response = httpClient
                    .GetAsync($"http://localhost:3851/customers/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCustomer = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Customers>(responseContent);

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(searchedCustomer))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(searchedCustomer);
                if (name != "Contract")
                {
                    Console.WriteLine($"{name}: {value}");
                }

            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        
        static void ChangeEmail()
        {
            Console.WriteLine("Specify CustomerID where you want to change the Email:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/customers/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCustomer = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Customers>(responseContent);

            Console.WriteLine("Specify new Email:");
            string newEmail = Console.ReadLine();

            Console.WriteLine($"Current Email of Customer: {searchedCustomer.Email}");
            
            searchedCustomer.Email = newEmail;
            
            Console.WriteLine($"*New* Email of Customer: {searchedCustomer.Email}");

            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(searchedCustomer),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();
            
            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/customers/ChangeEmail",httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"\nEmail of {searchedCustomer.FirstName} {searchedCustomer.LastName} has been updated.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void ChangePhoneNumber()
        {
            Console.WriteLine("Specify CustomerID where you want to change the Phonenumber:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/customers/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCustomer = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Customers>(responseContent);

            Console.WriteLine("Specify new Phonenumber:");
            string newPhonenumber = Console.ReadLine();

            Console.WriteLine($"Current Phonenumber of Customer: {searchedCustomer.PhoneNumber}");

            searchedCustomer.PhoneNumber = long.Parse(newPhonenumber);

            Console.WriteLine($"*New* Phonenumber of Customer: {searchedCustomer.PhoneNumber}");

            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(searchedCustomer),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();

            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/customers/ChangePhoneNumber", httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"\nPhonenumber of {searchedCustomer.FirstName} {searchedCustomer.LastName} has been updated.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void DeleteCustomer()
        {
            Console.WriteLine("Specify the ID of the Customer you want to delete:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var responseRead = httpClientRead
                    .GetAsync($"http://localhost:3851/customers/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = responseRead  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedCustomer = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Customers>(responseContent);
            Console.WriteLine($"\nAre you sure you want to delete {searchedCustomer.FirstName} {searchedCustomer.LastName} from the Database? (y/n)");
            string yesorno = Console.ReadLine();
            if (yesorno == "y")
            {
                HttpClient httpClientDelete = new HttpClient();
                var responseDelete = httpClientDelete
                        .DeleteAsync($"http://localhost:3851/customers/{id}") //Sends get request
                        .GetAwaiter() //We get rid of the Task parameter in this fashion
                        .GetResult();

                Console.WriteLine("\nItem succesfully deleted.");
            }

            else
            {
                Console.WriteLine("Delete process canceled.");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }

        static void CustomersBasedOnFuelType()
        {
            Console.WriteLine("Specify the fuel type you want to search for (petrol,diesel,electric):");
            string fuel_type = Console.ReadLine();

            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync($"http://localhost:3851/customers_extra/{fuel_type}") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var customers = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Customers>>(responseContent);

            Console.WriteLine("Customers with this fuel type:\n");
            foreach (var customer in customers)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(customer))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(customer);
                    if (name != "Department")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        
        }
        static void CreateDepartment()
        {
            Console.WriteLine("\n Specify DepartmentName:");
            string departmentname = Console.ReadLine();
            Console.WriteLine("\n Specify Address:");
            string address = Console.ReadLine();

            Departments department = new Departments()
            {
                //DepartmentID generates automatically
                DepartmentName = departmentname,
                Address = address,
                CarCollection = null
            };

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(department),
                Encoding.UTF8,
                "application/json");

            var response = httpClient
                .PostAsync("http://localhost:3851/departments/AddDepartment",
                httpContent)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"The {department.DepartmentName} has been added to the Departments.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void GetAllDepartments()
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync("http://localhost:3851/departments/AllDepartments") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var departments = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Departments>>(responseContent);

            Console.WriteLine("List of available Departments:\n");
            foreach (var department in departments)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(department))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(department);
                    if (name != "CarCollection")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void GetDepartmentByID()
        {
            Console.WriteLine("Specify the searched Departments's ID:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClient = new HttpClient();

            var response = httpClient
                    .GetAsync($"http://localhost:3851/departments/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedDepartment = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Departments>(responseContent);

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(searchedDepartment))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(searchedDepartment);
                if (name != "CarCollection")
                {
                    Console.WriteLine($"{name}: {value}");
                }

            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void ChangeAddressOfDepartment()
        {
            Console.WriteLine("Specify DepartmentID where you want to change the address:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var Readresponse = httpClientRead
                    .GetAsync($"http://localhost:3851/departments/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = Readresponse  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedDepartment = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Departments>(responseContent);

            Console.WriteLine("Specify new Address:");
            string newAddress = Console.ReadLine();

            Console.WriteLine($"Current Address of Department: {searchedDepartment.Address}");
            searchedDepartment.Address = newAddress;
            Console.WriteLine($"*New* Address of Department: {searchedDepartment.Address}");

            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(searchedDepartment),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClientUpdate = new HttpClient();
            var response = httpClientUpdate
                    .PutAsync($"http://localhost:3851/departments/ChangeAddress", httpContent) //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            Console.WriteLine($"\n{searchedDepartment.DepartmentName} has been moved to {searchedDepartment.Address}.");
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void DeleteDepartment()
        {
            Console.WriteLine("Specify the ID of the Department you want to delete:");
            int id = int.Parse(Console.ReadLine());

            HttpClient httpClientRead = new HttpClient();
            var responseRead = httpClientRead
                    .GetAsync($"http://localhost:3851/departments/{id}") //Sends get request
                    .GetAwaiter() //We get rid of the Task parameter in this fashion
                    .GetResult();

            var responseContent = responseRead  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var searchedDepartment = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                    .DeserializeObject<Departments>(responseContent);
            Console.WriteLine($"\nAre you sure you want to delete {searchedDepartment.DepartmentName} from the Database? (y/n)");
            string yesorno = Console.ReadLine();
            if (yesorno == "y")
            {
                HttpClient httpClientDelete = new HttpClient();
                var responseDelete = httpClientDelete
                        .DeleteAsync($"http://localhost:3851/departments/{id}") //Sends get request
                        .GetAwaiter() //We get rid of the Task parameter in this fashion
                        .GetResult();

                Console.WriteLine("\nItem succesfully deleted.");
            }

            else
            {
                Console.WriteLine("Delete process canceled.");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
        static void CarsOnThisDepartment()
        {
            Console.WriteLine("Specify DepartmentID, where you want to list the cars from:");
            int id = int.Parse(Console.ReadLine());
            
            HttpClient httpClient = new HttpClient();

            var response = httpClient
                .GetAsync($"http://localhost:3851/departments_extra/{id}") //Sends get request
                .GetAwaiter() //We get rid of the Task parameter in this fashion
                .GetResult();

            var responseContent = response  //We get JSON here
                .Content
                .ReadAsStringAsync() //Convert it to string
                .GetAwaiter()
                .GetResult();

            var cars = JsonConvert  //We get this command from Netonsoft.Json Nuget package
                .DeserializeObject<List<Cars>>(responseContent);

            Console.WriteLine("Cars on this premiss:\n");
            foreach (var car in cars)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(car))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(car);
                    if (name != "Department")
                    {
                        Console.WriteLine($"{name}:\t{value}");
                    }

                }
                Console.WriteLine("\n-----------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
        }
    }
}
