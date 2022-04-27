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
    /// Interaction logic for AddOrEditDepartmentDialog.xaml
    /// </summary>
    public partial class AddOrEditDepartmentDialog : Window
    {
        public AddOrEditDepartmentDialog()
        {
            InitializeComponent();
        }

        public AddOrEditDepartmentDialog(Departments defaultDepartment)
        {
            InitializeComponent();
            ManageDepartmentsViewModel manageDepartmentsViewModel = new ManageDepartmentsViewModel();
            manageDepartmentsViewModel.Setup(defaultDepartment);

            if (defaultDepartment != null)
            {
                DepartmentNameTextbox.Text = defaultDepartment.DepartmentName;
                AddressTextBox.Text = defaultDepartment.Address;
            }
        }

        private Departments _currentDepartment = new Departments();
        public Departments CurrentDepartment { get { return _currentDepartment; } set { _currentDepartment = value; } }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentDepartment.DepartmentName = DepartmentNameTextbox.Text;
            CurrentDepartment.Address = AddressTextBox.Text;

            this.DialogResult = true;
            this.Close();
        }
    }
}
