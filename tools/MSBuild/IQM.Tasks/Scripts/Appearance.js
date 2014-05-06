/// <reference path="RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* Appearance Features
*/
RIMS.Appearance = function () { }

/*
* Add new appearance feature to the individual
* @param individualId - id of the individual
*/
RIMS.Appearance.Add = function (caller, individualId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Individual/CreateAppearanceFeature/" + individualId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Individual', 'CreateAppearanceFeatureDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Create Appearance Feature');
            RIMS.Global.RemoveLoadingIcon(caller);

            RIMS.Appearance.ConfigureDialog();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}

/*
* Add new appearance feature to the individual
* @param appearanceFeatureId - id of the feature to edit
*/
RIMS.Appearance.Edit = function (caller, appearanceFeatureId) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    RIMS.Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/Prosecutions/Individual/EditAppearanceFeature/" + appearanceFeatureId + "?" + new Date().getTime(),
        data: {},
        success: function (response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            RIMS.Dialog.SetupHelp($("#partialViewDialog"), 'Individual', 'EditAppearanceFeatureDialog');
            $("#partialViewDialog").dialog('option', 'title', 'Edit Appearance Feature');

            RIMS.Global.RemoveLoadingIcon(caller);

            RIMS.Appearance.ConfigureDialog();
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
RIMS.Appearance.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();

}

/*
* Callback that is fired after a save attempt has been made.
*/
RIMS.Appearance.EndRequest = function () {
    setTimeout("RIMS.Appearance._EndRequest()", 100);
}

/*
* actual callback
*/
RIMS.Appearance._EndRequest = function () {
    if ($("#AppearanceFeatureDetailComplete").length > 0) {
        var result = jsonParse($("#AppearanceFeatureDetailComplete").val());
        // remove the element from the dom as not to cause conflicts later.
        $("#AppearanceFeatureDetailComplete").remove();

        RIMS.Global.AjaxHook(result);

        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("#Appearancefeature_" + result.id).html($("#AppearanceFeatureDetailCompleteHtml").html());
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                RIMS.Appearance.ConfigureDialog();
                return;
            } else if (result.deleted) {
                setTimeout(function () {
                    $("#appearancefeatures").find("li[id=Appearancefeature_" + result.id + "]").fadeOut();
                }, 3000);
            } else {
                // close the dialog, and append the contents to the list
                $("#appearancefeatures").append($("<li>").attr("id", "Appearancefeature_" + result.id).append($("#AppearanceFeatureDetailCompleteHtml").html()));
                $("#partialViewDialog").dialog('close');

                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();

                RIMS.Appearance.ConfigureDialog();
                return;
            }
        }
    }


    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    RIMS.Appearance.ConfigureDialog();

}

RIMS.Appearance.ResetDropDown = function () {
    $("#partialViewDialog").find("select[name$=DropDownItemId]").children().remove();
    var metaData = $("#partialViewDialog").find("select[name=TempFeatureType]").val();

    var featureList = $("#partialViewDialog").find("select[name$=DropDownItemId]");
    var prevId = $("#partialViewDialog").find("input[name=prevDropDownItemId]").val();

    for (var i in window.FeatureValues) {
        var feature = window.FeatureValues[i];
        if (feature.MetaData == metaData) {
            var option = $("<option>").val(feature.Id).text(feature.Name);
            if (feature.Id == prevId) {
                option.attr("selected", "selected");
            }
            featureList.append(option);
        }
    }
}

RIMS.Appearance.ConfigureDialog = function () {
    RIMS.Global.SetUpButtons();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });

    $("#partialViewDialog").find("select[name=TempFeatureType]").change(function () {
        RIMS.Appearance.ResetDropDown();
    })
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();

    RIMS.Appearance.ResetDropDown();
}