/// <reference path="../libraries/_references.js" />
/// <reference path="../libraries/jquery/jquery-1.6.2.js" />
/// <reference path="../libraries/jquery/ui/jquery-ui-1.8.11.js" />
// make sure the namespace is setup
if (typeof ESTMAN == 'undefined')
{
    ESTMAN = function () { };
};

/*
* Prepare the dialog options for use.
*/
$(document).ready(function ()
{
    // don't set the height of the dialog as it doesn't work correctly in IE
    $("#ajaxDialog").dialog({
        bgiframe: true,
        autoOpen: false,
        width: 500,
        modal: true,
        resizable: false,
        buttons: {
            Cancel: function () {
                $(this).dialog('close');
            }
        },
        close: function () {
            $("#dialogButton").unbind('click');
            $("#dialogQuery").unbind('keyup');
        },
        open: function () {
            $("#dialogNav").hide();
            $('#dialogResults').children().remove();
        }
    });
    
    //// ESTMAN.Dialog.SetupHelp($("#ajaxDialog"));

    $("#dialog_Prev").button();
    $("#dialog_Next").button();
    $("#dialogButton").button();
});

/*
* Sets up the Dialog namespace in the ESTMAN namespace
*/
ESTMAN.Dialog = function () { };

/*
* loads a json object via ajax into the results dialog using the specified options
* @param options object to control results parsing
    {
        url: url of json to load
        page: page number of results
        create_link: function(item){} : function to create link for results list
        select_result: function(item){} : function to handle click event of results list item
    }
*/
ESTMAN.Dialog.LoadAjaxResults = function (inputOptions) {

    var options =
    {
        url: '',
        page: 1,
        create_link: function (item) { },
        select_result: function () { },
        no_results: function (dialogPane) { dialogPane.append("<li>No Results Found.</li>"); },
        has_results: function (dialogPane) { },
        post_data: {}
    };
    
    $.extend(options, inputOptions);

    var postData = $("#dialogCriteria").find("input").serialize();
    $.extend(postData, options.post_data);

    $("#dialogResults").addClass("ajaxloading");
    var ajaxUrl = options.url + parseInt(options.page);
    $.ajax({
        url: ajaxUrl,
        dataType: 'json',
        type: 'POST',
        data: postData,
        success: function (data) {
            // hide the loading image and remove previous results
            $("#dialogResults").removeClass("ajaxloading");
            $('#dialogResults').children().remove();
            // if no results then display a message saying so
            if (data.Results.length > 0) {
                $("#dialogNav").show();
                for (var i in data.Results)
                {
                    // Get the data item
                    var item = data.Results[i];
                    
                    // Setup the link item
                    var link = options.create_link(item);
                    $(link).data(item);
                    $(link).click(function ()
                    {
                        options.select_result($(this).data());
                    });

                    // Set up the list item
                    var li = $('<li>');
                    $(li).append(link);

                    // Add the list item to the results.
                    $("#dialogResults").append(li);
                }

                // Show/Hide the previous button as appropriate.
                if (data.HasPrev)
                {
                    $("#dialog_Prev").unbind('click');
                    $("#dialog_Prev").click(function () {
                        options.page = parseInt(data.Page) - 1;
                        ESTMAN.Dialog.LoadAjaxResults(options);
                    });
                    $("#dialog_Prev").show();
                }
                else {
                    $("#dialog_Prev").hide();
                }

                // Show/Hide the next button as appropriate.
                if (data.HasNext)
                {
                    $("#dialog_Next").unbind('click');
                    $("#dialog_Next").click(function () {
                        options.page = parseInt(data.Page) + 1;
                        ESTMAN.Dialog.LoadAjaxResults(options);
                    });
                    $("#dialog_Next").show();
                }
                else {
                    $("#dialog_Next").hide();
                }

                options.has_results($("#dialogResults"));
            }
            else
            {
                options.no_results($("#dialogResults"));
            }
            
            /* toggle the menus*/
            $("#dialogResults").menu();
            $("#dialogResults").menu("destroy");
            $("#dialogResults").menu();

            /* resize the results pane */
            var height = 0;
            $("#dialogResults").children().each(function () {
                height += $(this).height();
            });
            $("#dialogResults").height(height);
        },
        error: function (data) {
            /* handle the error */
            $("#dialogResults").removeClass("ajaxloading");
            $('#dialogResults').children().remove();
            $("#dialogResults").append("<li>An error has occurred please try again.</li>");

            /* toggle the menus*/
            $("#dialogResults").menu("destroy");
            $("#dialogResults").menu();
        }
    });
};

/*
* Opens the ajax dialog
*/
ESTMAN.Dialog.Open = function () {
    $("#ajaxDialog").dialog('open');
};

/*
* Closes the ajax dialog
*/
ESTMAN.Dialog.Close = function () {
    $("#ajaxDialog").dialog('close');
};

/*
* prepares the dialog ready for first use setting relevant details
*/
ESTMAN.Dialog.Prepare = function (inputOptions) {

    var options = {
        title: 'Search',
        rows:
        [
            [
                {
                    text: 'Search',
                    type: 'label'
                },
                {
                    name: 'filter',
                    type: 'text'
                },
                {
                    text: 'Search',
                    name: 'dialogButton',
                    type: 'button',
                    click: inputOptions && inputOptions.defaultSearch ? inputOptions.defaultSearch : function () { alert("No search function defined" ) },
                    defaultAction: true
                }
            ]
        ]
    };

    $.extend(options, inputOptions);

    $("#ajaxDialog").dialog('option', 'title', options.title);

    // remove the old criteria
    $("#dialogCriteria").children().remove();

    // For each row
    for (var r in options.rows)
    {
        var row = $("<tr>");
        
        // For each cell in each row.
        for (var i in options.rows[r])
        {
            // Create a cell.
            var cell = $("<td>");
            
            // Create a field for the cell.
            var field = options.rows[r][i].type == 'label' ? $("<label>") : $("<input>");
            $(field).attr("name", options.rows[r][i].name);
            $(field).attr("type", options.rows[r][i].type);
            $(field).attr("style", options.rows[r][i].style);

            // Determine the field type/
            if (options.rows[r][i].type == 'button' || options.rows[r][i].type == 'submit')
            {
                // Set the text of the field.
                $(field).val(options.rows[r][i].text);
                
                // Set the field as a type button.
                $(field).button();
                
                // set the onclick value of the button.
                $(field).click(options.rows[r][i].click);

                // If the default action has been set then set 
                // the button to be the default action.
                if (options.rows[r][i].defaultAction)
                {
                    $(field).data("default-action", true);
                }

            } 
            else if (options.rows[r][i].type == 'text') 
            {
                // Set the on keypress of enter to fire the default-action.
                $(field).keydown(function (e)
                {
                    if (e.keyCode == 13)
                    {
                        $("#dialogCriteria").find("input").each(function ()
                        {
                            if ($(this).data("default-action"))
                            {
                                $(this).click();
                            }
                        });
                    }
                });
            }
            else
            {
                // Add the text to the label
                $(field).text(options.rows[r][i].text);
            }

            cell.append(field);
            row.append(cell);
        }

        $("#dialogCriteria").append(row);
    }
};

/*
* setups up the help icon for a dialog
* @param dialog - dialog to setup the help on
* @param subject - help topic subject
* @param topic - help topic
*/
ESTMAN.Dialog.SetupHelp = function (dialog, subject, topic) {

    var helpButton = $('<a href="javascript: ;;" class="ui-corner-all ui-dialog-titlebar-help" role="buton"><div class="ui-icon ui-icon-helpthick">Help</div></a>');
    $(dialog).parent().find('.ui-dialog-titlebar-close').parent().append(helpButton);
    helpButton.click(function () {
        // show the help dialog
        ESTMAN.Help.Show(urlOffset + '/Help/AjaxDetail/' + subject + '/' + topic);
    });
};