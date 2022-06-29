let cardUri = "https://localhost:7265/api/Client/cards/userId=";
let reservationUri = "https://localhost:7265/api/Reservation/create";
let getReservationsUri = "https://localhost:7265/api/Reservation";
let cancelUri = "https://localhost:7265/api/Reservation/cancel/id=";
let notificatonUri = "https://localhost:7265/api/Notification/userId=";

let id = sessionStorage.getItem("userId");

var vehicles = []
var reservations = []

checkNotifications()

function checkNotifications() {
    let request = new XMLHttpRequest();
    request.open('GET', notificatonUri + id);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                let notifications = JSON.parse(request.responseText);
                if (notifications.length != 0)
                    alert("You have missed your reservation")
            } else {
                alert("Error during notification loading");
            }
        }
    }
    request.send()
    setTimeout(function(){checkNotifications()}, 15000);
}

let request = new XMLHttpRequest();
request.open('GET', cardUri + id);
request.onreadystatechange = function () {
    if (this.readyState === 4) {
        if (this.status === 200) {
            vehicles = JSON.parse(request.responseText);
            fillTable(vehicles)
            res()
        } else {
            alert("Error during card loading");
        }
    }
}
request.send()
function res() {
    request = new XMLHttpRequest();
    request.open('GET', getReservationsUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                reservations = JSON.parse(request.responseText);
                fillReservations(reservations)
            } else {
                alert("Error during reservation loading");
            }
        }
    }
    request.send();
}

function fillReservations(vehicles) {
    let table = document.createElement("table")
    let row = table.insertRow(0);
    row.setAttribute("class", "table-header");
    let cell1 = row.insertCell(0);
    cell1.innerHTML = "CARD ID"
    let cell2 = row.insertCell(1);
    cell2.innerHTML = "VEHICLE NAME"
    let cell3 = row.insertCell(2);
    cell3.innerHTML = "TIME START"
    let cell4 = row.insertCell(3);
    cell4.innerHTML = "TIME END"
    let cell5 = row.insertCell(4);
    cell5.innerHTML = "CANCEL RESERVATION"
    console.log(reservations)
    console.log(vehicles)
    for (let i = 0; i < reservations.length; ++i) {
        let reservation = reservations[i]
        let vehicle = getVehicleByCardId(reservation["cardId"])
        if (vehicle == null) continue

        let row = table.insertRow(i+1);
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);
        let cell4 = row.insertCell(3);
        let cell5 = row.insertCell(4);


        cell1.innerHTML = reservation["cardId"];
        cell2.innerHTML = vehicle["name"];
        cell3.innerHTML = reservation["startTime"].replace("T", " ");
        cell4.innerHTML = reservation["endTime"].replace("T", " ");

        let buttonCancel = document.createElement("button")
        buttonCancel.setAttribute("class", "btn-cancel")
        buttonCancel.setAttribute("id", reservation["id"])
        buttonCancel.onclick = function() {
            id = reservation["id"]
            cancel(id)
        }
        cell5.appendChild(buttonCancel)
    }
    document.getElementById("reservation-table").appendChild(table);

}

function getVehicleByCardId(cardId) {
    for (let i = 0; i < vehicles.length; i++) {
        if (vehicles[i]["card"]["id"] == cardId) 
            return vehicles[i]
    }
    return null;
}

function fillTable(vehicles) {
    let table = document.createElement("table")
    let row = table.insertRow(0);
    row.setAttribute("class", "table-header");
    let cell1 = row.insertCell(0);
    cell1.innerHTML = "CARD ID"
    let cell2 = row.insertCell(1);
    cell2.innerHTML = "VEHICLE NAME"
    let cell3 = row.insertCell(2);
    cell3.innerHTML = "REGISTRATION PLATE"
    let cell4 = row.insertCell(3);
    cell4.innerHTML = "RESERVATION"
    for (let i = 0; i < vehicles.length; ++i) {
        let vehicle = vehicles[i]
        if (vehicle["card"] == null) continue

        let row = table.insertRow(i+1);
        row.setAttribute("id", vehicle["card"]["id"]);
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);
        let cell4 = row.insertCell(3);

        cell1.innerHTML = vehicle["card"]["id"];
        cell2.innerHTML = vehicle["name"];
        cell3.innerHTML = vehicle["registrationPlate"];

        let buttonReserve = document.createElement("button")
        buttonReserve.setAttribute("class", "btn-reserve")
        buttonReserve.setAttribute("id", vehicle["card"]["id"])
        buttonReserve.onclick = function() {
            reservationModal.style.display = "block";
            let b = document.getElementsByClassName("do-reserve")[0]
            b.setAttribute("id", this.id)
        }
        cell4.appendChild(buttonReserve)
    }
    document.getElementById("card-table").appendChild(table);

}

var reservationModal = document.getElementById("reservation-modal");
var reservationSpan = document.getElementById("reservation-close");

reservationSpan.onclick = function() {
  reservationModal.style.display = "none";
}
window.onclick = function(event) {
  if (event.target == reservationModal) {
    reservationModal.style.display = "none";
  }
}

function reserve() {
    dateStart = document.getElementById("reservation-start").value
    dateEnd = document.getElementById("reservation-end").value
    cardId = document.getElementsByClassName("do-reserve")[0].id

    let dto = {
        startTime: dateStart,
        endTime: dateEnd,
        cardId: cardId
    }
    console.log(dto)

    let request = new XMLHttpRequest();
    request.open('POST', reservationUri);
    request.setRequestHeader('Content-Type', 'application/json');
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                alert("Reservation successful!")
                document.location.reload()
            } else {
                alert("Reservation fail!");
            }
        }
    }
    request.send(JSON.stringify(dto));
}

function cancel(id) {
    let request = new XMLHttpRequest();
    request.open('GET', cancelUri + id);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                alert("Cancel successful!")
                document.location.reload()
            } else {
                alert("Cancel fail!");
            }
        }
    }
    request.send();
}

