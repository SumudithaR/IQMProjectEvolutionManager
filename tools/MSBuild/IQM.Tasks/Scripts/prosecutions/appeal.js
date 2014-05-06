// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
}

$(document).ready(function () {
    RIMS.Appeal.ConfigureDialog();
});

RIMS.Appeal = function() { };

RIMS.Appeal.Add = function (caller, courtCaseId, inputOptions) {

    var options = {
        Url: "/Prosecutions/Appeal/CreateDialog"
    };
    
    $.extend(options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
            type: "GET",
            dataType: "html",
            url: urlOffset + options.Url + "/" + courtCaseId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateAppealDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Create Appeal');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Appeal.ConfigureDialog();
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
RIMS.Appeal.BeginRequest = function() {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

};

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Appeal.EndRequest = function() {
    setTimeout("RIMS.Appeal._EndRequest()", 100);
};

/*
* actual callback
*/
RIMS.Appeal._EndRequest = function() {
    if ($("#AppealDetailComplete").length > 0) {
        var result = jsonParse($("#AppealDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#AppealDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("div[name=Appeal_" + result.id + "]").html($("#AppealDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                // TODO add delete appeal
            } else {
                // close the dialog, and append the contents to the list
                $("#Appeals").append($("#AppealDetailCompleteHtml").html());
                $("#CurrentAppeal").html($("#AppealDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#AppealsMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Appeal.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};


RIMS.Appeal.ConfigureDialog = function () {

    ////$("#Appeals").accordion();

    // if we are being called fromthe quicked edits setup the current appeal info
    RIMS.Global.DatePicker($("input[name$=SendResultsLetter]"));
    RIMS.Global.DatePicker($("input[name$=DateToPay]"));

    RIMS.Global.DatePicker($("#partialViewDialog").find("input[name$=Created]"));
    RIMS.Global.DatePicker($("#partialViewDialog").find("input[name$=DateToPay]"));
    ////    RIMS.Global.DatePicker($("div[rel=appeal]").find("input[name=Data.Created]"));

    $("div[rel=appeal], #partialViewDialogData").find("input[name$=ProxyAppeal]").each(function () {
        $(this).click(function () {
            var proxyAppealFields = $(this).parent().parent().find("div[name=proxy-appeal-fields]");
            if ($(this).is(":checked")) {
                proxyAppealFields.slideDown();
            } else {
                proxyAppealFields.slideUp();
            }
        });
    });

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });

    RIMS.Global.SetUpButtons();

    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
};