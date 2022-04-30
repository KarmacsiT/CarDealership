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

let departments = [];

getDepartmentdata();

async function getDepartmentdata() {
    await fetch('http://localhost:3851/departments')
        .then(x => x.json())
        .then(y => {
            departments = y;
            console.log(departments);
            displayDepartments();
        });
}

function displayDepartments() {
    document.getElementById('resultareaDepartments').innerHTML = "";
    departments.forEach(d => {
        document.getElementById('resultareaDepartments').innerHTML +=
            "<tr><td>" + d.departmentID + "</td>" +
            "<td>" + d.departmentName + "</td>" +
            "<td>" + d.address + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${d.departmentID})">Delete`
            + `<button type="button" onclick="update(${d.departmentID})">Edit` + "</td>" +
            "</tr>";
    });
}

function reset() {
    document.getElementById("deparmentNameInput").value = null;
    document.getElementById("addressInput").value = null;
}

function create() {
    let departmentname = document.getElementById("deparmentNameInput").value;
    let address = document.getElementById("addressInput").value;

    fetch('http://localhost:3851/departments', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                departmentName: departmentname,
                address: address,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getDepartmentdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    reset();
}

function remove(id) {

    fetch('http://localhost:3851/departments/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getDepartmentdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function update(id) {
    if (document.getElementById("deparmentNameInput").value == '') {
        let modifiedDepartment = departments.find(x => x.departmentID == id);

        remove(id);

        document.getElementById("deparmentNameInput").value = modifiedDepartment.departmentName;
        document.getElementById("addressInput").value = modifiedDepartment.address;


        alert('Modify the selected department by changing the values in the text boxes and press Add Department to modify the selected department.')
    }

    else {
        alert('Please end your previous Adding or Editing process before editing another contract or press the Reset Fields button.')
    }
}