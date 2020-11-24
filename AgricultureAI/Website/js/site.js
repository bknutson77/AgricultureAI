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


/* Module Home Functions */
function learnAboutAIRedirect() {
    document.location = "LearnAboutAI";
}

function playAGameRedirect() {
    document.location = "GameRules";
}

function returnToModuleHome() {
    document.location = "ModuleHome";
}


/* AI/Machine Learning Functions */
function generateURL(imageIndex) { // takes as input a string denoting a file and returns a URL for an image
    return "https://firebasestorage.googleapis.com/v0/b/agricultureai-15ce0.appspot.com/o/" + imageIndex + "?alt = media"
}

function getImageKeys() {
    $.ajax({
        url: "Login/?handler=ImageKeys",
        type: 'GET',
        success: function (result) {
            console.log(result);
        },
        error: function (error) {
            alert("Request To Acquire Image Keys Through Server Failed");
            console.log(error);
        }
    });
}

function getMLPrediction(imageURL) {
    var requestString = "&imageURL=" + imageURL;
    $.ajax({
        url: "Login/?handler=AIPrediction" + requestString,
        type: 'GET',
        success: function (result) {
            console.log(result);
        },
        error: function (error) {
            alert("Request To Acquire Machine Learning Prediction Through Server Failed");
            console.log(error);
        }
    });
}

function getGroundTruth(imageURL) {
    var requestString = "&imageURL=" + imageURL;
    $.ajax({
        url: "Login/?handler=GroundTruth" + requestString,
        type: 'GET',
        success: function (result) {
            console.log(result);
        },
        error: function (error) {
            alert("Request To Acquire Ground Truth Through Server Failed");
            console.log(error);
        }
    });
}


/* Play a Game Module */

// -- Variables:
var voteFlag = true;

// -- Functions:
function continueToGameRedirect() {
    document.location = "GamePlay";
}

function continueToGameResults() {
    document.location = "GameResults";
}

function generatRandomNumber(maxNumber) {
    return Math.floor(Math.random() * Math.floor(maxNumber));
}

function castVoteOrContinue() {
    voteFlag = !voteFlag;
    if (voteFlag) {
        document.getElementById("castVoteDiv").style.display = "contents";
        document.getElementById("votesAreInDiv").style.display = "none";
    } else {
        document.getElementById("castVoteDiv").style.display = "none";
        document.getElementById("votesAreInDiv").style.display = "contents";
    }
}