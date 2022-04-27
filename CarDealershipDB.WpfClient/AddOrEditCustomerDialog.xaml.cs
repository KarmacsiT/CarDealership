using CarDealershipDB.WpfClient.ViewModels;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddOrEditCustomerDialog.xaml
    /// </summary>
    public partial class AddOrEditCustomerDialog : Window
    {
        ManageContractsViewModel manageContractsViewModel = new ManageContractsViewModel();

        public AddOrEditCustomerDialog()
        {
            InitializeComponent();
        }

        public AddOrEditCustomerDialog(Customers defaultCustomer)
        {
            InitializeComponent();
            ManageCustomersViewModel manageCustomersViewModel = new ManageCustomersViewModel();
            manageCustomersViewModel.Setup(defaultCustomer);

            if (defaultCustomer != null)
            {
                FirstNameTextbox.Text = defaultCustomer.FirstName;
                LastNameTextBox.Text = defaultCustomer.LastName;
                EmailTextBox.Text = defaultCustomer.Email;
                PhoneNumberTextBox.Text = defaultCustomer.PhoneNumber.ToString();
                ContractIDTextBox.Text = defaultCustomer.ContractId.ToString();
            }
        }

        private Customers _currentCustomer = new Customers();
        public Customers CurrentCustomer { get { return _currentCustomer; } set { _currentCustomer = value; } }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool hasMatchingContractID = false;
            bool duplicateCustomer = true;
            foreach (var contract in manageContractsViewModel.ContractsObservableCollection)
            {
                if (contract.CustomerID == CurrentCustomer.ContractId)
                {
                    hasMatchingContractID = true;

                    if (contract.CustomerID is 0)
                    {
                        duplicateCustomer = false;
                    }
                }
            }

            if (hasMatchingContractID && !duplicateCustomer)
            {
                CurrentCustomer.FirstName = FirstNameTextbox.Text;
                CurrentCustomer.LastName = LastNameTextBox.Text;
                CurrentCustomer.Email = EmailTextBox.Text;
                CurrentCustomer.PhoneNumber = long.Parse(PhoneNumberTextBox.Text);
                CurrentCustomer.ContractId = int.Parse(ContractIDTextBox.Text);

                this.DialogResult = true;
                this.Close();
                return;
            }

            if (!hasMatchingContractID)
            {
                MessageBox.Show("There exist no Contract with the following ContractID, so you can't bind a Customer to it.");
                return;
            }

            if (duplicateCustomer)
            {
                MessageBox.Show("The following Contract already has a Customer, you can't add another one to it.");
                return;
            }
        }

    }
}
