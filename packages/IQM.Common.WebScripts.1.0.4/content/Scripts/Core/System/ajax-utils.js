/// <reference path="~/Scripts/Core/System/global.js" />
/// <reference path="~/Scripts/libraries/jquery/jquery-2.0.3.js"/>

// make sure the namespace is setup
if (typeof AjaxUtils == 'undefined') {
    AjaxUtils = function () {
    };
}

/*
* Gets a list of items formated in json, the result of which is used to populate a table.
*/
AjaxUtils.SelectToTable = function(caller, id, controller) {
    var options =
    {
        listUrl: '/' + controller + '/AjaxSelect/',
        selectUrl: '/' + controller + '/AjaxSelectedFor/'
    };

    var searchFunction = function() {
        var searchOptions =
        {
            url: options.listUrl,
            page: 1,
            create_link: function(item) {
                return $("<a>").attr("href", "javascript: ;;").text(item.text);
            },
            select_result: function(item) {
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: options.selectUrl + id + "?selectedId=" + item.id + "&selectingFor=Table&" + new Date().getTime(),
                    data: {},
                    success: function(response) {
                        $("#" + controller + "DetailComplete").remove();
                        $("#" + controller + "List").append($(response).find("#" + controller + "DetailCompleteHtml tbody tr"));
                        //// Global.SetUpButtons();
                        return;
                    },
                    error: function(jqXHR, textStatus, errorThrown) {
                        Global.RemoveLoadingIcon(caller);
                        Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
                    }
                });

                Dialog.Close();
            }
        };

        Dialog.LoadAjaxResults(searchOptions);
    };

    Dialog.Prepare({
        title: controller + ' Search',
        defaultSearch: searchFunction
    });

    Dialog.Open();
};

/*
* Opens the Partial view for adding an object.
*/
AjaxUtils.Add = function (caller, clientId, controller, inputOptions, dialogOptions) {
    var options =
    {
        ajaxAction: "AjaxCreate",
        returnObjId: controller,
    };

    var dialogOpts =
    {
        title: 'Create ' + controller,
    };
    $.extend(dialogOpts, _defaultDialogOpts);
    $.extend(dialogOpts, dialogOptions);

    $.extend(options, inputOptions);
    
    Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");
    var urlToExecute = options.parentType == null ? "/" + controller + "/" + options.ajaxAction + "/" + clientId + "?returnObjId=" + options.returnObjId : "/" + controller + "/" + options.ajaxAction + "/" + clientId + "?returnObjId=" + options.returnObjId + "&parentType=" + options.parentType;

    $.ajax({
        type: "GET",
        dataType: "html",
        url: urlToExecute + "&" + new Date().getTime(),
        data: {},
        success: function(response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog(dialogOpts);

            Global.RemoveLoadingIcon(caller);
            AjaxUtils.ConfigureDialog();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Opens the Partial view for editing an object.
*/
AjaxUtils.Edit = function(caller, clientId, controller, inputOptions, dialogOptions) {
    var options =
    {
        Url: "/" + controller + "/AjaxEdit/",
        returnObjId: controller
    };

    var dialogOpts =
    {
        title: 'Edit ' + controller
    };
    $.extend(dialogOpts, _defaultDialogOpts);
    $.extend(dialogOpts, dialogOptions);
    
    $.extend(options, inputOptions);
    Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: options.Url + clientId + "?returnObjId=" + options.returnObjId + "&" + new Date().getTime(),
        data: {},
        success: function(response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog(dialogOpts);

            Global.RemoveLoadingIcon(caller);
            AjaxUtils.ConfigureDialog();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Opens the Partial view for deleting an object.
*/
AjaxUtils.Delete = function (caller, clientId, controller, inputOptions, dialogOptions) {
    var options =
    {
        Url: "/" + controller + "/AjaxDelete/",
        returnObjId: controller
    };

    var dialogOpts =
    {
        title: 'Delete ' + controller
    };
    $.extend(dialogOpts, _defaultDialogOpts);
    $.extend(dialogOpts, dialogOptions);
    
    $.extend(options, inputOptions);
    Global.AddLoadingIcon(caller);

    // load the content
    var resultDiv = $("#partialViewDialogData");

    $.ajax({
        type: "GET",
        dataType: "html",
        url: options.Url + clientId + "?returnObjId=" + options.returnObjId + "&" + new Date().getTime(),
        data: {},
        success: function(response) {
            resultDiv.html('');
            resultDiv.html(response);
            $("#partialViewDialog").dialog(dialogOpts);

            Global.RemoveLoadingIcon(caller);
            AjaxUtils.ConfigureDialog();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Lazily loads data from the selection of one object into another, using the action specified on the controller.
*/
AjaxUtils.LazyLoadSelect = function (caller, controller, action, depostResults) {
    var selectedItem = $("#" + caller + " option:selected").val();
    caller = $("#" + caller);
    Global.AddLoadingIcon(caller);
    $.ajax({
        type: "GET",
        dataType: "html",
        url: "/" + controller + "/" + action + "?id=" + selectedItem + "&" + new Date().getTime(),
        data: {},
        success: function (response) {
            $("#" + depostResults).empty();
            if (response.length > 0) {
                $("#" + depostResults).append(response);
                $("#" + depostResults).show();
            } else {
                $("#" + depostResults).append("<option value='0'>No Results.</option>");
                $("#" + depostResults).hide();
            }

            Global.RemoveLoadingIcon(caller);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            Global.RemoveLoadingIcon(caller);
            Global.AddErrorMessage(caller, jqXHR, textStatus, errorThrown);
        }
    });
};

/*
* Call back that is fired when a save attempt is made
*/
AjaxUtils.BeginRequest = function () {
    $("#partialViewDialogData").hide();
    $("#partialViewDialogLoading").show();
};

/*
* Callback that is fired after a save attempt has been made.
*/
AjaxUtils.EndRequest = function(controller, returnObjId) {
    setTimeout(function() {
        AjaxUtils._EndRequest(controller, returnObjId);
    }, 100);
};

/*
* actual callback
*/
AjaxUtils._EndRequest = function(controller, returnObjId) {
    if ($("#" + controller + "DetailComplete").length > 0) {
        var result = JSON.parse($("#" + controller + "DetailComplete").val());

        Global.AjaxHook(result);

        // remove the element from the dom as not to cause conflicts later.
        $("#" + controller + "DetailComplete").remove();
        if (result.success) {
            if (result.update) {
                // close the dialog, and append the contents to the list
                $("tr[name=" + controller + "_" + result.id + "]").html($("#" + controller + "DetailCompleteHtml tr").html());
                $("#partialViewDialog").dialog('close');
                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                Global.SetupButtons();
                return;
            } else if (result.deleted) {
                $("#partialViewDialog").dialog('close');
                setTimeout(function() {
                    $("#" + returnObjId + "List").find("tr[id=" + controller + "_" + result.id + "]").fadeOut();
                    setTimeout(function () {
                        $("#" + returnObjId + "List").find("tr[id=" + controller + "_" + result.id + "]").remove();
                    }, 500);
                }, 500);
            } else if (result.deleted) {

            } else {
                // close the dialog, and append the contents to the list
                $("#" + returnObjId + "List").append($("#" + controller + "DetailCompleteHtml tbody tr"));
                $("#partialViewDialog").dialog('close');
                $("#partialViewDialogData").show();
                $("#partialViewDialogLoading").hide();
                $("#" + controller + "Message").remove();
                Global.SetupButtons();
                return;
            }
            
            
        }
    }
    
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function() {
        $("#partialViewDialog").dialog('close');
    });
};

/*
* Configures the Ajax dialog and then calls the base Global.ConfigureDialog.
*/
AjaxUtils.ConfigureDialog = function() {
    Global.ConfigureDialog();
    //// ControlUtils.ConfigureDialog();
};

/*
* The default dialog options for the ajax dialogs.
*/
var _defaultDialogOpts =
{
    open: function (event, ui) {
        $(".ui-dialog-titlebar-close").hide();
    },
    resizable: false,
    modal: true,
    width: 450,
    closeOnEscape: true
};