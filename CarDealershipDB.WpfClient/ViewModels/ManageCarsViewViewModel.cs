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
    public class ManageCarsViewViewModel : ObservableRecipient
    {
        private string _endpoint = "http://localhost:3851/cars";

        private ApiClient _apiClient = new ApiClient();

        private ObservableCollection<Cars> _carsObservableCollection;
        public ObservableCollection<Cars> CarsObservableCollection
        {
            get => _carsObservableCollection;
            set => SetProperty(ref _carsObservableCollection, value);
        }

        private Cars _selectedCar;

        public Cars SelectedCar
        {
            get => _selectedCar;
            set
            {
                SetProperty(ref _selectedCar, value);
            }
        }

        private int _selectedCarIndex;

        public int SelectedCarIndex
        {
            get => _selectedCarIndex;
            set => SetProperty(ref _selectedCarIndex, value);
        }

        public void Setup(Cars car)
        {
            SelectedCar = car;
        }

        public RelayCommand AddCarCommand { get; set; }
        public RelayCommand DeleteCarCommand { get; set; }
        public RelayCommand EditCarCommand { get; set; }
        public RelayCommand CustomerOfCarCommand { get; set; }

        public ManageCarsViewViewModel()
        {
            CarsObservableCollection = new ObservableCollection<Cars>();

            _apiClient.GetAsync<List<Cars>>(_endpoint)
                .ContinueWith((carsTask) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CarsObservableCollection = new ObservableCollection<Cars>(carsTask.Result);
                    });
                });

            AddCarCommand = new RelayCommand(AddCar);
            EditCarCommand = new RelayCommand(EditCar);
            DeleteCarCommand = new RelayCommand(DeleteCar);
            CustomerOfCarCommand = new RelayCommand(CustomerOfCar);
        }

        private void AddCar()
        {
            AddOrEditCarDialog addOrEditCarDialog = new AddOrEditCarDialog();
            addOrEditCarDialog.ShowDialog();

            if (addOrEditCarDialog.DialogResult is true)
            {
                SelectedCar = addOrEditCarDialog.CurrentCar;

                _apiClient.PostAsync(SelectedCar, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CarsObservableCollection.Add(SelectedCar);
                        });
                    });
            }
        }

        private void EditCar()
        {
            AddOrEditCarDialog addOrEditCarDialog = new AddOrEditCarDialog(SelectedCar);
            addOrEditCarDialog.ShowDialog();

            if (addOrEditCarDialog.DialogResult is true)
            {
                Cars editedCar = new Cars();
                editedCar = addOrEditCarDialog.CurrentCar;

                _apiClient.DeleteAsync(SelectedCar.CarID, _endpoint)
                   .ContinueWith(task =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                       {
                           CarsObservableCollection.Remove(SelectedCar);
                       });
                   });

                _apiClient.PostAsync(editedCar, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CarsObservableCollection.Add(editedCar);
                        });
                    });
            }
        }

        private void DeleteCar()
        {
            Cars deletedCar = SelectedCar;

            CarsObservableCollection = Task.Run(async () => await _apiClient.GetAsync<ObservableCollection<Cars>>(_endpoint)).Result; //Refreshing the collection so that the newly given element also has the correct ID

            foreach (var car in CarsObservableCollection)
            {
                if (car.LicensePlate == deletedCar.LicensePlate && car.CarBrand == deletedCar.CarBrand && car.CarModell == deletedCar.CarModell)
                {
                    SelectedCarIndex = car.CarID;
                    SelectedCar = car;
                }
            }

            _apiClient.DeleteAsync(SelectedCarIndex, _endpoint)
                .ContinueWith(task =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CarsObservableCollection.Remove(SelectedCar);
                    });
                });
        }

        private void CustomerOfCar()
        {
            List<Customers> customerOfCarResult = new List<Customers>();
            string customerOfCarEndpoint = "http://localhost:3851/CustomerOfCar";

            if (SelectedCar is null)
            {
                MessageBox.Show("Please select a Car in order to determine it's Customer.");
                return;
            }

            if (SelectedCar.Contract is null || SelectedCar.Contract.CustomerID is 0)
            {
                MessageBox.Show("The following Car does not have a Customer yet.");
                return;
            }
            customerOfCarResult = Task.Run(async () => await _apiClient.GetAsyncWithID<List<Customers>>(SelectedCar.CarID, customerOfCarEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

            MessageBox.Show($"The Customer of {SelectedCar.CarBrand} {SelectedCar.CarModell} is {customerOfCarResult.FirstOrDefault().FirstName} {customerOfCarResult.FirstOrDefault().LastName}.");
        }
    }
}
