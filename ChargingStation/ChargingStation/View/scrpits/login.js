let loginUri = "https://localhost:7265/api/Credentials/login"
let loggedCreds;
let loginForm = document.getElementById("login-form");

document.getElementById("submit").addEventListener("click", function () {
    login()
});

function login() {
    let username = document.getElementById("username").value.trim();
    let password = document.getElementById("password").value.trim();
    if (username == "" || password == "") {
        alert("Please fill all input fields.")
    } else {
        processLogin(username, password);
    }

    clearLogin();
}

function clearLogin() {
    document.getElementById("username").value = null;
    document.getElementById("password").value = null;
}

function processLogin(username, password) {
    let dto = {
        username: username,
        password: password
    }
    let request = new XMLHttpRequest();
    request.open('PUT', loginUri);
    request.setRequestHeader('Content-Type', 'application/json');
    request.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                // remember user
                sessionStorage.setItem("loggedCreds", this.responseText)
                loggedCreds = JSON.parse(this.responseText);
                console.log(loggedCreds);
                setUserId();
                redirectUser();
            } else {
                alert(this.responseText);
            }
        }
    }

    request.send(JSON.stringify(dto));
}

function setUserId() {
    userId = loggedCreds.id;
    sessionStorage.setItem("userId", userId);
}

function redirectUser() {
    switch (JSON.parse(sessionStorage.getItem("loggedCreds")).role) {
        case "client":
            window.location.href = "client-homepage.html";
            break;
        case "manager":
            window.location.href = "manager-homepage.html";
            break;
        case "admin":
            window.location.href = "admin-homepage.html";
            break;
    }
}