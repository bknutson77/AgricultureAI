// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

/* Current User */
class CurrentUser {
    constructor(name, username) {
        this.username = username;
    }
}
var currentUser = new CurrentUser("");

/* Login/Register/Continue-As-Guest Functions */
function loginRedirect() {
    document.location = "Login";
}

function registerRedirect() {
    document.location = "Register";
}

function continueAsGuestRedirect() {

    // Set user information:
    currentUser.username = "Guest";

    // Advance and confirm:
    document.location = "ModuleHome";
    alert("You are continuing as guest.");
}

function returnToLoginRegisterHome() {
    document.location = "Index";
}

function submitLogin() {
    var requestString = "&username=" + $('#loginUsername').val() + "&password=" + $('#loginPassword').val();
    $.ajax({
        url: "Login/?handler=AttemptLogin" + requestString,
        type: 'GET',
        success: function (result) {
            console.log(result);
            if (result == "Success") {

                // Set user information:
                currentUser.username = $('#loginUsername').val();

                // Advance and confirm:
                document.location = "ModuleHome";
                alert("Success! You have been logged in as: " + currentUser.username);
            } else if (result == "Incorrect Username/Password") {
                document.getElementById("loginErrorMessage").innerHTML = result;
            } else {
                alert(result);
            }
        },
        error: function (error) {
            alert("Request To Authenticate Through Server Failed");
            console.log(error);
        }
    });
}

function submitRegister() {
    var requestString =
        "&name=" + $('#registerName').val() +
        "&email=" + $('#registerEmail').val() +
        "&occupation=" + $('#registerOccupation').val() +
        "&plantExpert=" + document.getElementById("registerPlantExpert").checked +
        "&username=" + $('#registerUsername').val() +
        "&password=" + $('#registerPassword').val();
    $.ajax({
        url: "Login/?handler=AttemptRegister" + requestString,
        type: 'GET',
        success: function (result) {
            console.log(result);
            if (result == "Success") {
                // Set user information:
                currentUser.username = $('#registerUsername').val();

                // Advance and confirm:
                document.location = "ModuleHome";
                alert("Success! You have been registered and logged in as: " + currentUser.username);
            } else if (result == "Username Unavailable") {
                document.getElementById("registerErrorMessage").innerHTML = result;
            } else {
                alert(result);
            }
        },
        error: function (error) {
            alert("Request To Authenticate Through Server Failed");
            console.log(error);
        }
    });
}

//Module Home 

function module1Redirect() {
    document.location = "Module_1";
}

//Redirects from Machne Learning / AI to Module Home

function returnToModuleHome() {
    document.location = "ModuleHome";
}