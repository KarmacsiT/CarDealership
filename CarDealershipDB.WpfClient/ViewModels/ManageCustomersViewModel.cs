using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarDealershipDB.WpfClient.ViewModels
{
    class ManageCustomersViewModel : ObservableRecipient
    {
        private string _endpoint = "http://localhost:3851/customers";

        private ApiClient _apiClient = new ApiClient();

        private ObservableCollection<Customers> _customersObservableCollection;
        public ObservableCollection<Customers> CustomersObservableCollection
        {
            get => _customersObservableCollection;
            set => SetProperty(ref _customersObservableCollection, value);
        }

        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
            }
        }

        private int _selectedCustomerIndex;

        public int SelectedCustomerIndex
        {
            get => _selectedCustomerIndex;
            set => SetProperty(ref _selectedCustomerIndex, value);
        }

        public void Setup(Customers customer)
        {
            SelectedCustomer = customer;
        }

        public RelayCommand AddCustomerCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand EditCustomerCommand { get; set; }
        public RelayCommand CustomersWithoutWarrantyCommand { get; set; }
        public RelayCommand CustomersBasedOnFuelTypeCommand { get; set; }

        public ManageCustomersViewModel()
        {
            CustomersObservableCollection = new ObservableCollection<Customers>();

            _apiClient.GetAsync<List<Customers>>(_endpoint)
                .ContinueWith((customerTask) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CustomersObservableCollection = new ObservableCollection<Customers>(customerTask.Result);
                    });
                });

            AddCustomerCommand = new RelayCommand(AddCustomer);
            EditCustomerCommand = new RelayCommand(EditCustomer);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
            CustomersWithoutWarrantyCommand = new RelayCommand(CustomersWithoutWarranty);
            CustomersBasedOnFuelTypeCommand = new RelayCommand(CustomersBasedOnFuel);

        }



        private void AddCustomer()
        {
            AddOrEditCustomerDialog addOrEditCustomerDialog = new AddOrEditCustomerDialog();
            addOrEditCustomerDialog.ShowDialog();

            if (addOrEditCustomerDialog.DialogResult is true)
            {
                SelectedCustomer = addOrEditCustomerDialog.CurrentCustomer;

                _apiClient.PostAsync(SelectedCustomer, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CustomersObservableCollection.Add(SelectedCustomer);
                        });
                    });
            }
        }

        private void EditCustomer()
        {
            AddOrEditCustomerDialog addOrEditCustomerDialog = new AddOrEditCustomerDialog(SelectedCustomer);
            addOrEditCustomerDialog.ShowDialog();

            if (addOrEditCustomerDialog.DialogResult is true)
            {
                Customers editedCustomer = new Customers();
                editedCustomer = addOrEditCustomerDialog.CurrentCustomer;

                _apiClient.DeleteAsync(SelectedCustomer.CustomerID, _endpoint)
                   .ContinueWith(task =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                       {
                           CustomersObservableCollection.Remove(SelectedCustomer);
                       });
                   });

                _apiClient.PostAsync(editedCustomer, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CustomersObservableCollection.Add(editedCustomer);
                        });
                    });
            }

        }

        private void DeleteCustomer()
        {
            Customers deletedCustomer = SelectedCustomer;

            CustomersObservableCollection = Task.Run(async () => await _apiClient.GetAsync<ObservableCollection<Customers>>(_endpoint)).Result; //Refreshing the collection so that the newly given element also has the correct ID

            foreach (var customer in CustomersObservableCollection)
            {
                if (customer.FirstName == deletedCustomer.FirstName && customer.LastName == deletedCustomer.LastName && customer.Email == deletedCustomer.Email && customer.PhoneNumber == deletedCustomer.PhoneNumber)
                {
                    SelectedCustomerIndex = customer.CustomerID;
                    SelectedCustomer = customer;
                }
            }

            _apiClient.DeleteAsync(SelectedCustomerIndex, _endpoint)
                .ContinueWith(task =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CustomersObservableCollection.Remove(SelectedCustomer);
                    });
                });
        }

        private void CustomersWithoutWarranty()
        {
            List<Customers> customersWithoutWarranty = new List<Customers>();
            string customersWithoutWarrantyEndpoint = "http://localhost:3851/cars/CustomersWithoutWarranty";

            customersWithoutWarranty = Task.Run(async () => await _apiClient.GetAsync<List<Customers>>(customersWithoutWarrantyEndpoint)).Result; //Waits for the GetAsync to complete and returns the result



            if (customersWithoutWarranty.Count is not 0)
            {
                string message = $"Customers without Warranty:{Environment.NewLine}";

                foreach (Customers customer in customersWithoutWarranty)
                {
                    message += Environment.NewLine;
                    message += $"- {customer.FirstName} {customer.LastName}";
                }

                MessageBox.Show(message);
            }

            else
            {
                MessageBox.Show("Currently every Customer is covered under Warranty.");
            }
        }
        private void CustomersBasedOnFuel()
        {
            List<Customers> customersBasedOnFuel = new List<Customers>();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            string[] fuelTypes = { "petrol", "diesel", "electric" };

            string customersBasedOnFuelEndpoint = $"http://localhost:3851/customers_extra";

            customersBasedOnFuel = Task.Run(async () => await _apiClient.GetAsyncWithString<List<Customers>>(fuelTypes[0], customersBasedOnFuelEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

            string message = $"Customers with Petrol Vechicle:";
            customersBasedOnFuel = customersBasedOnFuel.Where(customer => customer is not null).ToList();

            foreach (var customer in customersBasedOnFuel)
            {
                message += Environment.NewLine;
                message += $"- {customer.FirstName} {customer.LastName}";
            }

            message += Environment.NewLine;
            message += Environment.NewLine;

            customersBasedOnFuel.Clear();

            customersBasedOnFuel = Task.Run(async () => await _apiClient.GetAsyncWithString<List<Customers>>(fuelTypes[1], customersBasedOnFuelEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

            message += $"Customers with Diesel Vechicle:";
            customersBasedOnFuel = customersBasedOnFuel.Where(customer => customer is not null).ToList();

            foreach (var customer in customersBasedOnFuel)
            {
                message += Environment.NewLine;
                message += $"- {customer.FirstName} {customer.LastName}";
            }

            message += Environment.NewLine;
            message += Environment.NewLine;

            customersBasedOnFuel.Clear();

            customersBasedOnFuel = Task.Run(async () => await _apiClient.GetAsyncWithString<List<Customers>>(fuelTypes[2], customersBasedOnFuelEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

            message += $"Customers with Electric Vechicle:";
            customersBasedOnFuel = customersBasedOnFuel.Where(customer => customer is not null).ToList();

            foreach (var customer in customersBasedOnFuel)
            {
                message += Environment.NewLine;
                message += $"- {customer.FirstName} {customer.LastName}";
            }

            MessageBox.Show(message);

        }

    }
}

