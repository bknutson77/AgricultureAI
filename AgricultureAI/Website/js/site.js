// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


/* Functions that need to be called on every reload */

// Updates the plant image so that one is always displayed on opening of the game.
function updatePlantImage() {
    var plantImage = localStorage.getItem("plantImage");
    if (plantImage !== null && plantImage !== undefined) {
        $("#plantImage").attr("src", stitchImageURL(plantImage));
    }
}
updatePlantImage();


/* Login/Register/Continue-As-Guest Functions */
function loginRedirect() {
    document.location = "Login";
}

function registerRedirect() {
    document.location = "Register";
}

function continueAsGuestRedirect() {

    // Set user information:
    localStorage.setItem("currentUser","Guest");

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
                var currentUser = $('#loginUsername').val();
                localStorage.setItem("currentUser", currentUser);

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
                var currentUser = $('#registerUsername').val();
                localStorage.setItem("currentUser", currentUser);

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
function stitchImageURL(imageKey) {

    // Obtain Firebase URL from Server:
    var firebaseURL = localStorage.getItem("firebaseURL");
    if (firebaseURL === null || firebaseURL === undefined) {
        firebaseURL = getFirebaseURL();
        localStorage.setItem("firebaseURL", firebaseURL);
    }

    // Parse and obtain the project key alone:
    var projectKey = firebaseURL.replace("https://", "");
    projectKey = projectKey.replace(".firebaseio.com/", "");

    // Return the image url:
    return "https://firebasestorage.googleapis.com/v0/b/" + projectKey + ".appspot.com/o/images_handheld%2F" + imageKey + "?alt=media";
}

function getFirebaseURL() {
    var response = $.ajax({
        url: "GameRules/?handler=FirebaseURL",
        type: 'GET',
        async: false,
        success: function (result) {
            console.log(result);
            return result;
        },
        error: function (error) {
            alert("Request To Acquire Firebase URL Through Server Failed");
            console.log(error);
            return error;
        }
    });
    return response.responseJSON;
}

function getImageKeys() {
    var response = $.ajax({
        url: "GameRules/?handler=ImageKeys",
        type: 'GET',
        async: false,
        success: function (result) {
            console.log(result);
            return result;
        },
        error: function (error) {
            alert("Request To Acquire Image Keys Through Server Failed");
            console.log(error);
            return error;
        }
    });
    return response.responseJSON;
}

function getMLPrediction(imageURL) {
    var requestString = "&imageURL=" + imageURL;
    var response = $.ajax({
        url: "GamePlay/?handler=AIPrediction" + requestString,
        type: 'GET',
        async: false,
        success: function (result) {
            console.log(result);
            return result;
        },
        error: function (error) {
            alert("Request To Acquire Machine Learning Prediction Through Server Failed");
            console.log(error);
            return result;
        }
    });
    return response.responseJSON;
}

function getGroundTruth(imageURL) {
    var requestString = "&imageURL=" + imageURL;
    var response = $.ajax({
        url: "GamePlay/?handler=GroundTruth" + requestString,
        type: 'GET',
        async: false,
        success: function (result) {
            console.log(result);
            return result;
        },
        error: function (error) {
            alert("Request To Acquire Ground Truth Through Server Failed");
            console.log(error);
            return result;
        }        
    });
    if (response.responseJSON == true) {
        return "Healthy";
    } else {
        return "Unhealthy";
    }
}


/* Play a Game Module */

// -- Variables:
var voteFlag = true;
var imageCount = 1;
var currentPlantImage = localStorage.getItem("plantImage");
var yourScore = 0;
var aiScore = 0;

// -- Functions:
function getRandomImage() {    
    var imageKeys = JSON.parse(localStorage.getItem("imageKeys"));
    var randomNumber = generateRandomNumber(imageKeys.length);
    return imageKeys[randomNumber];
}

function initializeGame() {

    // Get and store the image keys:
    var imageKeys = getImageKeys();
    localStorage.setItem("imageKeys", JSON.stringify(imageKeys));

    // Initialize the plant image to a random image:
    var randomImage = getRandomImage();
    localStorage.setItem("plantImage", randomImage);
}

function continueToGameRedirect() {
    initializeGame();
    document.location = "GamePlay";
}

function continueToGameResults() {

    if (yourScore < aiScore) {
        document.getElementById("gameResultsHeader").innerHTML = "You Lost";
        document.getElementById("gameResultsSummary").innerHTML = "So sorry, but the AI correctly identified more images than you.";
    } else if (yourScore == aiScore) {
        document.getElementById("gameResultsHeader").innerHTML = "You Tied!";
        document.getElementById("gameResultsSummary").innerHTML = "Not bad! You're as smart as the computer.";
    } else {
        document.getElementById("gameResultsHeader").innerHTML = "You Won!";
        document.getElementById("gameResultsSummary").innerHTML = "Wow great job! You beat the machine!";
    }

    document.getElementById("gamePlayDiv").style.display = "none";
    document.getElementById("gameResultsDiv").style.display = "contents";
}

function generateRandomNumber(maxNumber) {
    return Math.floor(Math.random() * Math.floor(maxNumber));
}

function updateVote() {
    $("#castVoteOrContinue").prop("disabled", false);
}

function castVoteOrContinue() {

    voteFlag = !voteFlag;
    if (voteFlag) {

        if (imageCount == 10) {
            continueToGameResults();
        } else {
            // Increase Count:
            imageCount++;

            // Update Text Fields:
            document.getElementById("castVoteDiv").style.display = "contents";
            document.getElementById("votesAreInDiv").style.display = "none";
            document.getElementById("imageNumber").innerHTML = "Image " + imageCount;
            document.getElementById("castVoteOrContinue").innerHTML = "Submit";

            // Update Image:
            currentPlantImage = getRandomImage();
            $("#plantImage").attr("src", stitchImageURL(currentPlantImage));

            // Clear Previous Vote:
            $('input[name="health"]').prop('checked', false);
            $("#castVoteOrContinue").prop("disabled", true);
        }            

    } else {

        // Acquire Votes and Ground Truth:
        var userVote = $('input[name="health"]:checked').val();
        var mlVote = getMLPrediction(stitchImageURL(currentPlantImage));
        var groundTruth = getGroundTruth(currentPlantImage);

        // Update Scores:
        if (userVote == groundTruth) {
            yourScore++;
        }
        if (mlVote == groundTruth) {
            aiScore++;
        }

        // Update Text Fields:
        document.getElementById("castVoteDiv").style.display = "none";
        document.getElementById("votesAreInDiv").style.display = "contents";
        document.getElementById("yourScore").innerHTML = yourScore;
        document.getElementById("aiScore").innerHTML = aiScore;
        document.getElementById("imageNumber").innerHTML = "Image " + imageCount + " Result";
        document.getElementById("groundTruthPrediction").innerHTML = groundTruth;
        document.getElementById("aiPrediction").innerHTML = mlVote;
        document.getElementById("yourPrediction").innerHTML = userVote;
        document.getElementById("castVoteOrContinue").innerHTML = "Continue";
    }
    
}