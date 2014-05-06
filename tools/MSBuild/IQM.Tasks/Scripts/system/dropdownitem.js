/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
/// <reference path="../RIMS.Global.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

RIMS.DropDownItem = function () { }

/*
* method to convert a string of a uk date to a date object
* @param sUK - Input string of the date to be converted to UK Format.
*/
RIMS.DropDownItem._dateFromUKFormat = function (sUK) {
    var A = sUK.split(/[\\\/]/);
    A = [A[1], A[0], A[2]];
    return new Date(Date.parse(A.join('/')));
}

/*
* Opens dialog to search for depots 
*/
RIMS.DropDownItem.Search = function (caller, inputOptions) {
    var Options = { LabelSelector: '#DropDownItemLabel', ValueSelector: '#DropDownItem', DropDownType: "PaymentLocation", Title: "DropDownItem Search" };
    $.extend(Options, inputOptions);

    RIMS.Dialog.Prepare();
    $("#ajaxDialog").dialog('option', 'title', Options.Title);
    RIMS.Dialog.SetupHelp($("#ajaxDialog"), 'Shared', 'dropDownItem-search-dialog');

    $("#dialogButton").click(function () {
        var options = {
            url: urlOffset + '/System/DropDown/AjaxList/',
            post_data: { dropDownType: Options.DropDownType },
            page: 0,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.Text);
            },
            select_result: function (item) {
                $(Options.ValueSelector).val(item.Id)
                $(Options.LabelSelector).text(item.Text)
                $("#ajaxDialog").dialog('close');
            }
        };
        RIMS.Dialog.LoadAjaxResults(options);
    });
    $("#ajaxDialog").dialog('open');
}

RIMS.DropDownItem.Clear = function (inputoptions) {
    var Options = {
        LabelSelector: "#DropDownItemLabel", // control to put the results into
        ValueSelector: "#DropDownItem"
    };
    $.extend(Options, inputoptions);
    $(Options.LabelSelector).text("Not Set");
    $(Options.ValueSelector).val('');
}

RIMS.DropDownItem.Remove = function (id) {
    if (confirm("Are you sure you wish to delete this item?")) {

        var postData = {
            "id": id
        };

        var ajax_url = urlOffset + '/DropDown/Remove';
        $.ajax({
            url: ajax_url,
            data: postData,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    // display success message
                    $("#dropList_" + id).fadeOut("slow");

                } else {
                    // display the failed message
                    alert("Item cannot be deleted because it is used elsewhere in the system. Set the visibility to off to hide it.");
                }
                $("#dialogLoadingMessage").dialog('close');
                $("#dialogCloneMessage").dialog('open');
            },
            error: function (data) {
                // display the failed message
                alert("an unknown error occurred whilst deleting the data, please try again");
            }
        });
    }
}