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

let customers = [];

getCustomersdata()

async function getCustomersdata() {
    await fetch('http://localhost:3851/customers')
        .then(x => x.json())
        .then(y => {
            customers = y;
            console.log(customers);
            displayCustomers();
        });
}

function displayCustomers() {
    document.getElementById('resultareaCustomers').innerHTML = "";
    customers.forEach(c => {
        document.getElementById('resultareaCustomers').innerHTML +=
            "<tr><td>" + c.customerID + "</td>" +
            "<td>" + c.firstName + "</td>" +
            "<td>" + c.lastName + "</td>" +
            "<td>" + c.email + "</td>" +
            "<td>" + c.phoneNumber + "</td>" +
            "<td>" + c.contractId + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${c.customerID})">Delete`
            + `<button type="button" onclick="update(${c.customerID})">Edit` + "</td>" +
            "</tr>";
    });
}

function reset() {
    document.getElementById("firstNameInput").value = null;
    document.getElementById("lastNameInput").value = null;
    document.getElementById("emailInput").value = null;
    document.getElementById("phoneNumberInput").value = null;
    document.getElementById("contractIDInput").value = null;
}

function create() {
    let firstname = document.getElementById("firstNameInput").value;
    let lastname = document.getElementById("lastNameInput").value;
    let email = document.getElementById("emailInput").value;
    let phonenumber = document.getElementById("phoneNumberInput").value;
    let contractid = document.getElementById("contractIDInput").value;

    if (customers.some(c => c.contractID) == document.getElementById("contractIDInput")) {
        alert('The given Contract already already belong to another customer.');
        return;
    }

    if (contracts.every(c => c.contractID != document.getElementById("contractIDInput").value)) {
        alert('There exist no contract with the given Contract ID.');
        return;
    }
    else {
        fetch('http://localhost:3851/customers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    firstName: firstname,
                    lastName: lastname,
                    email: email,
                    phoneNumber: phonenumber,
                    contractId: contractid
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getCustomersdata();
            })
            .catch((error) => {
                console.error('Error:', error);
            });

        reset();
    }
}

function remove(id) {

    fetch('http://localhost:3851/customers/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCustomersdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function update(id) {
    if (document.getElementById("firstNameInput").value == '') {
        let modifiedCustomer = customers.find(x => x.customerID == id);

        remove(id);

        document.getElementById("firstNameInput").value = modifiedCustomer.firstName;
        document.getElementById("lastNameInput").value = modifiedCustomer.lastName;
        document.getElementById("emailInput").value = modifiedCustomer.email;
        document.getElementById("phoneNumberInput").value = modifiedCustomer.phoneNumber;
        document.getElementById("contractIDInput").value = modifiedCustomer.contractId;


        alert('Modify the selected customer by changing the values in the text boxes and press Add Customer to modify the selected customer.')
    }

    else {
        alert('Please end your previous Adding or Editing process before editing another customer or press the Reset Fields button.')
    }
}