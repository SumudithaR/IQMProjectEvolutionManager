/// <reference path="../Global.js" />
/// <reference path="../../libraries/jquery-1.7.1-vsdoc.js" />
// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
};

$(document).ready(function () {
    $("a[rel=init_period]").click(function () {
        RIMS.Period.Init($(this).parents("div:eq(0)").attr("rel"));
    });
    RIMS.Global.DatePicker($("input[name$=Date]"));
    RIMS.Global.DatePicker($("#Start"));
    RIMS.Global.DatePicker($("#End"));
    RIMS.Global.DatePicker($("input[name^=Week]"));
});

/*
* Wrapper for period functions
*/
RIMS.Period = function () { };

RIMS.Period.cache = new Array();

/*
* loads the periods into the period detail
* @param options - options for loading period details
*/
RIMS.Period.LoadAjaxResults = function(options) {
    var key = options.url + options.page;
    if (typeof(RIMS.Period.cache[key]) == 'undefined') {
        $("#periodResults" + options.prefix).addClass("ajaxloading");
        var ajaxUrl = options.url + parseInt(options.page);
        $.ajax({
                url: ajaxUrl,
                dataType: 'json',
                type: 'POST',
                success: function(data) {
                    RIMS.Period.cache[key] = data;
                    RIMS.Period.ParseResults(options, data);
                },
                error: function() {
                    /* handle the error */
                    $('#periodResults' + options.prefix).remove();
                    $("#periodResults" + options.prefix).append("<li>An error has occured please try again.</li>");

                    /* toggle the menus*/
                    $("#periodResults" + options.prefix).menu("destroy");
                    $("#periodResults" + options.prefix).menu();
                }
            });
    } else {
        RIMS.Period.ParseResults(options, RIMS.Period.cache[key]);
    }
};

/*
* parse the results returned from the ajax
* @param options - options of how instance is configured
* @param data - data to load into results RIMS.Dialog.
*/
RIMS.Period.ParseResults = function (options, data) {
    // hide the loading image and remove previous results
    $("#periodResults" + options.prefix).removeClass("ajaxloading");
    $('#periodResults' + options.prefix).children().remove();
    // if no results then display a message saying so
    if (data.Results.length > 0) {
        $("#dialogNav").show();
        for (var i in data.Results) {
            var item = data.Results[i];
            var link = options.create_link(item);
            link.data(item);
            link.click(function () {
                options.select_result($(this).data());
            });
            //link.addClass("ui-corner-all");
            var li = $('<li>');
            li.append(link);
            $("#periodResults" + options.prefix).append(li);
        }
        if (data.HasPrev) {
            $("#period_Prev" + options.prefix).unbind('click');
            $("#period_Prev" + options.prefix).click(function () {
                options.page = parseInt(data.Page) - 1;
                RIMS.Period.LoadAjaxResults(options);
            });
            $("#period_Prev" + options.prefix).show();
        }
        else {
            $("#period_Prev" + options.prefix).hide();
        }
        if (data.HasNext) {
            $("#period_Next" + options.prefix).unbind('click');
            $("#period_Next" + options.prefix).click(function () {
                options.page = parseInt(data.Page) + 1;
                RIMS.Period.LoadAjaxResults(options);
            });
            $("#period_Next" + options.prefix).show();
        }
        else {
            $("#period_Next" + options.prefix).hide();
        }
    }
    else {
        $("#periodResults" + options.prefix).append("<li>No Results Found.</li>");
    }
    /* toggle the menus*/
    $("#periodResults" + options.prefix).menu("destroy");
    $("#periodResults" + options.prefix).menu();
};

/*
* configures and executes the period selector
* @param instance - unique instance id.
*/
RIMS.Period.Init = function (instance) {
    var options = {
        prefix: instance,
        url: window["urlOffset"] + '/System/Period/AjaxList/',
        page: 0,
        create_link: function (item) {
            var link = $("<a>").attr("href", "javascript: ;;");
            link.text(item.Name);
            return link;
        },
        select_result: function (item) {
            // find the elements we are looking for
            var parentId = instance;

            var startDate = new Date(parseInt(item.Start.substr(6)));
            var endDate = new Date(parseInt(item.End.substr(6)));
            $("#periodLabel" + instance).text(item.Name);
            // BAD: use a date format pattern so that it can reformatted later
            $("#" + parentId + "DateRangeCriteria input[name=StartDate]").val(startDate.getDate() + "/" + (startDate.getMonth() + 1) + "/" + startDate.getFullYear());
            $("#" + parentId + "DateRangeCriteria input[name=EndDate]").val(endDate.getDate() + "/" + (endDate.getMonth() + 1) + "/" + endDate.getFullYear());
        }
    };
    RIMS.Period.LoadAjaxResults(options);
};