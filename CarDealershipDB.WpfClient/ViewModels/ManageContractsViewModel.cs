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
    public class ManageContractsViewModel : ObservableRecipient
    {
        private string _endpoint = "http://localhost:3851/contracts";

        private ApiClient _apiClient = new ApiClient();

        private ObservableCollection<Contracts> _contractsObservableCollection;
        public ObservableCollection<Contracts> ContractsObservableCollection
        {
            get => _contractsObservableCollection;
            set => SetProperty(ref _contractsObservableCollection, value);
        }

        private Contracts _selectedContract;

        public Contracts SelectedContract
        {
            get => _selectedContract;
            set
            {
                SetProperty(ref _selectedContract, value);
            }
        }

        private int _selectedContractIndex;

        public int SelectedContractIndex
        {
            get => _selectedContractIndex;
            set => SetProperty(ref _selectedContractIndex, value);
        }

        public void Setup(Contracts contract)
        {
            SelectedContract = contract;
        }

        public RelayCommand AddContractCommand { get; set; }
        public RelayCommand DeleteContractCommand { get; set; }
        public RelayCommand EditContractCommand { get; set; }
        public RelayCommand TotalLeaseExpenditureCommand { get; set; }

        public ManageContractsViewModel()
        {
            ContractsObservableCollection = new ObservableCollection<Contracts>();

            _apiClient.GetAsync<List<Contracts>>(_endpoint)
                .ContinueWith((contractsTask) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ContractsObservableCollection = new ObservableCollection<Contracts>(contractsTask.Result);
                    });
                });

            AddContractCommand = new RelayCommand(AddContract);
            EditContractCommand = new RelayCommand(EditContract);
            DeleteContractCommand = new RelayCommand(DeleteContract);
            TotalLeaseExpenditureCommand = new RelayCommand(TotalLeaseExpenditure);
        }

        private void AddContract()
        {
            AddOrEditContractDialog addOrEditContractDialog = new AddOrEditContractDialog();
            addOrEditContractDialog.ShowDialog();

            if (addOrEditContractDialog.DialogResult is true)
            {
                SelectedContract = addOrEditContractDialog.CurrentContract;

                _apiClient.PostAsync(SelectedContract, _endpoint)
                    .ContinueWith(task =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ContractsObservableCollection.Add(SelectedContract);
                        });
                    });
            }
        }

        private void DeleteContract()
        {
            Contracts deletedContract = SelectedContract;

            ContractsObservableCollection = Task.Run(async () => await _apiClient.GetAsync<ObservableCollection<Contracts>>(_endpoint)).Result; //Refreshing the collection so that the newly given element also has the correct ID

            foreach (var contract in ContractsObservableCollection)
            {
                if (contract.ContractType == deletedContract.ContractType && contract.ContractDate == deletedContract.ContractDate && contract.ContractExpiryDate == deletedContract.ContractExpiryDate)
                {
                    SelectedContractIndex = contract.ContractID;
                    SelectedContract = contract;
                }
            }
            _apiClient.DeleteAsync(SelectedContractIndex, _endpoint)
                .ContinueWith(task =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ContractsObservableCollection.Remove(SelectedContract);
                    });
                });
        }

        private void EditContract()
        {
            AddOrEditContractDialog addOrEditContractDialog = new AddOrEditContractDialog(SelectedContract);
            addOrEditContractDialog.ShowDialog();

            if (addOrEditContractDialog.DialogResult is true)
            {
                Contracts editedContract = new Contracts();
                editedContract = addOrEditContractDialog.CurrentContract;

                DeleteContract();

                _apiClient.PostAsync(editedContract, _endpoint)
                   .ContinueWith(task =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                       {
                           ContractsObservableCollection.Add(editedContract);
                       });
                   });
            }
        }
        private void TotalLeaseExpenditure()
        {
            int totalLeaseExpenditure = 0;

            string totalLeaseExpenditureEndpoint = "http://localhost:3851/contracts_noncrud";

            if (SelectedContract is not null && SelectedContract.ContractType is "Lease" && SelectedContract.CustomerID is not 0)
            {
                Customers selectedCustomer = Task.Run(async () => await _apiClient.GetAsyncWithID<Customers>(SelectedContract.CustomerID, "http://localhost:3851/customers")).Result; //Waits for the GetAsync to complete and returns the result

                totalLeaseExpenditure = Task.Run(async () => await _apiClient.GetAsyncWithID<int>(SelectedContract.CustomerID, totalLeaseExpenditureEndpoint)).Result; //Waits for the GetAsync to complete and returns the result

                MessageBox.Show($"The Total Lease Expediture of {selectedCustomer.FirstName} {selectedCustomer.LastName} is {totalLeaseExpenditure} HUF");
                return;
            }

            if (SelectedContract is null)
            {
                MessageBox.Show("Please select a contract in order to calculate it's total lease expenditure.");
                return;
            }

            if (SelectedContract.ContractType is not "Lease")
            {
                MessageBox.Show("A sell contract does not have a Lease Expenditure, please select a leasing contract");
                return;
            }
        }
    }
}
