/// <reference path="RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method for court functionality
*/
RIMS.Court = function () { }
/*
* Opens the Partial view for adding a court to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.Court.Add = function (caller, inputOptions) {

    var Options = {
        Url: "/Prosecutions/Court/Create/",
    }
    $.extend(Options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + Options.Url + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateCourtDialog');
            $("#partialViewDialog").dialog('option','title','Create Court');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Court.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Opens the Partial view for editing a RIMS.Court.
* @param courtId - id of the court objects
*/
RIMS.Court.Edit = function (caller, courtId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Court/Edit/" + courtId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditCourtDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit Court');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Court.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Call back that is fired when a save attempt is made
*/
RIMS.Court.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Court.EndRequest = function () {
    setTimeout("RIMS.Court._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.Court._EndRequest = function () {
    if ($("#CourtDetailComplete").length > 0) {
        var result = jsonParse($("#CourtDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#CourtDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#Court_" + result.id).html($("#CourtDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#courts").find("li[id=Court_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#courts").append($("#CourtDetailCompleteHtml").html());
                $("#CurrentCourt").html($("#CourtDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#courtsMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Court.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
}


RIMS.Court.ConfigureDialog = function () {

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
}