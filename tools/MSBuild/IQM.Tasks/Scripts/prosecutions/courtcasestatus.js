/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

RIMS.CourtCaseStatus = function () { }

/*
* method to convert a string of a uk date to a date object
* @param sUK - Input string of the date to be converted to UK Format.
*/
RIMS.CourtCaseStatus._dateFromUKFormat = function (sUK) {
    var A = sUK.split(/[\\\/]/);
    A = [A[1], A[0], A[2]];
    return new Date(Date.parse(A.join('/')));
}

/*
* Opens dialog to search for depots 
*/
RIMS.CourtCaseStatus.Search = function(caller, courtCaseId, inputOptions) {

    var options = { 'label': '#CourtCaseStatusLabel', 'value': '#CourtCaseStatus', 'set': true };
    $.extend(options, inputOptions);

    RIMS.Dialog.Prepare();
    $("#ajaxDialog").dialog('option', 'title', 'CourtCaseStatus Search');
    RIMS.Dialog.SetupHelp($("#ajaxDialog"), 'Shared', 'courtCaseStatus-search-dialog');
    $("#dialogButton").click(function() {
        var resultOptions = {
            url: urlOffset + '/Prosecutions/CourtCase/AjaxCourtCaseStatusList/',
            page: 0,
            create_link: function(item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function(item) {
                $(options.value).val(item.Id);
                $(options.label).text(item.Text);
                $("#ajaxDialog").dialog('close');
                if (options.set) {
                    RIMS.CourtCaseStatus.Set(caller, courtCaseId, item.Id);
                }
            }
        };
        RIMS.Dialog.LoadAjaxResults(resultOptions);
    });
    $("#ajaxDialog").dialog('open');
};

RIMS.CourtCaseStatus.Set = function (caller, courtCaseId, courtCaseStatusId) {
    RIMS.Global.AddLoadingIcon(caller);
    // make the ajax call to assign the courtCaseStatus

    var postData = {
        "courtCaseId": courtCaseId,
        "courtCaseStatusId": courtCaseStatusId
    };
    var ajax_url = urlOffset + '/Prosecutions/CourtCase/SetCourtCaseStatus';
    $.ajax({
        url: ajax_url,
        data: postData,
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            RIMS.Global.RemoveLoadingIcon(caller);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            RIMS.Global.RemoveLoadingIcon(caller);
            RIMS.Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
}