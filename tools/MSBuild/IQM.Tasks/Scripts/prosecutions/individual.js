/// <reference path="RIMS.Global.js" />
/// <reference path="../libraries/jquery-1.7.1-vsdoc.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method for individual functionality
*/
RIMS.Individual = function () { }
/*
* Opens the Partial view for adding a individual to the RIMS.Inspector.
* @param courtCaseId - id of the inspector objects
*/
RIMS.Individual.Add = function (caller, courtCaseId, inputOptions) {

    var Options = {
        Url: "/Prosecutions/Individual/Create/"
    }
    $.extend(Options, inputOptions);
    RIMS.Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + Options.Url + courtCaseId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'CreateIndividualDialog');
            $("#partialViewDialog").dialog('option','title','Create Individual');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Individual.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Opens the Partial view for editing a RIMS.Individual.
* @param individualId - id of the individual to edit
*/
RIMS.Individual.Edit = function (caller, individualId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Individual/EditDialog/" + individualId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Inspector', 'EditIndividualDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit Individual');

            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Individual.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

RIMS.Individual.Search = function (caller, courtCaseId) {

    RIMS.Global.AddLoadingIcon(caller);
    RIMS.Dialog.Prepare();
    // set the title and configure the help options
    $("#ajaxDialog").dialog('option', 'title', 'Individual Search');
    RIMS.Dialog.SetupHelp($("#ajaxDialog"), 'Shared', 'individual-search-dialog');

    $("#dialogButton").click(function () {
        var options = {
            url: '/Prosecutions/Individual/AjaxList/',
            page: 0,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function (item) {
                $("#ajaxDialog").dialog('close');

                $("#partialViewDialog").dialog('open');
                $("#partialViewDialogData").children().remove();
                $("#partialViewDialogLoading").show();

                RIMS.Individual.AssignToCourtCase(courtCaseId, item.Id);
            },
            no_results: function (dialogPane) {
                var message = $("<p>").text("No results where found for the search result, you can create a new individual by clicking the button below");
                $("<li>").append(message).appendTo(dialogPane);

                var createButton = $("<input>").attr("type", "button").val("Create New Individual").button();
                createButton.click(function () {
                    RIMS.Individual.Add($(this), courtCaseId, {});
                    $("#ajaxDialog").dialog('close');
                });
                $("<li>").append("<p>").append(createButton).appendTo(dialogPane);

            }
        };
        RIMS.Dialog.LoadAjaxResults(options);
    });
    $("#ajaxDialog").dialog('open');

    RIMS.Global.RemoveLoadingIcon(caller);
}

RIMS.Individual.AssignToCourtCase = function (courtCaseId, individualId) {

    // show the loading dialog
    // post the data to the and load the data into the edit individual page.
    var resultDiv = $("#individualFormContainer");
    var postData = { courtCaseId: courtCaseId, individualId: individualId };
    $.ajax({
        type: "POST",
        dataType: "html",
        url: urlOffset + "/Prosecutions/CourtCase/AssignIndividual?" + new Date().getTime(),
        data: postData,
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);

            // hide the loading dialog
            $("#partialViewDialog").dialog('close');
            $("#partialViewDialogLoading").hide();

            RIMS.Global.SetUpButtons();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ////RIMS.Global.RemoveLoadingIcon(caller);
            ////RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Call back that is fired when a save attempt is made
*/
RIMS.Individual.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Individual.EndRequest = function () {
    setTimeout("RIMS.Individual._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.Individual._EndRequest = function () {
    if ($("#IndividualDetailComplete").length > 0) {
        var result = jsonParse($("#IndividualDetailComplete").val());

        RIMS.Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#IndividualDetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("div[name=Individual_" + result.id + "]").html($("#IndividualDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#individuales").find("li[id=Individual_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#individualFormContainer").html($("#IndividualDetailCompleteHtml").html());

                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                RIMS.Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Individual.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
}


RIMS.Individual.ConfigureDialog = function () {

    RIMS.Global.DatePicker($("#DateOfBirth"));

    $("#partialViewDialogData").find("input[name=Over18]").click(function () {
        if ($(this).is(":checked")) {
            $("#partialViewDialogData").find("input[name=ParentGuardianName]").parent().slideUp('slow');
        } else {
            $("#partialViewDialogData").find("input[name=ParentGuardianName]").parent().slideDown('slow');
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