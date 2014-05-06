/// <reference path="../Global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../../libraries/MicrosoftAjax.js" />
/// <reference path="../../libraries/json.js" />

// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
}

/*
* method for ReissueSummonsReason functionality
*/
RIMS.ReissueSummonsReason = function() { };
/*
* Opens the Partial view for adding a ReissueSummonsReason to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.ReissueSummonsReason.Add = function(caller, courtCaseId, inputOptions) {

    var options = {
        Url: "/Prosecutions/CourtCase/ReissueSummons"
    };

    $.extend(options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
            type: "GET",
            dataType: "html",
            url: options.Url + "/" + courtCaseId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateReissueSummonsReasonDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Create ReissueSummonsReason');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.ReissueSummonsReason.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Opens the Partial view for editing a RIMS.ReissueSummonsReason.
* @param ReissueSummonsReasonId - id of the ReissueSummonsReason objects
*/
RIMS.ReissueSummonsReason.Edit = function(caller, reissueSummonsReasonId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Prosecutions/ReissueSummonsReason/Edit/" + reissueSummonsReasonId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditReissueSummonsReasonDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Edit ReissueSummonsReason');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.ReissueSummonsReason.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Call back that is fired when a save attempt is made
*/
RIMS.ReissueSummonsReason.BeginRequest = function() {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();
};

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.ReissueSummonsReason.EndRequest = function() {
    setTimeout("RIMS.ReissueSummonsReason._EndRequest()", 100);
};

/*
* actual callback
*/
RIMS.ReissueSummonsReason._EndRequest = function() {
    if ($("#ReissueSummonsReasonDetailComplete").length > 0) {
        var result = jsonParse($("#ReissueSummonsReasonDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        if (typeof RIMS.WorkFlow != 'undefined') {
            var courtCaseId = $("#Data_CourtCaseId").val();
            if (courtCaseId > 0) {
                RIMS.WorkFlow.ReloadOverview(courtCaseId);
            }
        }

        // remove the element from the dom as not to cause conflicts later.
        $("#ReissueSummonsReasonDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("div[name=ReissueSummonsReason_" + result.id + "]").html($("#ReissueSummonsReasonDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function() {
                    $("#ReissueSummonsReasones").find("li[id=ReissueSummonsReason_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#ReissueSummonsReasones").append($("<li>").attr("id", "ReissueSummonsReason_" + result.id).append($("#ReissueSummonsReasonDetailCompleteHtml").html()));
                $("#CurrentReissueSummonsReason").html($("#ReissueSummonsReasonDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#ReissueSummonsReasonsMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.ReissueSummonsReason.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};


RIMS.ReissueSummonsReason.ConfigureDialog = function() {

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
};