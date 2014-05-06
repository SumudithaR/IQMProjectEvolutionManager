/// <reference path="../global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
}

RIMS.CourtCase = function() { };

RIMS.CourtCase.UpdateWorkflowStatus = function(courtCaseId) {

    var workFlowStatus = $("#workflow-status");
    workFlowStatus.slideUp('slow', function() {
        workFlowStatus.children().remove();
        workFlowStatus.append(RIMS.Global.GenerateLoadingMessageHtml("Refreshing Work Flow Status", "The work flow information is being refreshed, please be patient whilst this is happens."));
        workFlowStatus.slideDown('slow', function() {
            // make the ajax call to reload the data
            $.ajax({
                    type: "GET",
                    dataType: "html",
                    url: window.urlOffset + "/Prosecutions/CourtCase/WorkFlowStatus/" + courtCaseId + "?" + new Date().getTime(),
                    data: { },
                    success: function(response) {
                        workFlowStatus.slideUp('slow', function() {
                            workFlowStatus.html('');
                            workFlowStatus.html(response);
                            workFlowStatus.slideDown('slow');

                            RIMS.Global.AjaxHook({ result: true });
                        });
                    },
                    error: function(jqXhr, textStatus, errorThrown) {
                        // do something about the error
                    }
                });
        });
    });
};


RIMS.CourtCaseQuickEdit = function () { };

RIMS.CourtCaseQuickEdit.BeginRequest = function () {
    $("#page-loader").slideDown();
    $("#courtcase-quick-edit-pane").slideUp();
    $("#pfn-quick-error-pane").slideUp();
};

RIMS.CourtCaseQuickEdit.EndRequest = function () {
    setTimeout(RIMS.CourtCaseQuickEdit._EndRequest, 100);
};

RIMS.CourtCaseQuickEdit._EndRequest = function () {
    $("#courtcase-quick-edit-pane").find("input[id=aAddPayment]").click(function () {
        RIMS.Payment.Add($(this), $(this).attr("rel"), { type: $(this).attr("data-type") });
    });

    $("#courtcase-quick-edit-pane").find("input[name$=Voided]").click(function () {
        if ($(this).is(":checked")) {
            $("#courtcase-quick-edit-pane").find("div[name=VoidFields]").slideDown('slow');
        } else {
            $("#courtcase-quick-edit-pane").find("div[name=VoidFields]").slideUp('slow');
        }
    });

    if (RIMS.Appeal != 'undefined') {
        RIMS.Appeal.ConfigureDialog();
    }

    RIMS.Global.SetUpButtons();
    RIMS.Global.SetupWidgets();
    RIMS.WorkFlowValidate.SetupSteps();

    $("#page-loader").slideUp();
    $("#courtcase-quick-edit-pane").slideDown();

    // check to see if we are using History, if we are then push the state
    /*if (typeof $("#CourtCaseId").val() != 'undefined') {
        $.history.load($("#CourtCaseId").val());
    }*/
};

RIMS.CourtCaseQuickEdit.Error = function () {
    setTimeout(RIMS.CourtCaseQuickEdit._Error, 100);
};

RIMS.CourtCaseQuickEdit._Error = function () {
    $("#page-loader").slideUp();
    $("#courtcase-quick-edit-pane").slideUp();
    $("#pfn-quick-error-pane").slideDown();
};