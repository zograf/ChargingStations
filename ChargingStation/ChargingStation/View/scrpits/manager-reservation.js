var spotsUri = "https://localhost:7265/api/ChargingSpot";

document.addEventListener('DOMContentLoaded', function () {
    let request = new XMLHttpRequest();
    request.open('GET', spotsUri);
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
    let spotsContainer = document.getElementById("charging-spots");
    for (let i = 0; i < spots.length; ++i) {
        let spotDiv = document.createElement("div");
        spotDiv.setAttribute("class", "spot");
        var spotLink = document.createElement("a");
        spotLink.textContent = spots[i]["id"]
        spotLink.setAttribute("onclick", "viewSchedule(" + spots[i]["id"] + ")");
        spotDiv.appendChild(spotLink);
        console.log(spots[i]);
        spotsContainer.appendChild(spotDiv);
    }
}

function viewSchedule(spotId) {
    window.location.href = "reservation-schedule.html?spotId=" + spotId;
}