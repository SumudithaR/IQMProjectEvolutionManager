/// <reference path="~/Scripts/Core/global.js" />
/// <reference path="~/Scripts/Core/dialog.js" />
/// <reference path="~/Scripts/libraries/jquery/jquery-1.6.2.js"/>
// make sure the namespace is setup
if (typeof ESTMAN == 'undefined') {
    ESTMAN = function() { };
}

/*
* method for address functionality
*/
ESTMAN.AjaxUtils = function() { };

/*
* Gets a list of items formated in json, the result of which is used to populate a table.
*/
ESTMAN.AjaxUtils.SelectToTable = function (caller, id, controller) {
    var options =
    {
        listUrl: '/' + controller + '/AjaxSelect/',
        selectUrl: '/' + controller + '/AjaxSelectedFor/',
    };

    var searchFunction = function () {
        var searchOptions =
        {
            url: options.listUrl,
            page: 1,
            create_link: function (item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.text);
            },
            select_result: function (item) {
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: options.selectUrl + id + "?selectedId=" + item.id + "&selectingFor=Table&" + new Date().getTime(),
                    data: {},
                    success: function (response) {
                        $("#" + controller + "DetailComplete").remove();
                        $("#" + controller + "List").append($(response).find("#" + controller + "DetailCompleteHtml tbody tr"));
                        //// Global.SetUpButtons();
                        return;
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        Global.RemoveLoadingIcon(caller);
                        Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
                    }
                });

                ESTMAN.Dialog.Close();
            },

        };

        ESTMAN.Dialog.LoadAjaxResults(searchOptions);
    };

    ESTMAN.Dialog.Prepare({
        title: controller + ' Search',
        defaultSearch: searchFunction
    });

    ESTMAN.Dialog.Open();
};

/*
* Opens the Partial view for adding a address to the ESTMAN.Inspector.
* @param inspectorId - id of the inspector objects
*/
ESTMAN.AjaxUtils.Add = function (caller, clientId, controller, inputOptions) {
    var options =
    {
        Url: "/" + controller + "/AjaxCreate/",
        returnObjId: controller
    };
    
    $.extend(options, inputOptions);
    Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: options.Url + clientId + "?returnObjId=" + options.returnObjId + "&" + new Date().getTime(),
        data: { },
        success: function(response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog();
            $("#partialViewDialog").dialog('option', 'title', 'Create ' + controller);

            Global.RemoveLoadingIcon(caller);
            ESTMAN.AjaxUtils.ConfigureDialog();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Opens the Partial view for editing a ESTMAN.Address.
* @param addressId - id of the address objects
*/
ESTMAN.AjaxUtils.Edit = function (caller, controller, id) {
    // load the content
    var resultDiv = $("#partialViewDialogData");
    Global.AddLoadingIcon(caller);

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlOffset + "/" + controller + "/AjaxEdit/" + id + "?" + new Date().getTime(),
        data: { },
        success: function(response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog('open');
            $("#partialViewDialog").dialog('option', 'title', 'Edit ' + controller);

            Global.RemoveLoadingIcon(caller);
            ESTMAN.AjaxUtils.ConfigureDialog();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Call back that is fired when a save attempt is made
*/
ESTMAN.AjaxUtils.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();
};

/*
* Callback that is fired after a save attempt has been made.
*/
ESTMAN.AjaxUtils.EndRequest = function (controller, returnObjId)
{
    setTimeout(function () {
        ESTMAN.AjaxUtils._EndRequest(controller, returnObjId);
    }, 100);
};

/*
* actual callback
*/
ESTMAN.AjaxUtils._EndRequest = function (controller, returnObjId) {
    if ($("#" + controller + "DetailComplete").length > 0) {
        var result = JSON.parse($("#" + controller + "DetailComplete").val());

        Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#" + controller + "DetailComplete").remove();
        if (result.success) {
            if (result.update)
            {
                // close the dialog, and append the contents to the list
                $("tr[name=" + controller + "_" + result.id + "]").html($("#" + controller + "DetailCompleteHtml"));
                $("#partialViewDialog").dialog('close');
                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                Global.SetUpButtons();
                return;
            } else if (result.deleted)
            {
                setTimeout(function() {
                    $("#" + returnObjId + "List").find("tr[id=" + controller + "_" + result.id + "]").fadeOut();
                }, 3000);
            } else
            {
                // close the dialog, and append the contents to the list
                $("#" + returnObjId + "List").append($("#" + controller + "DetailCompleteHtml tbody tr"));
                $("#partialViewDialog").dialog('close');
                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                $("#" + controller + "Message").remove();
                
                /// Global.SetUpButtons();
                return;
            }
        }
    }
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    ESTMAN.Address.ConfigureDialog();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};

/*
* Configures the Ajax dialog and then calls the base Global.ConfigureDialog.
*/
ESTMAN.AjaxUtils.ConfigureDialog = function ()
{
    Global.ConfigureDialog();
};