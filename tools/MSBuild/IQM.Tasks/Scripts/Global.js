/// <reference path="../libraries/jquery-1.7.1-vsdoc.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
};

/*
* Global Functions
*/
RIMS.Global = function() {
};

// create the console.log method if not defined
if (typeof console == "undefined") var console = { log: function() { } };
else if (typeof console.log == "undefined") console.log = function () { };

RIMS.Global.Log = function(obj) {
    console.log(obj);
};

/*
* Setup buttons
*/
RIMS.Global.SetUpButtons = function () {

    RIMS.Global.SetupToolTips();

    if ($.browser.msie && $.browser.version == "6.0") {
        return;
    }
    $("[rel=button]").button();
    $("[rel=button]").attr("rel", "");
    $(":button,:submit").button();

    // setup the help
    try {
        RIMS.Help.InitLinks();
    }
    catch(e)
    {
        // help javascript hasn't been included
    }
};

/*
* Checks the date format and returns a strsing value 
* of the date in the ISO for if it is not already.
* Assumes the input is dd/mm/yyyy
* @param date - string value of the date
*/
RIMS.Global.ParseDate = function(date) {
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
RIMS.Global.ISODateString = function (d) {
    function pad(n) { return n < 10 ? '0' + n : n };
    return d.getUTCFullYear() + '-'
        + pad(d.getUTCMonth() + 1) + '-'
            + pad(d.getUTCDate()) + 'T'
                + pad(d.getUTCHours()) + ':'
                    + pad(d.getUTCMinutes()) + ':'
                        + pad(d.getUTCSeconds()) + 'Z';
};

RIMS.Global.ToTitleCase = function(str) {
    return str.replace( /\w\S*/g , function(txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
};


$(document).ready(function () {
    RIMS.Global.SetUpButtons();
    //$("html, body, table, input, textarea, select").css("font-size","12px");

    // turn off autocomplete for everything, EVERYTHING!
    $('input:input').attr('autocomplete', 'off');
    $('input:input').live('focus', function () {
        $(this).attr('autocomplete', 'off');
    });

    RIMS.Global.LoadZoom();
});

RIMS.Global.SetupToolTips = function() {
    $("a[rel='tooltip']").each(function() {
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
RIMS.Global.AddLoadingIcon = function(caller) {
    if (caller != null) {
        $('<span>&nbsp;</span>')
            .attr("rel", "loading")
            .addClass("loading-content")
            .insertAfter($(caller));
    }
};

/*
* remove the loading icon
*/
RIMS.Global.RemoveLoadingIcon = function(caller) {
    if (caller != null) {
        var loadingIcon = $(caller).next();
        if (loadingIcon.attr("rel") == "loading") {
            loadingIcon.remove();
        }
    }
};

RIMS.Global.ConfigureDialog = function () {
    // setup the destination searches
    RIMS.Global.SetupDestinationFields();

    // set the close action on the cancel button
    $("#partialViewDialog").find("button[rel=Cancel]").click(function () {
        $("#partialViewDialog").dialog('close');
    });
    
    // as we are using client side validation we need to make
    // the manual call to reload the validation JSON
    RIMS.Global.SetupValidation();
};

/*
* displays an error message
*/
RIMS.Global.AddErrorMessage = function(caller, jqXhr, textStatus, errorThrown) {
    if (caller != null) {
        var errorMessage = $("<span><span>")
            .addClass("error")
            .text(jqXhr.status + ": " + jqXhr.statusText)
            .insertAfter(caller);

        setTimeout(function() {
            $(errorMessage).fadeOut('slow', function() {
                $(this).remove();
            });
        }, 3000);
    }
};

/*
* adds a custom error message
*/
RIMS.Global.AddCustomErrorMessage = function(caller, errorMessage) {
    if (caller != null) {
        var errorMessageObject = $("<span><span>")
            .addClass("error")
            .text(errorMessage)
            .insertAfter(caller);

        setTimeout(function() {
            $(errorMessageObject).fadeOut('slow', function() {
                $(this).remove();
            });
        }, 3000);
    }
};

RIMS.Global.SetupDestinationFields = function () {

    $("input[name*=Destination]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window["urlOffset"] + "/Destination/AjaxList",
                dataType: "json",
                data: {
                    filter: RIMS.Global.ToTitleCase(request.term)
                },
                success: function (data) {
                    response($.map(data.Results, function (item) {

                        if (window.DestinationByCode) {
                            return {
                                label: item.CRS + " - " + item.Name,
                                value: item.CRS
                            };
                        } else {
                            return {
                                label: item.Name,
                                value: item.Name
                            };
                        }
                    }));
                }
            });

        },
        minLength: window.DestinationByCode ? 1 : 3,
        select: function (event, ui) {
            /*log(ui.Name ? ("Selected: " + ui.item.label) : "Nothing selected, input was " + this.value);*/
            $(this).val(ui.item.value);
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    }).blur(function () {
        $(this).val(RIMS.Global.ToTitleCase($(this).val()));
    });
    try {
        RIMS.AutoFormatFields.SetupTime($("input[name*=WorkedTo]"));
    } catch (e) {
        console.log(e);
    }
    try {
        RIMS.AutoFormatFields.SetupTime($("input[name*=WorkedFrom]"));
    } catch (e) {
        console.log(e);
    }
};

/*
* create and open the dialog to view the barrier train work comment
*/
RIMS.Global.ViewComment = function (hiddenField) {
    var commentControl = $("#" + hiddenField);

    var controlId = new Date().getTime();
    var commentDialog = $("<div>");
    commentDialog.html('<textarea name="comment' + controlId + '" style="width: 100%;height: 100%" readonly="readonly"></textarea>');
    commentDialog.attr("id", controlId);
    $("<body>").append(commentDialog);

    commentDialog.find("textarea[name=comment" + controlId + "]").val(commentControl.val());

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

RIMS.Global.GenerateLoadingMessageHtml = function(title, message) {
    var loadingMessage = $("<div>");
    loadingMessage.append(
        $("<h3>").text(title)
    );
    loadingMessage.append(
        $("<img>").attr("alt", "Loading...").attr("src", window.urlOffset + "/Content/Images/sending_sms.gif")
    );
    loadingMessage.append(
        $("<p>").text(message)
    );
    return loadingMessage;
};


RIMS.Global.DatePicker = function (control, inputOptions) {
    var options = {
        showOn: 'button',
        buttonImage: window["urlOffset"] + '/Content/Images/22x22/calendar.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        firstDay: window.firstDay,
        dateFormat: window.dateformat
    };
    $.extend(options, inputOptions);

    $(control).datepicker(options);
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

RIMS.Global.DateTimePicker = function(control, inputOptions) {

    var options = {
        showOn: 'button',
        buttonImage: window["urlOffset"] + '/Content/Images/22x22/calendar.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        firstDay: window.firstDay,
        dateFormat: window.dateformat,
        stepMinute: 1
    };
    $.extend(options, inputOptions);

    $(control).datetimepicker(options);
};

RIMS.Global.TimePicker = function(control, inputOptions) {

    var options = {
        showOn: 'button',
        buttonImage: window["urlOffset"] + '/Content/Images/22x22/clock.png',
        buttonImageOnly: true,
        firstDay: window.firstDay,
        dateFormat: window.timeformat,
        stepMinute: 1
    };
    $.extend(options, inputOptions);

    $(control).timepicker(options);
};


/*
    <div rel="widget">
        <h3>Widget Title</h3>
        <div rel="content">
            widget body
        </div>
    <div>
*/

RIMS.Global.SetupWidgets = function () {

    if ($.browser.msie && $.browser.version == "6.0") {
        return;
    }

    $("div[rel=widget]")
        .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
        .css("display", "block")
        .css({ 'padding': '5px', 'margin': '10px' });

    $("div[rel=widget] > h3")
        .addClass("ui-widget-header ui-corner-all")
        .css({ 'margin-top': 0, 'padding': '5px', 'margin-bottom': '10px' }).each(function () {
            if ($(this).parent().find("div[rel=content]").css("display") == "none") {
                /*$(this).prepend($("<span class='ui-icon ui-icon-plusthick'></span>").css({
                    float: 'right'
                 }));*/
            }
            else {
               $(this).prepend($("<span class='ui-icon ui-icon-minusthick'></span>").css("float", 'right'));
            }
        });

    $("div[rel=widget] > h3 .ui-icon").click(function () {
        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $(this).parent().parent().find("div[rel=content]").first().toggle();
    });

    $("div[rel=widget]").attr("rel", "");
};

RIMS.Global.LoadZoom = function() {
    var size = RIMS.Global.ReadCookie("fontsize");
    if (size != null) {
        $('html, body, table, input, textarea, select').css('font-size', parseFloat(size, 10));
    }
};

RIMS.Global.ZoomIn = function() {
    var currentFontSize = $('html').css('font-size');
    var currentFontSizeNum = parseFloat(currentFontSize, 10);
    var newFontSize = currentFontSizeNum * 1.1;
    $('html, body, table, input, textarea, select').css('font-size', parseFloat(newFontSize, 10));
    RIMS.Global.CreateCookie("fontsize", newFontSize, 365);
};

RIMS.Global.ZoomOut = function() {
    var currentFontSize = $('html').css('font-size');
    var currentFontSizeNum = parseFloat(currentFontSize, 10);
    var newFontSize = currentFontSizeNum * 0.9;
    $('html, body, table, input, textarea, select').css('font-size', parseFloat(newFontSize, 10));
    RIMS.Global.CreateCookie("fontsize", newFontSize, 365);
};

RIMS.Global.ZoomReset = function() {
    $('html, body, table, input, textarea, select').css('font-size', '');
    RIMS.Global.EraseCookie("fontsize");
};


RIMS.Global.CreateCookie = function(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }

    document.cookie = name + "=" + value + expires + "; path=/";
};

RIMS.Global.ReadCookie = function(name) {
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

RIMS.Global.EraseCookie = function(name) {
    RIMS.Global.CreateCookie(name, "", -1);
};

RIMS.Global.Hooks = new Array();

RIMS.Global.AjaxHook = function (result) {
    for (var i in RIMS.Global.Hooks) {
        var callBack = RIMS.Global.Hooks[i];
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

RIMS.Global.RegisterAjaxHook = function(hook) {
    RIMS.Global.Hooks.push(hook);
};

RIMS.Global.AjaxFormError = function (ajaxContext) {

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
            $("<div>").html(responseData).find("title").each(function() {
                errorList.append('<li class="error">' + $(this).text() + '</li>');
            });
        }
    }
    
    $("#partialViewDialogData").append(errorMessage);
    $("#partialViewDialogData").show();
    $("#partialViewDialogLoading").hide();
};

RIMS.Global.SetupValidation = function() {
    if (typeof jQuery.validator != 'undefined') {
        jQuery.validator.unobtrusive.parse();
    }
    else {
        Sys.Mvc.FormContext._Application_Load();
    }
};

RIMS.Global.ViewAudit = function (createdBy, created, editedBy, edited) {
    var controlId = new Date().getTime();
    var auditDialog = $("<div>");
    auditDialog.attr("id", controlId);

    var html = '<div class="MockForm">' +
        '<p><label>Created By</label> ' + createdBy + '</p>' +
            '<p><label>Created On</label> ' + created + '</p>' +
                '<p><label>Edited By</label> ' + editedBy + '</p>' +
                    '<p><label>Edited On</label> ' + edited + '</p>' +
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
};