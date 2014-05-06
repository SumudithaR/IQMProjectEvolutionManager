// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function () { }
}

/*
* method to provide String.Format functionality
*/
String.prototype.format = function () {
    formatted = this;
    for (i = 0; i < arguments.length; i++) {
        formatted = formatted.replace("{" + i + "}", arguments[i]);
    }
    return formatted;
}

/*
* methods to provide wrapping functionality for wrapping ajax calls with a timeout stack 
* message display system
*/
RIMS.AjaxWrapper = function () { }

/*
* Setup the internal variables that are to be used
* _stack - stack for containing time out elements
* _timeout - default time out length in milliseconds, default 10 seconds
* _statusMessage - message that is to be displayed in the notification window
* _notificationWindow - reference to the notification window
*/
RIMS.AjaxWrapper._stack = new Array();
RIMS.AjaxWrapper._timeout = 1000 * 10;
RIMS.AjaxWrapper._statusMessage = "Actions are still being processed, there are currently {0} item(s) left in the queue.";
RIMS.AjaxWrapper._notificationWindow = null;

/*
* set the time out value of the notification window
* @param timeout - timeout in millisecond before the notification window is show
*/
RIMS.AjaxWrapper.SetTimeout = function (timeout) {
this._timeout = timeout;
}
/*
* Adds a new item to the callback stack and executes the specified RIMS.AjaxWrapper.Begin
* @param id - unique if of the action that is being processed
* @param callback - the callback that this is wrapping
*/
RIMS.AjaxWrapper.BeginRequest = function (id, callBack) {
    // check to see if we have been passes a string that needs evaling
    if (typeof callBack == "string") {
        var callBackString = callBack;
        callBack = function () { eval(callBackString) };
    } else {
        // make sure we have a callbackable paramter
        callBack = typeof callBack == "function" ? callBack : function () { };
    }
    // call the callback
    callBack.apply(this);

    // add the id to the stack
    this._addToStack(id);
}
/*
* method to get the size of an object
*/
RIMS.AjaxWrapper._stackSize = function () {
    var size = 0, key;
    for (key in this._stack) {
        if (this._stack.hasOwnProperty(key)) size++;
    }
    return size;
}
/*
* Addes an item to the stack
* @param id - id of the item to add to the stack
*/
RIMS.AjaxWrapper._addToStack = function (id) {
    this._stack[id] = setTimeout(function () { RIMS.AjaxWrapper.DisplayNotification() }, this._timeout);
    // update the notification window
    this._updateNotificationWindow();
}

/*
* Removed an item from the callback stack and executes the specified RIMS.AjaxWrapper.Begin
* @param id - unique if of the action that is being processed
* @param callback - the callback that this is wrapping
*/
RIMS.AjaxWrapper.EndRequest = function (id, callBack) {
    // check to see if we have been passes a string that needs evaling
    if (typeof callBack == "string") {
        var callBackString = callBack;
        callBack = function () { eval(callBackString) };
    } else {
        // make sure we have a callbackable paramter
        callBack = typeof callBack == "function" ? callBack : function () { };
    }
    // call the callback
    callBack.apply(this);

    // add the id to the stack
    this._removeFromStack(id);
}

/*
* Removes an item from the stack
* @param id - id of the item to remove from the stack
*/
RIMS.AjaxWrapper._removeFromStack = function (id) {
    // get item from the stack
    var item = this._stack[id];
    // make sure the id isn't invalid or hasn't already been removed.
    if (id != null) {
        clearTimeout(item);
        delete this._stack[id];
    }
    // update the notification window
    this._updateNotificationWindow();

    // check the length of the stack, if its 0 then hide it
    if (this._stackSize() == 0) {
        this._hideNotificationWindow()
    }
}

/*
* hides the notification window
*/
RIMS.AjaxWrapper._hideNotificationWindow = function () {
    // use jQuery to hide the window
    $("#AjaxWrapper_NotificationWindow").slideUp("slow");
}

/*
* creates the notification window
*   <div id="AjaxWrapper_NotificationWindow">
*       <label id="AjaxWrapper_StatusMessage"></label>
*       <label id="AjaxWrapper_Stack_Count"></label>
*   </div>
*/
RIMS.AjaxWrapper._initNotificationWindow = function () {
    // make sure that we haven't already created the notification window
    if (this._notificationWindow == null) {
        // create the dom elements to and append it to the body
        // use the internal var so we can check it later
        this._notificationWindow = document.createElement("div");
        this._notificationWindow.setAttribute("id", "AjaxWrapper_NotificationWindow");
        this._notificationWindow.setAttribute("style", "display: none;");
        
        // create the status message element
        var statusMessage = document.createElement("p");
        statusMessage.setAttribute("id", "AjaxWrapper_StatusMessage");

        // append child elements to the notification window
        this._notificationWindow.appendChild(statusMessage);

        // append notification window to the body
        document.getElementsByTagName("body")[0].appendChild(this._notificationWindow);
    }
}
/*
* updates the notification window values
*/
RIMS.AjaxWrapper._updateNotificationWindow = function () {
    // init the window
    this._initNotificationWindow();
    // update the values

    var statusMessage = document.getElementById("AjaxWrapper_StatusMessage");
    statusMessage.innerHTML = this._statusMessage.format(this._stackSize());

    ///stackCount.innerHTML = "0"; // Object.size(this._stack);
}

/*
* display the notification window
*/
RIMS.AjaxWrapper.DisplayNotification = function () {
    this._updateNotificationWindow();
    if (!$("#AjaxWrapper_NotificationWindow").is(':visible')) {
        // use jQuery to display the window
        $("#AjaxWrapper_NotificationWindow").slideDown("slow");
    }
}

/*
* Callback that is RIMS.AjaxWrapper.Begin when the ajax form is posted
* sets the submit button to disabled and addes the loading css class
* @param submitButtonId - id of the submit button to disable
*/
RIMS.AjaxWrapper.Begin = function (submitButtonId) {
    $("#" + submitButtonId).attr("disabled", true);
    $("#" + submitButtonId).addClass("loading-content");
}

/*
* Callback that is called when the ajax form fails
* sets the submit button to enabled and removes the loading css class
* @param submitButtonId - id of the submit button to disable
*/
RIMS.AjaxWrapper.Failed = function (submitButtonId) {
    $("#" + submitButtonId).attr("disabled", false);
    $("#" + submitButtonId).removeClass("loading-content");
}