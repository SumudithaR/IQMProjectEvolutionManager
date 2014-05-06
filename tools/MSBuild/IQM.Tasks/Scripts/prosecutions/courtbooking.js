/// <reference path="../global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../../libraries/json.js" />
/// <reference path="../../libraries/MicrosoftMvcAjax.js" />

// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
};

/*
* method for courtBooking functionality
*/
RIMS.CourtBooking = function() { };
/*
* Opens the Partial view for adding a courtBooking to the RIMS.Inspector.
*/
RIMS.CourtBooking.Add = function(caller, inputOptions) {

    var options = {
        Url: "/Prosecutions/CourtBooking/Create/"
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
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateCourtBookingDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Create CourtBooking');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.CourtBooking.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Opens the Partial view for editing a RIMS.CourtBooking.
* @param courtBookingId - id of the courtBooking objects
*/
RIMS.CourtBooking.Edit = function(caller, courtBookingId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
            type: "GET",
            dataType: "html",
            url: window["urlOffset"] + "/Prosecutions/CourtBooking/Edit/" + courtBookingId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditCourtBookingDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Edit CourtBooking');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.CourtBooking.ConfigureDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Changes the court cases current court booking
*/
RIMS.CourtBooking.Change = function (caller, courtCaseId, inputOptions) {
    $("#currentCourtBookingErrorMessage").hide();
    RIMS.Dialog.Prepare();
    // set the title and configure the help options
    $("#ajaxDialog").dialog('option', 'title', 'Court Booking Search');

    $("#dialogButton").click(function () {
        var options = {
            url: window["urlOffset"] + "/Prosecutions/CourtBooking/AjaxList/",
            page: 0,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function (item) {
                RIMS.CourtBooking._setCourtBooking(courtCaseId, item.Id, inputOptions);
                $("#ajaxDialog").dialog('close');
            }
        };
        RIMS.Dialog.LoadAjaxResults(options);
    });
    $("#ajaxDialog").dialog('open');
};

RIMS.CourtBooking._setCourtBooking = function (courtCaseId, courtBookingId, inputOptions) {

    var options = {};
    $.extend(options, inputOptions);

    // make the ajax call to set the court booking
    var postData = {
        "courtCaseId": courtCaseId,
        "courtBookingId": courtBookingId
    };

    var ajaxUrl = window["urlOffset"] + '/Prosecutions/CourtBooking/SetCourtBooking';
    $.ajax({
        url: ajaxUrl,
        data: postData,
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            RIMS.Global.Log(data);
            if (data.success) {
                $("#currentCourtBookingCourtName").text(data.CourtName);
                $("#currentCourtBookingDate").text(data.CourtDate);
            }
        },
        error: function (data) {
            // do something with the error
            $("#currentCourtBookingErrorMessage").show();
            $("#currentCourtBookingErrorMessage").text(data.message);
        }
    });
};

RIMS.CourtBooking._timer = null;
RIMS.CourtBooking._interval = 1000 * 3;

RIMS.CourtBooking.Print = function (caller, action, courtBookingId, workFlowStepType) {
    RIMS.Global.AddLoadingIcon(caller);

    var ajaxUrl = window["urlOffset"] + '/Prosecutions/CourtBooking/' + action + '/' + courtBookingId + '?' + new Date().getTime();
    $.ajax({
        url: ajaxUrl,
        dataType: 'json',
        data: { workFlowStepType: workFlowStepType },
        type: 'POST',
        success: function (data) {
            RIMS.Global.Log(data);

            var id = new Date().getTime();
            var processingDialog = $("<div>");
            processingDialog.attr("id", id);

            // Setup the loading dialog
            $(processingDialog).html('<div><div id="PrintProgressBar"></div><div id="PrintProgressMessage">Processing...</div><div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"><button type="button"  role="button" aria-disabled="false" rel="Cancel">Close</button></div></div>');
            $(processingDialog).find("button").button();
            $(processingDialog).dialog({
                title: 'Generating Files',
                autoOpen: false,
                width: 600,
                modal: true,
                resizable: false,
                close: function (event, ui) {
                    clearTimeout(RIMS.CourtBooking._timer);
                    $(this).dialog('destroy').remove();
                }
            });

            RIMS.CourtBooking.GetPrintStatus(data);
            $(processingDialog).dialog('open');
            RIMS.CourtBooking._timer = setInterval(function () { RIMS.CourtBooking.GetPrintStatus(data); }, RIMS.CourtBooking._interval);


            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.CourtBooking.ConfigureDialog();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            // do something with the error
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
        }
    });
};

RIMS.CourtBooking.GetPrintStatus = function (transaction) {
    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "/Home/ActionStatusMessage" + "?" + new Date().getTime(),
        data: {
            'transactionId': transaction
        },
        success: function (response) {
            $("#PrintProgressBar").progressbar({
                value: response.Percent
            });
            $("#PrintProgressMessage").text(response.Message);

            if (response.Complete == true) {
                clearTimeout(RIMS.CourtBooking._timer);
                $("#PrintProgressBar").slideUp();
                if (response.Success == true) {
                    $("#PrintProgressMessage").html('<p>You document is ready, please <a href="/ContentStorage/View/' + response.RelatedId + '">click here</a>.');
                } else {
                    $("#PrintProgressMessage").html('<p>An error occurred whilst trying to process the documents. ' + response.Message + '</a>.');
                }
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            // jsut ignore the error
        }
    });
};

/*
* Call back that is fired when a save attempt is made
*/
RIMS.CourtBooking.BeginRequest = function() {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

};

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.CourtBooking.EndRequest = function() {
    setTimeout("RIMS.CourtBooking._EndRequest()", 100);
};

/*
* actual callback
*/
RIMS.CourtBooking._EndRequest = function() {
    if ($("#CourtBookingDetailComplete").length > 0) {
        var result = jsonParse($("#CourtBookingDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#CourtBookingDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("div[name=CourtBooking_" + result.id + "]").html($("#CourtBookingDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function() {
                    $("#courtBookinges").find("li[id=CourtBooking_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#courtBookings").prepend($("#CourtBookingDetailCompleteHtml").html());
                $("#CurrentCourtBooking").html($("#CourtBookingDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#courtBookingsMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.CourtBooking.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};


RIMS.CourtBooking.ConfigureDialog = function () {
    RIMS.Global.DateTimePicker($("#Date"));
    RIMS.Global.ConfigureDialog();
};