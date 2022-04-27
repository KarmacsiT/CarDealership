using CarDealershipDB.WpfClient.ViewModels;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddOrEditCarDialog.xaml
    /// </summary>
    public partial class AddOrEditCarDialog : Window
    {
        public AddOrEditCarDialog()
        {
            InitializeComponent();
        }

        public AddOrEditCarDialog(Cars defaultCar)
        {
            InitializeComponent();
            ManageCarsViewViewModel manageCarsViewViewModel = new ManageCarsViewViewModel();
            manageCarsViewViewModel.Setup(defaultCar);

            if (defaultCar != null)
            {
                BrandTextbox.Text = defaultCar.CarBrand;
                ModelTextbox.Text = defaultCar.CarModell;
                LicensePlateTextBox.Text = defaultCar.LicensePlate;
                WarrantyTextBox.Text = defaultCar.Warranty.ToString();
                EngineDisplacementTextBox.Text = defaultCar.EngineDisplacement.ToString();
                HorsePowerTextBox.Text = defaultCar.HorsePower.ToString();
                FuelComboBox.Text = defaultCar.FuelType;
                TransmissionComboBox.Text = defaultCar.Transmission;
                MileageTextbox.Text = defaultCar.Mileage.ToString();
                MOTTextBox.Text = defaultCar.MOTUntil.ToString();
                LeaseTextBox.Text = defaultCar.LeasePrice.ToString();
                SellTextBox.Text = defaultCar.SellingPrice.ToString();
            }
        }

        private Cars _currentCar = new Cars();
        public Cars CurrentCar { get { return _currentCar; } set { _currentCar = value; } }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentCar.CarBrand = BrandTextbox.Text;
            CurrentCar.CarModell = ModelTextbox.Text;
            CurrentCar.LicensePlate = LicensePlateTextBox.Text;
            CurrentCar.Warranty = int.Parse(WarrantyTextBox.Text);
            CurrentCar.EngineDisplacement = double.Parse(EngineDisplacementTextBox.Text, CultureInfo.InvariantCulture);
            CurrentCar.HorsePower = int.Parse(HorsePowerTextBox.Text);
            CurrentCar.FuelType = FuelComboBox.Text;
            CurrentCar.Transmission = TransmissionComboBox.Text;
            CurrentCar.Mileage = int.Parse(MileageTextbox.Text);
            CurrentCar.MOTUntil = DateTime.Parse(MOTTextBox.Text);
            CurrentCar.LeasePrice = int.Parse(LeaseTextBox.Text);
            CurrentCar.SellingPrice = int.Parse(SellTextBox.Text);

            this.DialogResult = true;
            this.Close();
        }

    }
}
