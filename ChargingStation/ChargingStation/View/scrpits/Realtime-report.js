var reportUri = "https://localhost:7265/api/RealtimeReport";
var repairUri = "https://localhost:7265/api/RealtimeReport/repair";
var malfunctionUri = "https://localhost:7265/api/RealtimeReport/malfunction";
var reserveUri = "https://localhost:7265/api/RealtimeReport/reserve";
var arriveUri = "https://localhost:7265/api/RealtimeReport/arrive";

document.addEventListener('DOMContentLoaded', function () {
    let request = new XMLHttpRequest();
    request.open('GET', reportUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                spots = JSON.parse(request.responseText);
                makeSpots(spots);
            } else {
                alert("Error during card loading");
            }
        }
    }
    request.send()
}, false);

function makeSpots(spots) {
    let spotsContainer = document.getElementById("spots");
    for (let key in spots) {
        let spotDiv = document.createElement("div");
        spotDiv.textContent = key;
        if (spots[key] == 0)
            spotDiv.setAttribute("class", "spot-entry vacant");
        else if (spots[key] == 1)
            spotDiv.setAttribute("class", "spot-entry reserved");
        else if (spots[key] == 2)
            spotDiv.setAttribute("class", "spot-entry charging");
        else if (spots[key] == 3)
            spotDiv.setAttribute("class", "spot-entry malfunction");
        console.log(key);
        console.log(spots[key]);
        spotsContainer.appendChild(spotDiv);
    }
}

let reserveButton = document.getElementById("reserve");
reserveButton.onclick = function () {
    let request = new XMLHttpRequest();
    request.open('PUT', reserveUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                console.log("reserve success");
            } else {
                alert("Error during reservation");
            }
        }
    }
    request.send();
}

let malfunctionButton = document.getElementById("malfunction");
malfunctionButton.onclick = function () {
    let request = new XMLHttpRequest();
    request.open('PUT', malfunctionUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                console.log("malfunction success");
            } else {
                alert("Error during malfunction");
            }
        }
    }
    request.send();
}

let arriveButton = document.getElementById("arrive");
arriveButton.onclick = function () {
    let request = new XMLHttpRequest();
    request.open('PUT', arriveUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                console.log("arrive success");
            } else {
                alert("Error during arrival");
            }
        }
    }
    request.send();
}

let repairButton = document.getElementById("repair");
repairButton.onclick = function () {
    let request = new XMLHttpRequest();
    request.open('PUT', repairUri);
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                console.log("repair success");
            } else {
                alert("Error during repair");
            }
        }
    }
    request.send();
}