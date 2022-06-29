let clientUri = "https://localhost:7265/api/Client/userId=";
let cardUri = "https://localhost:7265/api/Client/cards/userId=";
let arriveUri = "https://localhost:7265/api/Charging/arrive";

let userId = sessionStorage.getItem("userId");



let request = new XMLHttpRequest();
request.open('GET', clientUri + userId);
request.onreadystatechange = function () {
    if (this.readyState === 4) {
        if (this.status === 200) {
            let user = JSON.parse(request.responseText);
            fillLabels(user);
        } else {
            alert("Client load failed");
        }
    }
}
request.send();



let req = new XMLHttpRequest();
req.open('GET', cardUri + userId);
req.onreadystatechange = function () {
    if (this.readyState === 4) {
        if (this.status === 200) {
            let vehicles = JSON.parse(req.responseText);
            fillSelect(vehicles);
        } else {
            alert("Vehicle load failed");
        }
    }
}
req.send();

function fillLabels(user) {
    document.getElementById("name").innerHTML = "<strong style='color:#00d6b7'>Name: </strong>" + user["name"]
    document.getElementById("surname").innerHTML = "<strong style='color:#00d6b7'>Surname: </strong>" + user["surname"]
    document.getElementById("uin").innerHTML = "<strong style='color:#00d6b7'>UIN: </strong>" + user["userIdentificationNumber"]
    document.getElementById("balance").innerHTML = "<strong style='color:#00d6b7'>Balance: </strong>" + user["balance"] + " RSD"
}

var reservationModal = document.getElementById("reservation-modal");
var reservationSpan = document.getElementById("reservation-close");
var btnArrive = document.getElementsByClassName("btn-arrive")[0]

btnArrive.onclick = function() {
    reservationModal.style.display = "block";
}

reservationSpan.onclick = function() {
    reservationModal.style.display = "none";
}
window.onclick = function(event) {
    if (event.target == reservationModal) {
        reservationModal.style.display = "none";
    }
}

function fillSelect(vehicles) {
    let select = document.getElementById("card-select")
    for (let i = 0; i < vehicles.length; i++) {
        let vehicle = vehicles[i]
        option = document.createElement('option');
        option.setAttribute('value', vehicle["card"]["id"]);
        option.appendChild(document.createTextNode(vehicle["name"]));
        select.appendChild(option); 
    }
}

function reserve() {
    let dto = {
        startTime: document.getElementById("reservation-start").value,
        endTime: document.getElementById("reservation-end").value,
        cardId: document.getElementById("card-select").value
    }
    console.log(dto)

    let req = new XMLHttpRequest();
    req.open('PUT', arriveUri);
    req.setRequestHeader('Content-Type', 'application/json');
    req.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                alert("Arrive successful!")
            } else {
                alert("Arrive failed!");
            }
        }
    }
    req.send(JSON.stringify(dto));
}

