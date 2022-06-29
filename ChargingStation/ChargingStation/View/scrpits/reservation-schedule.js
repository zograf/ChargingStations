const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const spotId = urlParams.get('spotId')
var timeSlotsUri = "https://localhost:7265/api/Reservation/reserved-slots=" + spotId;
var containerHeight = 1200;
document.addEventListener('DOMContentLoaded', function() {
    printFixedTimeSlots();
    let title = document.getElementById("spot");
    title.innerText = "Charging spot with id " + spotId + " schedule: ";
    let request = new XMLHttpRequest();
    request.open('GET', timeSlotsUri);
    request.onreadystatechange = function() {
        if (this.readyState === 4) {
            if (this.status === 200) {
                slots = JSON.parse(request.responseText);
                printSlots(slots);
            } else {
                alert("Error during card loading");
            }
        }
    }
    request.send(parseFloat(spotId));

}, false);

var diffHours = (a, b) => {
    return (a.getTime() - b.getTime()) / 1000 / 60 / 60;
}

function printFixedTimeSlots() {
    let timeContainer = document.getElementById("time");
    let now = new Date();
    let addHours = (a, hours) => {
        a.setTime(a.getTime() + hours * 1000 * 60 * 60);
    }
    timeContainer.style.setProperty("height", containerHeight + "px");
    let start = new Date();
    start.setTime(now.getTime());

    let height = containerHeight / 12;
    for (let i = now.getHours() + 1; i < 13 + now.getHours(); ++i) {
        addHours(start, 1);

        let startNowDiff = diffHours(start, now);
        //console.log(startNowDiff);

        let timeSlot = document.createElement("div");
        timeSlot.setAttribute("class", "time-slot");
        timeSlot.innerText = i % 24 + ":00";

        //proportion
        let startNowOffset = (startNowDiff * containerHeight) / 12;

        timeSlot.style.setProperty("margin-top", (startNowOffset - 15) + "px");
        timeSlot.style.setProperty("height", height + "px");
        timeContainer.appendChild(timeSlot);
    }
}


function printSlots(slots) {
    let timeSlotsContainer = document.getElementById("time-slots");
    let now = new Date();
    timeSlotsContainer.style.setProperty("height", containerHeight + "px");
    timeSlotsContainer.setAttribute("class", "time-slot-container");
    for (let i = 0; i < slots.length; ++i) {
        let start = new Date(slots[i]["item1"]);
        let end = new Date(slots[i]["item2"]);

        let startNowDiff = diffHours(start, now);
        let startEndDiff = diffHours(end, now);

        let timeSlot = document.createElement("div");
        timeSlot.setAttribute("class", "time-slot");
        timeSlot.style.setProperty("background-color", "red");

        //proportion
        let startNowOffset = (startNowDiff * containerHeight) / 12;
        let startEndOffset = (startEndDiff * containerHeight) / 12;

        timeSlot.style.setProperty("margin-top", startNowOffset + "px");
        timeSlot.style.setProperty("height", (startEndOffset - startNowOffset) + "px");
        timeSlotsContainer.appendChild(timeSlot);


        console.log(start);
        console.log(end)
        console.log(startNowDiff);
        console.log(startEndDiff);
    }
}