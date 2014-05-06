/// <reference path="RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method for offence functionality
*/
RIMS.Offence = function () { }
/*
* Opens the Partial view for adding a offence to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.Offence.Add = function (caller, inputOptions) {

    var Options = {
        Url: "/Prosecutions/Offence/Create/"
    }
    $.extend(Options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + Options.Url + "/" + Options.type + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateOffenceDialog');
            $("#partialViewDialog").dialog('option','title','Create Offence');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Offence.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Opens the Partial view for editing a RIMS.Offence.
* @param offenceId - id of the offence objects
*/
RIMS.Offence.Edit = function (caller, offenceId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Offence/Edit/" + offenceId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditOffenceDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit Offence');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Offence.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

RIMS.Offence.ViewSummary = function(caller, offenceId){
    RIMS.Global.AddLoadingIcon(caller);
    $.ajax({
        type: "GET",
        dataType: "json",
        url: urlOffset + "/Prosecutions/Offence/Summary/" + offenceId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            var id = new Date().getTime();
            var commentDialog = $("<div>");
            commentDialog.html('<textarea name="comment' + id + '" style="width: 100%;height: 100%" readonly="readonly"></textarea>')
            commentDialog.attr("id", id);
            $("<body>").append(commentDialog);

            commentDialog.find("textarea[name=comment" + id + "]").val(response)

            $(commentDialog).dialog({
                title: 'View Offence',
                autoOpen: false,
                width: 400,
                height: 300,
                modal: false,
                buttons: {
                    Close: function () {
                        $(commentDialog).dialog('close');
                        $(commentDialog).remove();
                    }
                }
            });
            $(commentDialog).dialog('open');

            RIMS.Global.RemoveLoadingIcon(caller);
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
RIMS.Offence.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Offence.EndRequest = function () {
    setTimeout("RIMS.Offence._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.Offence._EndRequest = function () {
    if ($("#OffenceDetailComplete").length > 0) {
        var result = jsonParse($("#OffenceDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#OffenceDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#Offence_" + result.id).html($("#OffenceDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#offences").find("li[id=Offence_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#offences").append($("<li>").attr("id", "Offence_" + result.id).append($("#OffenceDetailCompleteHtml").html()));
                $("#CurrentOffence").html($("#OffenceDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#offencesMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Offence.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
}


RIMS.Offence.ConfigureDialog = function () {

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
}