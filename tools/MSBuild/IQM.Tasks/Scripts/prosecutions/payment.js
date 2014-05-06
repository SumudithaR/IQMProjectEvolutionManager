/// <reference path="../global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../../libraries/json.js" />
/// <reference path="../../libraries/MicrosoftAjax.js" />

// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
};

/*
* functions for penalty fares
*/
RIMS.Payment = function() { };

$(document).ready(function () {
    $("#aAddPayment").click(function () {
        RIMS.Payment.Add($(this), $(this).attr("rel"), {type: $(this).attr("data-type")});
    });

    $("#aLinkPayment").click(function () {
        RIMS.Payment.Link($(this), $(this).attr("rel"));
    });
    /*
    $("#aAddPaymentOtsf").click(function () {
        RIMS.Payment.Add($(this), $(this).attr("rel"), {type: "OnTheSpotFine"});
    });*/
    
});

/*
* function to show the create new penalty fare dialog
* @param itemId - itemId
*/
RIMS.Payment.Add = function(caller, itemId, inputOptions) {
    // load the content into the partial view controller.
    // load the content 
    var resultDiv = $("#partialViewDialogData");

    var options = {
        type: "PenaltyFareNotice"
    };
    $.extend(options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
            type: "GET",
            dataType: "html",
            url: window["urlOffset"] + "/Prosecutions/Payment/Create/" + options.type + "/" + itemId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'PenaltyFare', 'CreatePaymentDialog');
                $("#partialViewDialog").dialog('option', 'title', 'Create Payment');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Payment.SetupDialog();
            },
            error: function(jqXhr, textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};

/*
* Edit Payment
* @param paymentId - id of the payment instance to update
*/
RIMS.Payment.Edit = function(caller, paymentId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
            type: "GET",
            dataType: "html",
            url: window["urlOffset"] + "/Prosecutions/Payment/Edit/" + paymentId + "?" + new Date().getTime(),
            data: { },
            success: function(response) {
                resultDiv.html('');
                resultDiv.html(response);
                $("#partialViewDialog").dialog('open');
                RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'PenaltyFare', 'EditPaymentDialog');
                $("#partialViewDialog").dialog('option', 'title', 'EditPayment');

                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Payment.SetupDialog();
            },
            error: function(jqXhr
            , textStatus, errorThrown) {
                RIMS.Global.RemoveLoadingIcon(caller);
                RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
            }
        });
};


RIMS.Payment.Remove = function (caller, penaltyFareId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);
    $.ajax({
        type: "GET",
        dataType: "html",
        url: window["urlOffset"] + "/Prosecutions/Payment/Delete/" + penaltyFareId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Payment', 'RemovePaymentDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Remove Payment');
            $("#aAddPenaltyFare").removeClass("loading-content");
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Payment.ConfigureDialog();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXhr, textStatus, errorThrown);
        }
    });
};

/*
* Searches and links an unlinked payment to an object
*/
RIMS.Payment.Link = function(caller, id, inputOptions) {
    var options = {
        type: "PenaltyFareNotice"
    };
    $.extend(options, inputOptions);

    RIMS.Dialog.Prepare();
    // set the title and configure the help options
    $("#ajaxDialog").dialog('option', 'title', 'Unlinked Payments Search');

    $("#dialogButton").click(function () {
        var options = {
            url: window["urlOffset"] + "/Prosecutions/Payment/UnlinkedAjaxList/",
            page: 0,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function (item) {
                RIMS.Payment._Link(id, item.Id, options);
                $("#ajaxDialog").dialog('close');
            }
        };
        RIMS.Dialog.LoadAjaxResults(options);
    });
    $("#ajaxDialog").dialog('open');
};

RIMS.Payment._Link = function (id, paymentId, inputOptions) {
    var options = {
        type: "PenaltyFareNotice"
    };
    $.extend(options, inputOptions);

    // make the ajax call to set the court booking
    var postData = {
        "id": id,
        "paymentId": paymentId,
        type: options.type
    };

    var ajaxUrl = window["urlOffset"] + '/Prosecutions/Payment/Link';
    $.ajax({
        url: ajaxUrl,
        data: postData,
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            RIMS.Global.Log(data);
            if (data.success) {
                // things have worked out ok so get the detail view
                // make a call and load the data
                $.ajax({
                        type: "GET",
                        dataType: "html",
                        url: window["urlOffset"] + "/Prosecutions/Payment/AjaxDetail/" + paymentId + "?" + new Date().getTime(),
                        data: { },
                        success: function(response) {
                            $("#payments").append($("<li>").attr("id", "Payment_" + paymentId).append(response));
                            $("#paymentsMessage").remove();
                        }
                    });
            }
        },
        error: function (data) {
            // do something with the error
            $("#currentCourtBookingErrorMessage").show();
            $("#currentCourtBookingErrorMessage").text(data.message);
        }
    });
};

/*
* Call back that is fired when a save attempt is made
*/
RIMS.Payment.BeginRequest = function() {
    $("#partialViewDialogData").children().remove();
    $("#partialViewDialogLoading").show();
};

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Payment.EndRequest = function() {
    setTimeout("RIMS.Payment._EndRequest()", 100);
};

/*
* actual callback
*/
RIMS.Payment._EndRequest = function () {
    if ($("#PaymentDetailComplete").length > 0) {
        var result = jsonParse($("#PaymentDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#PaymentDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#Payment_" + result.id).html($("#PaymentDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Payment.SetupDialog();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#payments").find("li[id=Payment_" + result.id + "]").fadeOut();
                    $("#partialViewDialog").dialog('close');
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#payments").append($("<li>").attr("id", "Payment_" + result.id).append($("#PaymentDetailCompleteHtml").html()));
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#paymentsMessage").remove();
                RIMS.Payment.SetupDialog();
                return;
            }
        }
    }
    RIMS.Payment.SetupDialog();
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
};

RIMS.Payment.SetupDialog = function() {

    RIMS.Global.DateTimePicker($("#partialViewDialog").find("input[name=Paid]"));

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });

    $("#partialViewDialog").find("input[name=NotesToggle]").click(function() {
        if ($(this).is(":checked")) {
            $("#partialViewDialog").find("textarea[name=Notes]").parent().slideDown('slow');
        } else {
            $("#partialViewDialog").find("textarea[name=Notes]").parent().slideUp('slow');
        }
    });

    $("#partialViewDialog").find("input[name=Cancelled]").click(function() {
        if ($(this).is(":checked")) {
            $("#partialViewDialog").find("div[name=cancelledFields]").slideDown('slow');
        } else {
            $("#partialViewDialog").find("div[name=cancelledFields]").slideUp('slow');
        }
    });

    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();

    RIMS.Global.SetUpButtons();
};

/*
* create and open the dialog to view the barrier train work comment
*/
RIMS.Payment.ViewComment = function(hiddenField) {
    RIMS.Global.ViewComment(hiddenField);
};

RIMS.Payment.ConfigureDialog = function() {
    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
};