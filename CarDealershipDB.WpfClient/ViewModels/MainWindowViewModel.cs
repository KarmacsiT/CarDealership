using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.WpfClient.ViewModels
{
    class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand ManageCarsCommand { get; set; }
        public RelayCommand ManageContractsCommand { get; set; }
        public RelayCommand ManageCustomersCommand { get; set; }
        public RelayCommand ManageDepartmentsCommand { get; set; }
        public MainWindowViewModel()
        {
            ManageCarsCommand = new RelayCommand(ManageCars);
            ManageContractsCommand = new RelayCommand(ManageContracts);
            ManageCustomersCommand = new RelayCommand(ManageCustomers);
            ManageDepartmentsCommand = new RelayCommand(ManageDepartments);
        }

        private void ManageCars()
        {
            new ManageCarsView().Show();
        }
        private void ManageContracts()
        {
            new ManageContractsView().Show();
        }

        private void ManageCustomers()
        {
            new ManageCustomersView().Show();
        }

        private void ManageDepartments()
        {
            new ManageDepartmentsView().Show();
        }
    }
}
