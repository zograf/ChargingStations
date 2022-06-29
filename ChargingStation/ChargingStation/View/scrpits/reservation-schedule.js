const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const spotId = urlParams.get('spotId')
var timeSlotsUri = "https://localhost:7265/api/Reservation/reserved-slots=" + spotId;

document.addEventListener('DOMContentLoaded', function() {
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

function printSlots(slots) {
    let timeSlotsContainer = document.getElementById("time-slots");
    let now = Date();
    let containerHeight = 1200;
    timeSlotsContainer.style.setProperty("height", containerHeight + "px");
    timeSlotsContainer.style.setProperty("position", "relative");
    for (let i = 0; i < slots.length; ++i) {
        let start = new Date(slots[i]["item1"]);
        let end = new Date(slots[i]["item2"]);
        let startNowDiff = (start.getTime() - now);
        let startEndDiff = (end.getTime() - start.getTime())

        let timeSlot = document.createElement("div");
        timeSlot.style.setProperty("background-color", "red");
        timeSlot.style.setProperty("position", "absolute");
        timeSlot.style.setProperty("width", "50px");

        let startNowOffset = (startNowDiff * containerHeight) / 12;
        let startEndOffset = (startEndDiff * containerHeight) / 12;
        timeSlot.style.setProperty("margin-top", startNowOffset + "px");
        timeSlot.style.setProperty("height", startEndOffset + "px");
        timeSlotsContainer.appendChild(timeSlot);


        console.log(start);
        console.log(end)
        console.log(startNowDiff / 1000 / 60 / 60);
        console.log(startEndDiff / 1000 / 60 / 60);
    }
}