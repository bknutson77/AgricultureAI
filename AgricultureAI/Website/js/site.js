// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

/* Login/Register/Continue-As-Guest Functions */
function loginRedirect() {
    document.location = "Login";
}

function registerRedirect() {
    document.location = "Register";
}

function continueAsGuestRedirect() {
    document.location = "ModuleHome";
}

function returnToLoginRegisterHome() {
    document.location = "Index";
}