/// <reference path="RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method for address functionality
*/
RIMS.Address = function () { }
/*
* Opens the Partial view for adding a address to the RIMS.Inspector.
* @param inspectorId - id of the inspector objects
*/
RIMS.Address.Add = function (caller, inspectorId, inputOptions) {

    var Options = {
        Url: "/Prosecutions/Address/Create/",
        type: "Individual"
    }
    $.extend(Options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + Options.Url + inspectorId + "/" + Options.type + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateAddressDialog');
            $("#partialViewDialog").dialog('option','title','Create Address');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Address.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Opens the Partial view for editing a RIMS.Address.
* @param addressId - id of the address objects
*/
RIMS.Address.Edit = function (caller, addressId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Address/Edit/" + addressId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditAddressDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit Address');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Address.ConfigureDialog();
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
RIMS.Address.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Address.EndRequest = function () {
    setTimeout("RIMS.Address._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.Address._EndRequest = function () {
    if ($("#AddressDetailComplete").length > 0) {
        var result = jsonParse($("#AddressDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#AddressDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("div[name=Address_" + result.id + "]").html($("#AddressDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#addresses").find("li[id=Address_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#addresses").append($("<li>").attr("id", "Address_" + result.id).append($("#AddressDetailCompleteHtml").html()));
                $("#CurrentAddress").html($("#AddressDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                $("#addresssMessage").remove();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Address.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
}


RIMS.Address.ConfigureDialog = function () {

    RIMS.Global.DatePicker($("#KnownMoveDate"));

    $("#partialViewDialogData").find("input[name=IsVerified]").click(function () {
        if ($(this).is(":checked")) {
            $("#partialViewDialogData").find("div[name=AddressVerifiedFields]").slideDown('slow');
        } else {
            $("#partialViewDialogData").find("div[name=AddressVerifiedFields]").slideUp('slow');
        }
    });

    $("#partialViewDialogData").find("input[name=GivenAsFalse]").click(function () {
        if ($(this).is(":checked")) {
            $("#partialViewDialogData").find("div[name=GivenAsFalseFields]").slideDown('slow');
        } else {
            $("#partialViewDialogData").find("div[name=GivenAsFalseFields]").slideUp('slow');
        }
    });

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
}