// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


/* ------------------------------------------------------------ Functions that need to be called on every reload ------------------------------------------------------ */
/* -------------------------------------------------------------------------------------------------------------------------------------------------------------------- */

/**
 * Update Plant Image
 * @function
 * @description Upon reloading the site, this function updates the images in the game to the specified local storage image.
 */
function updatePlantImage() {
    var plantImage = localStorage.getItem("plantImage");
    if (plantImage !== null && plantImage !== undefined) {
        $("#plantImage").attr("src", stitchImageURL(plantImage));
    }
}
updatePlantImage();


/* ---------------------------------------------------------------- Login/Register/Continue-As-Guest Functions --------------------------------------------------------- */
/* --------------------------------------------------------------------------------------------------------------------------------------------------------------------- */

/**
 * Login Redirect
 * @function
 * @description Redirects to the login page.
 */
function loginRedirect() {
    document.location = "Login";
}

/**
 * Register Redirect
 * @function
 * @description Redirects to the register page.
 */
function registerRedirect() {
    document.location = "Register";
}

/**
 * Continue as Guest Redirect
 * @function
 * @description Sets the user to "Guest" and redirects to the module home page.
 */
function continueAsGuestRedirect() {

    // Set user information:
    localStorage.setItem("currentUser","Guest");

    // Advance and confirm:
    document.location = "ModuleHome";
    alert("You are continuing as guest.");
}

/**
 * Return to Login/Register Home
 * @function
 * @description Redirects to the login/register/continue-as-guest page.
 */
function returnToLoginRegisterHome() {
    document.location = "Index";
}

/**
 * Submit Login
 * @function
 * @description Queries the server to check the database for a password match. If the user provided the correct information they will be logged in and forwarded to the module home page, otherwise an error message will be displayed.
 */
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
                alert("Success! You have been logged in as: " + currentUser);
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

/**
 * Submit Register
 * @function
 * @description Requests the server to enter user information into the database. If the username provided is already taken, an error message will be displayed, otherwise they will be registered, logged in, and forwarded to the module home page.
 */
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
                alert("Success! You have been registered and logged in as: " + currentUser);
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


/* -------------------------------------------------------------------------------- Module Home Functions --------------------------------------------------------------- */
/* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- */

/**
 * Learn about AI Redirect
 * @function
 * @description Redirects to the learn about AI page.
 */
function learnAboutAIRedirect() {
    document.location = "LearnAboutAI";
}

/**
 * Play a Game Redirect
 * @function
 * @description Redirects to the Game Rules page (first page of the game).
 */
function playAGameRedirect() {
    document.location = "GameRules";
}

/**
 * Return to Module Home
 * @function
 * @description Redirects to the module home page.
 */
function returnToModuleHome() {
    document.location = "ModuleHome";
}


/* ---------------------------------------------------------------------- AI/Machine Learning Functions ------------------------------------------------------ */
/* ----------------------------------------------------------------------------------------------------------------------------------------------------------- */

/**
 * Stitch Image URL
 * @function
 * @description Given an image key, stitched together the url of the image in the firebase database.
 * @param {string} imageKey - The image key, or name (ex. DSC00025.JPG).
 * @returns The full url path of the image.
 */
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

/**
 * Get Firebase URL
 * @function
 * @description A request to the server to obtain the firebase URL.
 * @returns The firebase URL.
 */
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

/**
 * Get Image Keys
 * @function
 * @description Gets the array of image keys (or names) from the server, which has a copy from the parsed annotations provided by the research group.
 * @returns The array of image keys, or names (ex. DSC00025.JPG).
 */
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

/**
 * Get ML Prediction
 * @function
 * @description Provided an image URL, requests the machine learning prediction from the server as to whether the image is healthy or not.
 * @param {string} imageURL - The url of the image.
 * @returns The predicted label (healthy or unhealthy).
 */
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

/**
 * Get Ground Truth
 * @function
 * @description Provided an image URL, requests the ground truth from the server as to whether the image is healthy or not.
 * @param {string} imageURL - The url of the image.
 * @returns The ground truth label (healthy or unhealthy).
 */
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


/* ----------------------------------------------------------------------------- Play a Game Module --------------------------------------------------------------------------- */
/* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- */

// -- Variables:
var voteFlag = true;
var imageCount = 1;
var currentPlantImage = localStorage.getItem("plantImage");
var yourScore = 0;
var aiScore = 0;

/**
 * Get Random Image
 * @function
 * @description Helper function to obtain a random image from the database.
 * @returns The image key (or name) of a random image from the database (ex. DSC00025.JPG).
 */
function getRandomImage() {    
    var imageKeys = JSON.parse(localStorage.getItem("imageKeys"));
    var randomNumber = Math.floor(Math.random() * Math.floor(imageKeys.length));
    return imageKeys[randomNumber];
}

/**
 * Initialize Game
 * @function
 * @description Initializes the game when the user proceeds to playing a game against the AI. Variables are reinitialized.
 */
function initializeGame() {

    // Get and store the image keys:
    var imageKeys = getImageKeys();
    localStorage.setItem("imageKeys", JSON.stringify(imageKeys));

    // Initialize the plant image to a random image:
    var randomImage = getRandomImage();
    localStorage.setItem("plantImage", randomImage);
}

/**
 * Continue to Game Redirect
 * @function
 * @description Redirects to the game play page, but before doing so, initializes the game.
 */
function continueToGameRedirect() {
    initializeGame();
    document.location = "GamePlay";
}

/**
 * Continue to Game Results
 * @function
 * @description Computes the appropriate result of the game and displays the outcome (whether you have won or lost, or tied).
 */
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

/**
 * Update Vote
 * @function
 * @description Helper function called whenever a new vote is cast. This function enables the submit button, because the user has now voted.
 */
function updateVote() {
    $("#castVoteOrContinue").prop("disabled", false);
}

/**
 * Cast Vote or Continue
 * @function
 * @description This function is called whenever the user casts their vote on an image in the game (as healthy or unhealthy) or continues to the next image. Most of the game logic is here.
 */
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