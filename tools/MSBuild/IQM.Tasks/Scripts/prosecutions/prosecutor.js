/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

RIMS.Prosecutor = function () { }

/*
* method to convert a string of a uk date to a date object
* @param sUK - Input string of the date to be converted to UK Format.
*/
RIMS.Prosecutor._dateFromUKFormat = function (sUK) {
    var A = sUK.split(/[\\\/]/);
    A = [A[1], A[0], A[2]];
    return new Date(Date.parse(A.join('/')));
}

/*
* Opens dialog to search for depots 
*/
RIMS.Prosecutor.Search = function (caller, courtCaseId, inputOptions) {

    var Options = { 'label': '#ProsecutorLabel', 'value': '#Prosecutor' };
    $.extend(Options, inputOptions);

    RIMS.Dialog.Prepare();
    $("#ajaxDialog").dialog('option', 'title', 'Prosecutor Search');
    RIMS.Dialog.SetupHelp($("#ajaxDialog"), 'Shared', 'prosecutor-search-dialog');
    $("#dialogButton").click(function () {
        var options = {
            url: urlOffset + '/Inspector/AjaxProsecutorList/',
            page: 0,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function (item) {
                $(Options.label).val(item.Id)
                $(Options.label).text(item.Text)
                $("#ajaxDialog").dialog('close');
                RIMS.Prosecutor.Set(caller, courtCaseId, item.Id);
            }
        };
        RIMS.Dialog.LoadAjaxResults(options);
    });
    $("#ajaxDialog").dialog('open');
}

RIMS.Prosecutor.Set = function (caller, courtCaseId, prosecutorId) {
    RIMS.Global.AddLoadingIcon(caller);
    // make the ajax call to assign the prosecutor

    var postData = {
        "courtCaseId": courtCaseId,
        "prosecutorId": prosecutorId
    };
    var ajax_url = urlOffset + '/Prosecutions/CourtCase/SetProsecutor';
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