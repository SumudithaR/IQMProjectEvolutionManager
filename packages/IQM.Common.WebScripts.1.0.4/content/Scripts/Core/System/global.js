// make sure the namespace is setup
if (typeof Global == 'undefined') {
    Global = function () { };
};

if (typeof console == "undefined") var console = { log: function () { } };
else if (typeof console.log == "undefined") console.log = function () { };

//IE8 Date.now() fix
if (!Date.now) {
    Date.now = function () {
        return new Date().valueOf();
    };
}

$(document).ready(function () {
    Global.SetupButtons();

    // turn off autocomplete for everything, EVERYTHING!
    $('input:input').attr('autocomplete', 'off');
    $('input:input').focus(function () {
        $(this).attr('autocomplete', 'off');
    });

    Global.LoadZoom();
});

// create the console.log method if not defined
if (typeof console == "undefined") var console = { log: function () { } };
else if (typeof console.log == "undefined") console.log = function () { };

// fix the stupid formatting bug
/*$.validator.methods.dateISO = function (value, element) {
    return this.optional(element) || window.Globalize.parseDate(value, "d/M/yyyy") !== null;
};*/

/*
* Used to write out to the javascript console.
*/
Global.Log = function (obj) {
    console.log(obj);
};

/*
* Setup buttons
*/
Global.SetupButtons = function () {

    //// Global.SetupToolTips();

    $("[rel=button]").button();
    $("[rel=button]").attr("rel", "");
    $(":button,:submit").button();
};

/*
* Checks the date format and returns a strsing value 
* of the date in the ISO for if it is not already.
* Assumes the input is dd/mm/yyyy
* @param date - string value of the date
*/
Global.ParseDate = function (date) {
    var parts = date.split('/');
    if (parts[0].length == 4) {
        // everything is find return the input
        return date;
    } else {
        // re-order the string and return
        return parts[2] + '/' + parts[1] + '/' + parts[0];
    }
};

/*
* returns the date in an ISO Date String
*/
Global.ISODateString = function (d) {
    function pad(n) { return n < 10 ? '0' + n : n };
    return d.getUTCFullYear() + '-'
        + pad(d.getUTCMonth() + 1) + '-'
            + pad(d.getUTCDate()) + 'T'
                + pad(d.getUTCHours()) + ':'
                    + pad(d.getUTCMinutes()) + ':'
                        + pad(d.getUTCSeconds()) + 'Z';
};

/*
* Sets up a string as Title Case
*/
Global.ToTitleCase = function (str) {
    return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
};

/*
* Sets up the tool tips
*/
Global.SetupToolTips = function () {
    $("a[rel='tooltip']").each(function () {
        $(this).bt($(this).attr('title'),
            {
                fill: '#FFF',
                cornerRadius: 10,
                strokeWidth: 0,
                shadow: true,
                shadowOffsetX: 3,
                shadowOffsetY: 3,
                shadowBlur: 8,
                shadowColor: 'rgba(0,0,0,.9)',
                shadowOverlap: false,
                noShadowOpts: { strokeStyle: '#999', strokeWidth: 2 },
                positions: ['right', 'top']
            });
        $(this).attr("rel", "");
    });
};

/*
* adds a loading icon after the caller object
*/
Global.AddLoadingIcon = function (caller, replace) {
    replace = replace == null ? false : replace;
    if (caller != null) {
        if (replace) {
            _replacedClass = $(caller).attr("class");

            $(caller)
                .attr("rel", "loading")
                .toggleClass("loading-content-replace")
                .toggleClass(_replacedClass);
        } else {
            $(caller).find("span").append(
            $('<span>&nbsp;</span>')
                .attr("rel", "loading")
                .addClass("loading-content"));
        }
    }
};

/*
* remove the loading icon
*/
Global.RemoveLoadingIcon = function (caller, replace) {
    replace = replace == null ? false : replace;
    var loadingIcon;
    if (caller != null) {
        if (replace) {
            loadingIcon = $(caller);
            if (loadingIcon.attr("rel") == "loading") {
                loadingIcon
                    .toggleClass("loading-content-replace")
                    .toggleClass(_replacedClass);
                _replacedClass = null;
            }
        } else {
            $(caller).find("span[rel='loading']").remove();
            /*if (loadingIcon.attr("rel") == "loading") {
                loadingIcon.toggleClass("loading-content");
            }*/
        }
    }
};

/*
* Configure the dialog
*/
Global.ConfigureDialog = function ()
{
    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });

    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    //// Global.SetupValidation();
    Global.SetupButtons();
    Global.SetupDates();
};

/*
* displays an error message
*/
Global.AddErrorMessage = function (caller, jqXhr, textStatus, errorThrown) {
    if (caller != null) {
        var errorMessage = $("<span><span>")
            .addClass("error")
            .text(jqXhr.status + ": " + jqXhr.statusText)
            .insertAfter(caller);

        setTimeout(function () {
            $(errorMessage).fadeOut('slow', function () {
                $(this).remove();
            });
        }, 3000);
    }
};

/*
* adds a custom error message
*/
Global.AddCustomErrorMessage = function (caller, errorMessage) {
    if (caller != null) {
        var errorMessageObject = $("<span><span>")
            .addClass("error")
            .text(errorMessage)
            .insertAfter(caller);

        setTimeout(function () {
            $(errorMessageObject).fadeOut('slow', function () {
                $(this).remove();
            });
        }, 3000);
    }
};

/*
* create and open the dialog to view the barrier train work comment
*/
Global.ViewComment = function (hiddenField) {
    var commentControl = $("#" + hiddenField);

    var id = new Date().getTime();
    var commentDialog = $("<div>");
    commentDialog.html('<textarea name="comment' + id + '" style="width: 100%;height: 100%" readonly="readonly"></textarea>');
    commentDialog.attr("id", id);
    $("<body>").append(commentDialog);

    commentDialog.find("textarea[name=comment" + id + "]").val(commentControl.val());

    $(commentDialog).dialog({
        title: 'View Comment',
        autoOpen: false,
        width: 400,
        height: 300,
        modal: false,
        buttons: {
            Close: function () {
                $(commentDialog).dialog('close');
                $(commentDialog).remove();
            }
        }
    });
    $(commentDialog).dialog('open');
};

/*
* Used to geneartea a dialog that displays a loading message.
*/
Global.GenerateLoadingMessageHtml = function (title, message) {
    var loadingMessage = $("<div>");
    loadingMessage.append(
        $("<h3>").text(title)
    );
    loadingMessage.append(
        $("<img>").attr("alt", "Loading...").attr("src", window.urlOffset + "/Content/Images/Core/System/ajax-loader.gif")
    );
    loadingMessage.append(
        $("<p>").text(message)
    );
    return loadingMessage;
};

/*
* Used to open a dialog with a date picker control
*/
Global.DatePicker = function (control, inputOptions) {
    var options = {
        showOn: 'button',
        buttonImage: window["urlOffset"] + '/Content/Images/22x22/calendar.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        firstDay: window.firstDay,
        dateFormat: window.dateformat,
        formatOnly: false
    };
    $.extend(options, inputOptions);
    if (!options.formatOnly) {
        $(control).datepicker(options);
    }
    $(control).blur(function () {
        var oldValue = $(this).val();
        $(this).val($(this).val().replace(/\//g, ''));

        if ($(this).val().length == 6) {
            var yearStart = "20";
            // year check code will need to be updated in 9 years
            if (parseInt($(this).val().substring(4, 6)) > 20) {
                yearStart = "19";
            }
            $(this).val(
                $(this).val().substring(0, 2) + "/" + $(this).val().substring(2, 4) + "/" + yearStart + $(this).val().substring(4, 6)
            );
        } else if ($(this).val().length == 8) {
            $(this).val(
                $(this).val().substring(0, 2) + "/" + $(this).val().substring(2, 4) + "/" + $(this).val().substring(4, 8)
            );
        }
        else {
            $(this).val(oldValue);
        }
    });
};

/*
* Used to open a dialog with a date and time picker control
*/
Global.DateTimePicker = function (control, inputOptions) {

    var options = {
        showOn: 'button',
        buttonImage: '/Content/Images/22x22/calendar.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        firstDay: window.firstDay,
        dateFormat: window.dateformat,
        stepMinute: 1,
        formatOnly: false
    };
    $.extend(options, inputOptions);
    if (!options.formatOnly) {
        $(control).datetimepicker(options);
    }
    $(control).blur(function () {
        var oldValue = $(this).val();
        var dateTime = $(this).val().replace(/\//g, '').replace(/ /, '').replace(/:/, '');

        // 1412111030 14/12/11 10:30
        // 141212011030 14/12/2011 10:30
        if (dateTime.length == 10) {
            var yearStart = "20";
            // year check code will need to be updated in 9 years
            if (parseInt(dateTime.substring(4, 6)) > 20) {
                yearStart = "19";
            }
            $(this).val(
                dateTime.substring(0, 2) + "/" + dateTime.substring(2, 4) + "/" + yearStart + dateTime.substring(4, 6) + ' ' + dateTime.substring(6, 8) + ':' + dateTime.substring(8, 10)
            );
        } else if (dateTime.length == 12) {
            $(this).val(
                dateTime.substring(0, 2) + "/" + dateTime.substring(2, 4) + "/" + dateTime.substring(4, 8) + ' ' + dateTime.substring(8, 10) + ':' + dateTime.substr(10, 12)
            );
        }

        else {
            $(this).val(oldValue);
        }
    });
};

/*
* Used to open a dialog with a time picker control
*/
Global.TimePicker = function (control, inputOptions) {
    /*
        var options = {
            showOn: 'button',
            buttonImage: '/Content/Images/22x22/clock.png',
            buttonImageOnly: true,
            firstDay: window.firstDay,
            dateFormat: window.timeformat,
            stepMinute: 1,
            formatOnly: false
        };
        $.extend(options, inputOptions);
        if (!options.formatOnly) {
            $(control).timepicker(options);
        }
    */

    $(control).blur(function () {
        if ($(this).val().length == 3) {
            $(this).val(
                "0" + $(this).val().substring(0, 1) + ":" + $(this).val().substring(1, 3)
            );
        }
        else if ($(this).val().length == 4) {
            $(this).val(
                $(this).val().substring(0, 2) + ":" + $(this).val().substring(2, 4)
            );
        }
    });
};

/*
* Sets up the GUI widgets (collapsables, and tabs)
*/
Global.SetupWidgets = function () {
    // Setup the tab widgets
    $("div[rel=tab-widget]").tabs();

    // Setup the collapsable widgets
    $("div[rel=widget]")
        .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
        .css("display", "block")
        .css({ 'padding': '5px', 'margin': '10px' });

    $("div[rel=widget] > h3")
        .addClass("ui-widget-header ui-corner-all")
        .css({ 'margin-top': 0, 'padding': '5px', 'margin-bottom': '10px' }).each(function () {
            if ($(this).parent().find("div[rel=content]").css("display") == "none") {
                $(this).prepend($("<span class='ui-icon ui-icon-plusthick'></span>").css("float", "right"));
            }
            else {
                $(this).prepend($("<span class='ui-icon ui-icon-minusthick'></span>").css("float", "right"));
            }
        });

    $("div[rel=widget] > h3 .ui-icon").click(function () {
        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $(this).parent().parent().find("div[rel=content]").first().toggle();
    });

    $("div[rel=widget]").attr("rel", "");
};

/*
* Sets up the GUI dates
*/
Global.SetupDates = function () {
    /*var options = {
        showOn: 'button',
        buttonImage: '/Content/Images/22x22/calendar.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        formatOnly: false
    };
    
    $(".date").datepicker(options);*/
    $("input[type=date]").each(function () {
        $(this).datepicker({ dateFormat: 'dd/mm/yy' });
    });
};

Global.LoadZoom = function () {
    var size = Global.ReadCookie("fontsize");
    if (size != null) {
        $('html, body, table, input, textarea, select').css('font-size', parseFloat(size, 10));
    }
};

Global.ZoomIn = function () {
    var currentFontSize = $('html').css('font-size');
    var currentFontSizeNum = parseFloat(currentFontSize, 10);
    var newFontSize = currentFontSizeNum * 1.1;
    $('html, body, table, input, textarea, select').css('font-size', parseFloat(newFontSize, 10));
    Global.CreateCookie("fontsize", newFontSize, 365);
};

Global.ZoomOut = function () {
    var currentFontSize = $('html').css('font-size');
    var currentFontSizeNum = parseFloat(currentFontSize, 10);
    var newFontSize = currentFontSizeNum * 0.9;
    $('html, body, table, input, textarea, select').css('font-size', parseFloat(newFontSize, 10));
    Global.CreateCookie("fontsize", newFontSize, 365);
};

Global.ZoomReset = function () {
    $('html, body, table, input, textarea, select').css('font-size', '');
    Global.EraseCookie("fontsize");
};

Global.CreateCookie = function (name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }

    document.cookie = name + "=" + value + expires + "; path=/";
};

Global.ReadCookie = function (name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEq) == 0)
            return c.substring(nameEq.length, c.length);
    }
    return null;
};

Global.EraseCookie = function (name) {
    Global.CreateCookie(name, "", -1);
};

/*
* Sets up the array of hooks for access by the hook calls.
*/
Global.Hooks = new Array();

Global.AjaxHook = function (result) {
    for (var i in Global.Hooks) {
        var callBack = Global.Hooks[i];
        // check to see if we have been passes a string that needs evaling
        if (typeof callBack == "string") {
            var callBackString = callBack;
            callBack = function () { eval(callBackString); };
        } else {
            // make sure we have a callbackable paramter
            callBack = typeof callBack == "function" ? callBack : function () { };
        }
        
        // call the callback
        callBack.apply(this, Array(result));
    }
};

Global.RegisterAjaxHookWithCallback = function(hook, callback) {
    Global.Hooks.push(hook);
    eval(callback);
};

Global.RegisterAjaxHook = function (hook) {
    Global.Hooks.push(hook);
};

Global.AjaxFormError = function (ajaxContext) {

    var errorMessage = $("<div>");
    errorMessage.append("<h3>An Unknown Error Has Occurred</h3>");
    errorMessage.append("<p>An error has occurred whilst performing the action, please review the error message below and try again.</p>");
    var errorList = $("<ul>");
    errorMessage.append(errorList);
    if (ajaxContext != null) {
        var response = ajaxContext.get_response();
        if (response != null) {
            var statusCode = response.get_statusCode();
            var statusText = response.get_statusText();
            errorList.append('<li class="error">' + statusCode + ' - ' + statusText + '</li>');

            var responseData = response.get_responseData();
            $("<div>").html(responseData).find("title").each(function () {
                errorList.append('<li class="error">' + $(this).text() + '</li>');
            });
        }
    }

    $("#partialViewDialogData").append(errorMessage);
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();
};

Global.SetupValidation = function () {
    if (typeof jQuery.validator != 'undefined') {
        jQuery.validator.unobtrusive.parse();
    }
    else {
        window.Sys.Mvc.FormContext._Application_Load();
    }
};

/*
* Used to view the audit information of a data item.
*/
Global.ViewAudit = function (caller, createdBy, created, editedBy, edited, auditString) {
    var id = new Date().getTime();
    var auditDialog = $("<div>");
    auditDialog.attr("id", id);
    Global.AddLoadingIcon(caller);

    var html = '<div class="MockForm">' +
        '<p><label>Created By</label> ' + createdBy + '</p>' +
            '<p><label>Created On</label> ' + created + '</p>' +
                '<p><label>Edited By</label> ' + editedBy + '</p>' +
                    '<p><label>Edited On</label> ' + edited + '</p>' +
                        '<p><label>Data</label><textarea class="text-box multi-line" style="width: 100%"> ' + auditString.replace(/,/g, '\n') + '</textarea></p>' +
                        '</div>';
    // create the help dialog
    $(auditDialog).html(html);
    $("<body>").append(auditDialog);
    $(auditDialog).dialog({
        title: 'Audit Information',
        autoOpen: false,
        width: 400,
        modal: false,
        resizable: false,
        buttons: {
            Close: function () {
                $(auditDialog).dialog('close');
                $(auditDialog).remove();
            }
        }
    });
    $(auditDialog).dialog('open');
    Global.RemoveLoadingIcon(caller);
};

/*
* Used to create a delete confirmation window.
*/
Global.Delete = function (action) {
    if (confirm("Are you sure you wish to delete this item?")) {
        window.location.href = action;
    }
};

Global.ZeroPad = function (number, length) {
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }

    return str;
}

/*
* Get the application install folder.
*/
Global.GetInstallFolder = function () {
    return _appInstallFolder;
};

/*
* Set the application install folder.
*/
Global.SetInstallFolder = function (value) {
    _appInstallFolder = value;
};

/*
* The application install folder.
*/
var _appInstallFolder = "";

/*
* Used to restore the replaced class(es) when doing a LoadingIcon replacement
*/
var _replacedClass = null;