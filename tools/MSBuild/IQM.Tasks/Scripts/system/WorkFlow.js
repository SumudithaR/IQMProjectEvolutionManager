/// <reference path="../Global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />

/*
* Work Flow Functions
*/
RIMS.WorkFlow = function() { };

/*
* Begin Work Flow Request
*/
RIMS.WorkFlow.Begin = function() {
    // hide the process div
    $("#process-workflow-detail").slideUp("slow");

    // show the executing details
    $("#process-workflow-execute").slideDown("slow");
};

RIMS.WorkFlow.ReloadOverview = function(courtCaseId) {
    $.ajax({
        url: '/Prosecutions/CourtCase/WorkFlow/' + courtCaseId,
        dataType: "html",
        success: function (response) {
            var resultDiv = $("#workflow-pane");
            resultDiv.html('');
            resultDiv.html(response);
            RIMS.Global.SetupWidgets();
            RIMS.Global.SetUpButtons();
        }
    });
};

/*
* End Work Flow Request
*/
RIMS.WorkFlow.End = function() {
    setTimeout("RIMS.WorkFlow._End()", 1000);
};

RIMS.WorkFlow._End = function() {
    $("#process-workflow-execute").slideUp("slow", function() {
        $("#ExecuteResponse").slideDown("slow");
    });
    RIMS.Global.AjaxHook({ result: true });
    RIMS.Global.SetUpButtons();
};

RIMS.WorkFlow.Error = function() {
    $("#process-workflow-execute").slideUp("slow", function() {
        $("#ErrorResponse").slideDown("slow");
    });
};

RIMS.WorkFlowValidate = function() { };

RIMS.WorkFlowValidate.Begin = function(detailId, resultsId, loadingId) {
    // hide previous results
    $("#" + resultsId).slideUp("slow");

    // hide the process div
    $("#" + detailId).slideUp("slow");

    // show the executing details
    $("#" + loadingId).slideDown("slow");
};

RIMS.WorkFlowValidate.End = function(detailId, resultsId, loadingId) {
    setTimeout("RIMS.WorkFlowValidate._End('" + detailId + "', '" + resultsId + "', '" + loadingId + "')", 1000);
};

RIMS.WorkFlowValidate._End = function(detailId, resultsId, loadingId) {
    $("#" + loadingId).slideUp("slow", function() {
        $("#" + resultsId).slideDown("slow");
        $("#" + detailId).slideDown("slow");
    });
    RIMS.Global.AjaxHook({ result: true });
    RIMS.Global.SetUpButtons();

    RIMS.Global.Log(resultsId);

    $("#" + resultsId).find("input[name=select-all]").click(function() {
        if ($(this).is(":checked")) {
            $("#" + resultsId).find("input[name=courtCases]:visible").attr('checked', 'checked');
        } else {
            $("#" + resultsId).find("input[name=courtCases]:visible").removeAttr('checked');
        }
    });

    // Setup the table filtering.
    $("[class='filter-table']").each(function() {
        RIMS.TableFilter(this);
        $(this).removeClass("filter-table");
    });
};

RIMS.WorkFlowValidate.Error = function(detailId, loadingId) {
    setTimeout(function() {
        RIMS.WorkFlowValidate._End(detailId, loadingId);
        RIMS.Global.AddCustomErrorMessage($("#" + detailId), "An error has occurred that has preventing items from correctly being loaded.");
    }, 1000);
};

RIMS.WorkFlowValidate.SetupSteps = function() {
    $("#toggleStatus@(Model.CourtCaseId)").click(function() {
        if ($(this).is(":checked")) {
            $("#Status@(Model.CourtCaseId)").slideDown('slow');
        } else {
            $("#Status@(Model.CourtCaseId)").slideUp('slow');
        }
    });

    $("textarea[name=Notes]").alphanumeric({ ichars: ',' });

    RIMS.Global.DatePicker($("input[name$=SendResultsLetter]"));
    RIMS.Global.DatePicker($("input[name$=DateToPay]"));

    $("div[rel=enter-court-result]").find("select[name=ResultType]").change(function() {
        for (var i in Window.CourtCaseResults) {
            if (Window.CourtCaseResults[i].DropDownItemId == $(this).val() && Window.CourtCaseResults[i].MetaData == "A") {
                $(this).parent().parent().find("div[rel=assign-new-court-date]").slideDown();
                $(this).parent().parent().find("div[rel=court-result-details]").slideUp();
            } else if (Window.CourtCaseResults[i].DropDownItemId == $(this).val() && Window.CourtCaseResults[i].MetaData != "A") {
                $(this).parent().parent().find("div[rel=assign-new-court-date]").slideUp();
                $(this).parent().parent().find("div[rel=court-result-details]").slideDown();
            }
        }
    });
};