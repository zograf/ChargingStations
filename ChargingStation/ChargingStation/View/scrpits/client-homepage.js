let clientUri = "https://localhost:7265/api/Client/userId=";

let userId = sessionStorage.getItem("userId");

let request = new XMLHttpRequest();
request.open('GET', clientUri + userId);
request.onreadystatechange = function () {
    if (this.readyState === 4) {
        if (this.status === 200) {
            let user = JSON.parse(request.responseText);
            fillLabels(user);
        } else {
            alert("Greska prilikom ucitavanja korisnika.");
        }
    }
}
request.send();

function fillLabels(user) {
    document.getElementById("name").innerHTML = "<strong style='color:#00d6b7'>Name: </strong>" + user["name"]
    document.getElementById("surname").innerHTML = "<strong style='color:#00d6b7'>Surname: </strong>" + user["surname"]
    document.getElementById("uin").innerHTML = "<strong style='color:#00d6b7'>UIN: </strong>" + user["userIdentificationNumber"]
    document.getElementById("balance").innerHTML = "<strong style='color:#00d6b7'>Balance: </strong>" + user["balance"] + " RSD"
}
