function openDivision(evt, divisionName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(divisionName).style.display = "block";
    evt.currentTarget.className += " active";
}

let cars = [];

getCardata();

async function getCardata() {
    await fetch('http://localhost:3851/cars')
        .then(x => x.json())
        .then(y => {
            cars = y;
            console.log(cars);
            displayCars();
        });
}


function displayCars() {
    document.getElementById('resultareaCars').innerHTML = "";
    cars.forEach(c => {
        document.getElementById('resultareaCars').innerHTML +=
            "<tr><td>" + c.carID + "</td>" +
            "<td>" + c.carBrand + "</td>" +
            "<td>" + c.carModell + "</td>" +
            "<td>" + c.licensePlate + "</td>" +
            "<td>" + c.warranty + "</td>" +
            "<td>" + c.engineDisplacement + "</td>" +
            "<td>" + c.fuelType + "</td>" +
            "<td>" + c.horsePower + "</td>" +
            "<td>" + c.transmission + "</td>" +
            "<td>" + c.mileage + "</td>" +
            "<td>" + c.motUntil + "</td>" +
            "<td>" + c.leasePrice + "</td>" +
            "<td>" + c.sellingPrice + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${c.carID})">Delete`
            + `<button type="button" onclick="update(${c.carID})">Edit` + "</td>" +
            "</tr>";
    });
}

function reset() {
    document.getElementById("brandInput").value = null;
    document.getElementById("modelInput").value = null;
    document.getElementById("licensePlateInput").value = null;
    document.getElementById("warrantyInput").value = null;
    document.getElementById("engineDisplacementInput").value = null;
    document.getElementById("fuelInput").value = null;
    document.getElementById("horsepowerInput").value = null;
    document.getElementById("transmissionInput").value = null;
    document.getElementById("mileageInput").value = null
    document.getElementById("motUntilInput").value = null;
    document.getElementById("leasePriceInput").value = null;
    document.getElementById("sellingPriceInput").value = null;
}

function create() {

    let brand = document.getElementById("brandInput").value;
    let model = document.getElementById("modelInput").value;
    let licenseplate = document.getElementById("licensePlateInput").value;
    let warranty = document.getElementById("warrantyInput").value;
    let enginedisplacement = document.getElementById("engineDisplacementInput").value;
    let fuel = document.getElementById("fuelInput").value;
    let horsepower = document.getElementById("horsepowerInput").value;
    let transmission = document.getElementById("transmissionInput").value;
    let mileage = document.getElementById("mileageInput").value;
    let motuntil = document.getElementById("motUntilInput").value;
    let leaseprice = document.getElementById("leasePriceInput").value;
    let sellingprice = document.getElementById("sellingPriceInput").value;

    fetch('http://localhost:3851/cars', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                carBrand: brand,
                carModell: model,
                licensePlate: licenseplate,
                warranty: warranty,
                engineDisplacement: enginedisplacement,
                fuelType: fuel,
                horsePower: horsepower,
                transmission: transmission,
                mileage: mileage,
                motUntil: motuntil,
                leasePrice: leaseprice,
                sellingPrice: sellingprice
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCardata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    reset();
}

function remove(id) {

    fetch('http://localhost:3851/cars/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCardata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function update(id) {
    if (document.getElementById("brandInput").value == '') {
        let modifiedCar = cars.find(x => x.carID == id);

        remove(id);

        document.getElementById("brandInput").value = modifiedCar.carBrand;
        document.getElementById("modelInput").value = modifiedCar.carModell;
        document.getElementById("licensePlateInput").value = modifiedCar.licensePlate;
        document.getElementById("warrantyInput").value = modifiedCar.warranty;
        document.getElementById("engineDisplacementInput").value = modifiedCar.engineDisplacement;
        document.getElementById("fuelInput").value = modifiedCar.fuelType;
        document.getElementById("horsepowerInput").value = modifiedCar.horsePower;
        document.getElementById("transmissionInput").value = modifiedCar.transmission;
        document.getElementById("mileageInput").value = modifiedCar.mileage;
        document.getElementById("motUntilInput").value = modifiedCar.motUntil;
        document.getElementById("leasePriceInput").value = modifiedCar.leasePrice;
        document.getElementById("sellingPriceInput").value = modifiedCar.sellingPrice;

        alert('Modify the selected car by changing the values in the text boxes and press Add Car to modify the selected car.')
    }

    else {
        alert('Please end your previous Adding or Editing process before editing another car or press the Reset Fields button.')
    }
}