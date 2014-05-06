/// <reference path="../RIMS.Global.js" />

/*
* Work Flow Functions
*/
RIMS.Print = function() { };

RIMS.Print.Cancel = function () {
    $("#document-pane").slideUp("slow");
};

/*
* Begin Work Flow Request
*/
RIMS.Print.BeginRequest = function() {
    // hide the process div
    $("#document-pane").slideUp("slow");

    // show the executing details
    $("#process-document-pane").slideDown("slow");
};

/*
* End Work Flow Request
*/
RIMS.Print.EndRequest = function() {
    setTimeout("RIMS.Print._EndRequest()", 1000);
};

RIMS.Print._EndRequest = function () {

    RIMS.Global.AjaxHook({ result: true });

    $("#process-document-pane").slideUp("slow", function () {
        $("#document-pane").slideDown("slow");
    });

    ////RIMS.Global.AjaxHook({ result: true });
    RIMS.Global.SetUpButtons();
    RIMS.Global.SetupWidgets();
};