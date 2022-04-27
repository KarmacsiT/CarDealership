using CarDealershipDB.WpfClient.ViewModels;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarDealershipDB.WpfClient
{
    /// <summary>
    /// Interaction logic for AddOrEditContractDialog.xaml
    /// </summary>
    public partial class AddOrEditContractDialog : Window
    {
        ManageContractsViewModel manageContractViewModel = new ManageContractsViewModel();
        ManageCarsViewViewModel manageCarsViewModel = new ManageCarsViewViewModel();
        ManageCustomersViewModel manageCustomersViewModel = new ManageCustomersViewModel();

        public AddOrEditContractDialog()
        {
            InitializeComponent();
        }

        public AddOrEditContractDialog(Contracts defaultContract)
        {
            InitializeComponent();
            manageContractViewModel.Setup(defaultContract);

            if (defaultContract != null)
            {
                TypeComboBox.Text = defaultContract.ContractType;
                SigningDateTextbox.Text = defaultContract.ContractDate.ToString();
                ExpiryDateTextBox.Text = defaultContract.ContractExpiryDate.ToString();
                CarIDTextBox.Text = defaultContract.CarID.ToString();
                CustomerIDTextBox.Text = defaultContract.CustomerID.ToString();
            }
        }
        private Contracts _currentContract = new Contracts();

        public Contracts CurrentContract
        {
            get { return _currentContract; }
            set { _currentContract = value; }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool hasMatchingCarID = false;
            bool hasMatchingCustomerID = false;
            bool duplicateContract = true;
            bool duplicateCustomer = true;
            ObservableCollection<Cars> crossRefernceCarID = manageCarsViewModel.CarsObservableCollection;

            DateTime checkContractDate = new DateTime();
            DateTime checkContractExpiryDate = new DateTime();
            CurrentContract.ContractType = TypeComboBox.Text;
            CurrentContract.CarID = int.Parse(CarIDTextBox.Text);
            CurrentContract.CustomerID = int.Parse(CustomerIDTextBox.Text);

            bool ConrtractDateIsValid = DateTime.TryParse(SigningDateTextbox.Text, out checkContractDate);
            bool ContractExpiryDateIsValid = DateTime.TryParse(ExpiryDateTextBox.Text, out checkContractExpiryDate);

            foreach (var car in crossRefernceCarID)
            {
                if (car.CarID == CurrentContract.CarID)
                {
                    hasMatchingCarID = true;

                    if (car.Contract is null)
                    {
                        duplicateContract = false;
                    }
                }
            }

            foreach (var customer in manageCustomersViewModel.CustomersObservableCollection)
            {
                if (customer.CustomerID == CurrentContract.CustomerID)
                {
                    hasMatchingCustomerID = true;

                    if (customer.ContractId is 0)
                    {
                        duplicateCustomer = false;
                    }
                }
            }

            if (hasMatchingCarID && !duplicateContract)
            {
                if (hasMatchingCustomerID && !duplicateCustomer)
                {
                    if (ConrtractDateIsValid)
                    {
                        CurrentContract.ContractDate = checkContractDate;
                    }
                    else
                    {
                        MessageBox.Show("The inputed Contract Date was invalid.");
                        return;
                    }

                    if (!ContractExpiryDateIsValid)
                    {
                        CurrentContract.ContractExpiryDate = null;
                    }
                    else
                    {
                        CurrentContract.ContractExpiryDate = checkContractExpiryDate;
                    }

                    if (CurrentContract.ContractType is "Sell" && CurrentContract.ContractExpiryDate is not null)
                    {
                        MessageBox.Show("A selling contract does not have an expiry Date.");
                        return;
                    }


                    if (CurrentContract.ContractType is "Sell" && CurrentContract.ContractExpiryDate is null)
                    {
                        this.Close();
                        this.DialogResult = true;
                        return;
                    }

                    if (CurrentContract.ContractType is "Lease" && CurrentContract.ContractExpiryDate is null)
                    {
                        MessageBox.Show("A leasing contract must have an Expiry Date.");
                        return;
                    }

                    if (CurrentContract.ContractType is "Lease" && CurrentContract.ContractExpiryDate is not null)
                    {
                        this.Close();
                        this.DialogResult = true;
                        return;
                    }
                }

            }

            if (!hasMatchingCarID)
            {
                MessageBox.Show("There exist no Car with the following CarID, so you can't bind a Contract to it.");
                return;
            }

            if (duplicateContract)
            {
                MessageBox.Show("The following Car already has a Contract, you can't add another one to it.");
                return;
            }

            if (!hasMatchingCustomerID)
            {
                MessageBox.Show("There exist no Customer with the following CustomerID, so you can't bind a Contract to it.");
                return;
            }

            if (duplicateCustomer)
            {
                MessageBox.Show("The following Customer already has a Contract, you can't add another one to it.");
                return;
            }
        }
    }
}
