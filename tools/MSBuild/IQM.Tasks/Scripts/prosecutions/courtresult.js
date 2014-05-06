/// <reference path="../Global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../../libraries/json.js" />

// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
}

/*
* method for courtResults functionality
*/
RIMS.CourtResults = function() { };
/*
* Opens the Partial view for adding a courtResults to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.CourtResults.Add = function(caller, inputOptions) {

    var options = {
        Url: "/Prosecutions/CourtResults/Create"
    };

    $.extend(options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
            type: "GET",
            dataType: "html",
            url: window["urlOffset"] + options.Url + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateCourtResultsDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Create CourtResults');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.CourtResults.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Opens the Partial view for editing a RIMS.CourtResults.
* @param courtResultsId - id of the courtResults objects
*/
RIMS.CourtResults.Edit = function(caller, courtResultsId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
            type: "GET",
            dataType: "html",
            url: window["urlOffset"] + "/Prosecutions/CourtResults/Edit/" + courtResultsId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditCourtResultsDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Edit CourtResults');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.CourtResults.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

RIMS.CourtResults.SetAdjournedBooking = function (caller, courtResultId, inputOptions) {
    var options = {
        Url: window["urlOffset"] + "/Prosecutions/CourtBooking/SetAdjournedBooking"
    };

    $.extend(options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    var postData = { courtResultId: courtResultId, courtBookingId: $("#adjournedDetails" + courtResultId).find("#AdjournedCourtBookingId").val() };

    $.ajax({
        url: options.Url,
        data: postData,
        dataType: 'html',
        type: 'POST',
        success: function (response) {
            $("#adjournedDetails" + courtResultId).html('');
            $("#adjournedDetails" + courtResultId).html(response);

            RIMS.Global.SetUpButtons();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
        }
    });
};

/*
* Call back that is fired when a save attempt is made
*/
RIMS.CourtResults.BeginRequest = function() {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

};

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.CourtResults.EndRequest = function() {
    setTimeout("RIMS.CourtResults._EndRequest()", 100);
};

/*
* actual callback
*/
RIMS.CourtResults._EndRequest = function() {
    if ($("#CourtResultsDetailComplete").length > 0) {
        var result = jsonParse($("#CourtResultsDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#CourtResultsDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#CourtResults_" + result.id).html($("#CourtResultsDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function() {
                    $("#courtResults").find("li[id=CourtResults_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#courtResults").append($("#CourtResultsDetailCompleteHtml").html());
                $("#CurrentCourtResults").html($("#CourtResultsDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#courtResultsMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.CourtResults.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};


RIMS.CourtResults.ConfigureDialog = function() {

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
};