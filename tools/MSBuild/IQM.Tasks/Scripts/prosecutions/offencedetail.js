/// <reference path="../RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method for offenceDetail functionality
*/
RIMS.OffenceDetail = function () { }
/*
* Opens the Partial view for adding a offenceDetail to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.OffenceDetail.Add = function (caller, courtCaseId, inputOptions) {

    var Options = {
        Url: "/Prosecutions/OffenceDetail/Create"
    }
    $.extend(Options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + Options.Url + "/" + courtCaseId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateOffenceDetailDialog');
            $("#partialViewDialog").dialog('option','title','Create OffenceDetail');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.OffenceDetail.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Opens the Partial view for editing a RIMS.OffenceDetail.
* @param offenceDetailId - id of the offenceDetail objects
*/
RIMS.OffenceDetail.Edit = function (caller, offenceDetailId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/OffenceDetail/Edit/" + offenceDetailId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditOffenceDetailDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit OffenceDetail');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.OffenceDetail.ConfigureDialog();
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
RIMS.OffenceDetail.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.OffenceDetail.EndRequest = function () {
    setTimeout("RIMS.OffenceDetail._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.OffenceDetail._EndRequest = function () {
    if ($("#OffenceDetailDetailComplete").length > 0) {
        var result = jsonParse($("#OffenceDetailDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#OffenceDetailDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#OffenceDetail_" + result.id).html($("#OffenceDetailDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                RIMS.Global.SetupWidgets();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#offenceDetails").find("li[id=OffenceDetail_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#offenceDetails").append($("<li>").attr("id", "OffenceDetail_" + result.id).append($("#OffenceDetailDetailCompleteHtml").html()));
                $("#CurrentOffenceDetail").html($("#OffenceDetailDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#offenceDetailsMessage").remove();
                RIMS.Global.SetUpButtons();
                RIMS.Global.SetupWidgets();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.OffenceDetail.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
}


RIMS.OffenceDetail.ConfigureDialog = function () {

    RIMS.Global.DatePicker($("#partialViewDialog").find("input[name=Date]"))

    // setup the destination searches
    RIMS.Global.SetupDestinationFields();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
}