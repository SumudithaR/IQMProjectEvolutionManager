// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}


$(document).ready(function () {
    $("#importServices").accordion({
        collapsible: true,
        autoHeight: false,
        active: false
    });
});

RIMS.Import = function () { }

RIMS.Import._timer = null;
RIMS.Import._interval =  1000 * 10;
/*
* sets up the process to get the import status as it is being executed
*/
RIMS.Import.Begin = function () {
    $("#ImportStatus").slideDown('fast');
    $("#ImportForm, #PendingSteps").slideUp('fast');
    // setup the time out
    RIMS.Import._timer = setInterval(function () { RIMS.Import.GetStatus() }, RIMS.Import._interval);
}

/*
* Gets the status of the current import
*/
RIMS.Import.GetStatus = function () {
/*
    $.ajax({
        type: "POST",
        dataType: 'json',
        url: urlOffset + "/Import/Status",
        data: {
            'Type': $("#Type").val(),
            'ImportProcess': $("#ImportProcess").val()
        },
        success: function (response) {
            alert(response);
            // append the response
            $("#ImportMessages").children().remove();
            for (var i in response) {
                $("#ImportMessages").append($("li").text(response[i].Message));
            }
        },
        error: function (error) {
            // jsut ignore the error
            alert("Error: " + error);
        }
    });
    */
}

/*
* method that is called when the import has finished, either failed or completed.
*/
RIMS.Import.Complete = function () {
    clearTimeout(RIMS.Import._timer);
    $("#ImportStatus").slideUp('fast');
    $("#ImportComplete").slideDown('fast');
    setTimeout(RIMS.Global.SetUpButtons, 100);
}