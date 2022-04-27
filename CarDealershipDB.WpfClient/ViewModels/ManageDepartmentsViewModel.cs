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
    class ManageDepartmentsViewModel : ObservableRecipient
    {
        private string _endpoint = "http://localhost:3851/departments";

        private ApiClient _apiClient = new ApiClient();

        private ObservableCollection<Departments> _departmentsObservableCollection;
        public ObservableCollection<Departments> DepartmentsObservableCollection
        {
            get => _departmentsObservableCollection;
            set => SetProperty(ref _departmentsObservableCollection, value);
        }

        private Departments _selectedDepartment;

        public Departments SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                SetProperty(ref _selectedDepartment, value);
            }
        }

        private int _selectedDepartmentIndex;

        public int SelectedDepartmentIndex
        {
            get => _selectedDepartmentIndex;
            set => SetProperty(ref _selectedDepartmentIndex, value);
        }

        public void Setup(Departments department)
        {
            SelectedDepartment = department;
        }

        public RelayCommand AddDepartmentCommand { get; set; }
        public RelayCommand DeleteDepartmentCommand { get; set; }
        public RelayCommand EditDepartmentCommand { get; set; }
        public RelayCommand CarsOnThisDepartmentCommand { get; set; }

        public ManageDepartmentsViewModel()
        {
            DepartmentsObservableCollection = new ObservableCollection<Departments>();

            _apiClient.GetAsync<List<Departments>>(_endpoint)
                .ContinueWith((departmentTask) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DepartmentsObservableCollection = new ObservableCollection<Departments>(departmentTask.Result);
                    });
                });

            AddDepartmentCommand = new RelayCommand(AddDepartment);
            EditDepartmentCommand = new RelayCommand(EditDepartment);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
            CarsOnThisDepartmentCommand = new RelayCommand(CarsOnThisDepartment);
        }

        private void AddDepartment()
        {
            AddOrEditDepartmentDialog addOrEditDepartmentDialog = new AddOrEditDepartmentDialog();
            addOrEditDepartmentDialog.ShowDialog();

            SelectedDepartment = addOrEditDepartmentDialog.CurrentDepartment;

            if (addOrEditDepartmentDialog.DialogResult is true)
            {
                _apiClient.PostAsync(SelectedDepartment, _endpoint)
                .ContinueWith(task =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DepartmentsObservableCollection.Add(SelectedDepartment);
                    });
                });
            }
        }

        private void EditDepartment()
        {
            AddOrEditDepartmentDialog addOrEditDepartmentDialog = new AddOrEditDepartmentDialog(SelectedDepartment);
            addOrEditDepartmentDialog.ShowDialog();

            if (addOrEditDepartmentDialog.DialogResult is true)
            {
                Departments editedDepartment = new Departments();
                editedDepartment = addOrEditDepartmentDialog.CurrentDepartment;

                _apiClient.DeleteAsync(SelectedDepartment.DepartmentID, _endpoint)
                   .ContinueWith(task =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                       {
                           DepartmentsObservableCollection.Remove(SelectedDepartment);
                       });
                   });

                _apiClient.PostAsync(editedDepartment, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            DepartmentsObservableCollection.Add(editedDepartment);
                        });
                    });
            }

        }

        private void DeleteDepartment()
        {
            Departments deletedDepartment = SelectedDepartment;

            DepartmentsObservableCollection = Task.Run(async () => await _apiClient.GetAsync<ObservableCollection<Departments>>(_endpoint)).Result; //Refreshing the collection so that the newly given element also has the correct ID

            foreach (var department in DepartmentsObservableCollection)
            {
                if (department.DepartmentName == deletedDepartment.DepartmentName && department.Address == deletedDepartment.Address)
                {
                    SelectedDepartmentIndex = department.DepartmentID;
                    SelectedDepartment = department;
                }
            }
            _apiClient.DeleteAsync(SelectedDepartmentIndex, _endpoint)
                .ContinueWith(task =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DepartmentsObservableCollection.Remove(SelectedDepartment);
                    });
                });
        }

        private void CarsOnThisDepartment()
        {
            List<Cars> carsOnThisDepartment = new List<Cars>();
            string carsOnThisDepartmentEndpoint = "http://localhost:3851/departments_extra";
            bool deparmentHasNoCars = true;

            if (SelectedDepartment is not null)
            {
                carsOnThisDepartment = Task.Run(async () => await _apiClient.GetAsyncWithID<List<Cars>>(SelectedDepartment.DepartmentID, carsOnThisDepartmentEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

                string message = $"Cars on {SelectedDepartment.DepartmentName}:";

                foreach (var car in carsOnThisDepartment)
                {
                    if (car.DepartmentID == SelectedDepartment.DepartmentID)
                    {
                        deparmentHasNoCars = false;
                    }

                    if (!deparmentHasNoCars)
                    {
                        message += Environment.NewLine;
                        message += $"- {car.CarBrand} {car.CarModell}";
                    }
                }

                if (!deparmentHasNoCars)
                {
                    MessageBox.Show(message);
                    return;
                }

                else
                {
                    MessageBox.Show("The following department has no cars on it.");
                }
            }

            else
            {
                MessageBox.Show("Please select a Department in order to determine the Cars on it.");
                return;
            }
        }
    }
}
