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

let contracts = [];

getContractdata();

async function getContractdata() {
    await fetch('http://localhost:3851/contracts')
        .then(x => x.json())
        .then(y => {
            contracts = y;
            console.log(contracts);
            displayContracts();
        });
}

function displayContracts() {
    document.getElementById('resultareaContracts').innerHTML = "";
    contracts.forEach(c => {
        document.getElementById('resultareaContracts').innerHTML +=
            "<tr><td>" + c.contractID + "</td>" +
            "<td>" + c.contractType + "</td>" +
            "<td>" + c.contractDate + "</td>" +
            "<td>" + c.contractExpiryDate + "</td>" +
            "<td>" + c.customerID + "</td>" +
            "<td>" + c.carID + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${c.contractID})">Delete`
            + `<button type="button" onclick="update(${c.contractID})">Edit` + "</td>" +
            "</tr>";
    });
}

function reset() {
    document.getElementById("typeInput").value = null;
    document.getElementById("signingDateInput").value = null;
    document.getElementById("expiryDateInput").value = null;
    document.getElementById("customerIDInput").value = null;
    document.getElementById("carIDInput").value = null;
}

function create() {
    let type = document.getElementById("typeInput").value;
    let sdate = document.getElementById("signingDateInput").value;
    let edate = document.getElementById("expiryDateInput").value;
    let customerid = document.getElementById("customerIDInput").value;
    let carid = document.getElementById("carIDInput").value;


    if (edate == '') {
        edate = null;
    }
    if (type == 'Sell' && edate != null) {
        alert('Selling Contracts don\'t have Contract Expiry Date!');
        return;
    }
    if (contracts.some(c => c.customerID) == document.getElementById("customerIDInput")) {
        alert('The given Customer already has a contract.');
        return;
    }

    if (contracts.some(c => c.carID) == document.getElementById("customerIDInput")) {
        alert('The given car already has a contract.');
        return;
    }

    if (cars.every(c => c.carID != document.getElementById("customerIDInput").value)) {
        alert('There exist no Customer with the given Customer ID.');
        return;
    }

    if (customers.every(c => c.customerID != document.getElementById("carIDInput").value)) {
        alert('There exist no Car with the given Car ID.');
        return;
    }
    else {
        fetch('http://localhost:3851/contracts', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    contractType: type,
                    contractDate: sdate,
                    contractExpiryDate: edate,
                    customerID: customerid,
                    carID: carid
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getContractdata();
            })
            .catch((error) => {
                console.error('Error:', error);
            });

        reset();
    }
}

function remove(id) {

    fetch('http://localhost:3851/contracts/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getContractdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function update(id) {
    if (document.getElementById("typeInput").value == '') {
        let modifiedContract = contracts.find(x => x.contractID == id);

        remove(id);

        document.getElementById("typeInput").value = modifiedContract.contractType;
        document.getElementById("signingDateInput").value = modifiedContract.contractDate;
        document.getElementById("expiryDateInput").value = modifiedContract.contractExpiryDate;
        document.getElementById("customerIDInput").value = modifiedContract.customerID;
        document.getElementById("carIDInput").value = modifiedContract.carID;


        alert('Modify the selected contract by changing the values in the text boxes and press Add Contract to modify the selected contract.')
    }

    else {
        alert('Please end your previous Adding or Editing process before editing another contract or press the Reset Fields button.')
    }
}