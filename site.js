// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var loadingimageTimeout;
var serverException;

$(document).ready(function () {
    var srcval = $("#imgSiteUrl").attr("src");
    baseUrl = srcval.substring(0, srcval.length - 1);

    RegisterCollapseExpand();
    SetAutocompleteOff();
    $.ajaxSetup({
        success: function (result) {

            //TODO: hide loading symbol    
            ShowHideLoadingImage(true);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // window.location = baseUrl + '/Error';
            HandlleClientSideErrors(XMLHttpRequest, textStatus, errorThrown);

            //TODO: hide loading symbol
            ShowHideLoadingImage(false);
        }
    });
    $(document).ajaxStart(function () {
        ShowHideLoadingImage(true);
    });
    $(document).ajaxStop(function () {
        ShowHideLoadingImage(false);
    });
    $(document).click(function (e) {
        if (!$(e.target).hasClass("nav-item") &&
            !$(e.target).hasClass("nav-link")) {
            $('.dropdown-toggle-custom').each(function () {
                if ($(this).data("level") == "0") {
                    $(this).data("selected", "0");
                }
                $(this).parent().find('.show').removeClass('show');
                $(this).parent().find('.selected-menu').removeClass('selected-menu');
            });
        }
    });
});

/* Handles the client side errors */
function HandlleClientSideErrors(xhr, status, error) {
    console.log(xhr);
    serverException = true;
    var messageObject = null;
    try {
        messageObject = JSON.parse(xhr.responseText);
    } catch (e) {

    }
    //ShowMessage(messageObject, null, false, false);
    ShowAjaxErrorMessage(messageObject);
}
/*------------------------------------------------------------------*/
/* Registers the click event by class
 className should start with .
 example: .search
 Ensures the click event to be attached only once
 */
function RegisterClickEventByClass(className, callbackfunc) {
    $(className).off("click").on("click", function (e) {
        if ($(this).attr("disabled") != "disabled") {
            callbackfunc($(this));
        }
        ShowHideSpinner($(this), true);
        e.preventDefault();
    });
}

/*------------------------------------------------------------------*/
/* Shows or Hides the Loading image */
function ShowHideLoadingImage(show) {
    if (show) {
        loadingimageTimeout = setTimeout(function () {
            if (serverException == null || serverException == undefined)
                $(".loadingimage").show();
        }, 1000);
    }
    else {
        $(".loadingimage").hide();
        if (loadingimageTimeout != null) {
            clearTimeout(loadingimageTimeout);
        }
    }
}

/* Shows(Disables the element) or Hides(Enables the element) the Spinner */
function ShowHideSpinner(ele, showSpinner) {
    var eleTemp = $(ele);
    if (showSpinner) {
        eleTemp.attr("disabled", "disabled");
        eleTemp.removeClass("btn-empty-content");
        eleTemp.addClass("btn-fa-spinner");
    }
    else {
        eleTemp.removeAttr("disabled");
        eleTemp.addClass("btn-empty-content");
        eleTemp.removeClass("btn-fa-spinner");
    }
}

/*------------------------------------------------------------------*/
/* Registers the collapse/expand functionality */
function RegisterCollapseExpand(className) {
    if (className == "" || className == null || className == undefined) {
        className = ".search";
    }
    $(className).off("click").on("click", function () {
        var ele1 = $(this)[0];
        var content = ele1.nextElementSibling;
        if (content.style.display === "block" || content.style.display === "") {
            $(this).removeClass("legend-fa-search-up");
            $(this).addClass("legend-fa-search-down");
            content.style.display = "none";
        } else {
            $(this).removeClass("legend-fa-search-down");
            $(this).addClass("legend-fa-search-up");
            content.style.display = "block";
        }
    });
}

/* Sets the autocomplete attribute off for the form */
function SetAutocompleteOff() {
    $('form').attr('autocomplete', 'off');
}

/* Sets the Events and Attributes of elements or forms on load */
function SetEventsAttrOnLoad() {
    RegisterCollapseExpand();
    SetAutocompleteOff();
}

/*------------------------------------------------------------------*/
/* Makes a Ajax call Helper: Get/Post
 url: url text 
 posturl: post url text
 params: Example: {'Firstname':''}
 header: null or custom header
 dataType: html, json
 GetData: function to get the data
 PostData: function to post the data
 */
function AjaxCallsHelper(url, posturl, params, header, dataType) {
    this.fullurl = url;
    this.fullposturl = posturl;
    this.url = url;
    this.posturl = posturl;
    this.params = null;
    this.header = null;
    this.dataType = "json";


    if (url != null) {
        if (url.startsWith("/")) {
            this.url = baseUrl + url;
        }
        else {
            if (baseUrl != "" && baseUrl != undefined && baseUrl != null &&
                baseUrl.indexOf("https") == -1 && baseUrl.indexOf("http") == -1) {
                this.url = baseUrl + "/" + url;
            }
        }
    }

    if (posturl != null) {
        if (posturl.startsWith("/")) {
            this.posturl = baseUrl + posturl;
        }
        else {
            if (baseUrl != "" && baseUrl != undefined && baseUrl != null &&
                baseUrl.indexOf("https") == -1 && baseUrl.indexOf("http") == -1) {
                this.posturl = baseUrl + "/" + posturl;
            }
        }
    }

    if (params != null && params != undefined) {
        this.params = params;
    }

    if (header != null && header != undefined) {
        this.header = header;
    }

    if (dataType != null && dataType != undefined) {
        this.dataType = dataType;
    }

    var origurl = this.url;
    var origposturl = this.posturl;
    var origparams = this.params;
    var origheader = this.header;
    var origdataType = this.dataType;

    this.Reset = function () {
        this.url = origurl;
        this.posturl = origposturl;
        this.params = origparams;
        this.header = origheader;
        this.dataType = origdataType;
    }

    this.GetData = function (currEle, className) {
        serverException = null;
        var ajaxTemp = $.ajax({
            url: this.url,
            type: 'GET',
            data: this.params,
            headers: this.header,
            dataType: this.dataType,
            async: true,
            success: function (data) {
                if (className != null && className != undefined && className != "") {
                    //$(className).removeAttr("disabled");
                    ShowHideSpinner($(className), false);
                }
                if (currEle != null && currEle != undefined && currEle != "") {
                    //$(currEle).removeAttr("disabled");
                    ShowHideSpinner($(currEle), false);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                HandlleClientSideErrors(XMLHttpRequest, textStatus, errorThrown);
                if (className != null && className != undefined && className != "") {
                    //$(className).removeAttr("disabled");
                    ShowHideSpinner($(className), false);
                }
                if (currEle != null && currEle != undefined && currEle != "") {
                    //$(currEle).removeAttr("disabled");
                    ShowHideSpinner($(currEle), false);
                }
            }
        });

        return ajaxTemp;
    }

    this.PostData = function (currEle, className) {
        serverException = null;
        var paramsTemp = addRequestVerificationToken(this.params);

        var ajaxTemp = $.ajax({
            url: this.posturl,
            type: 'POST',
            data: paramsTemp,
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            headers: this.header,
            dataType: this.dataType,
            async: true,
            success: function (data) {
                if (className != null && className != undefined && className != "") {
                    $(className).removeAttr("disabled");
                }
                if (currEle != null && currEle != undefined && currEle != "") {
                    $(currEle).removeAttr("disabled");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                HandlleClientSideErrors(XMLHttpRequest, textStatus, errorThrown);
                if (className != null && className != undefined && className != "") {
                    $(className).removeAttr("disabled");
                }
                if (currEle != null && currEle != undefined && currEle != "") {
                    $(currEle).removeAttr("disabled");
                }
            }
        });

        return ajaxTemp;
    }

    this.GetDataFullUrl = function () {
        serverException = null;
        this.fullurl = url;
        var ajaxTemp = $.ajax({
            url: this.fullurl,
            type: 'GET',
            data: this.params,
            headers: this.header,
            dataType: this.dataType,
            async: false,
        });

        return ajaxTemp;
    }

    this.PostDataFullUrl = function () {
        serverException = null;
        var paramsTemp = addRequestVerificationToken(this.params);
        this.fullposturl = posturl;
        var ajaxTemp = $.ajax({
            url: this.fullposturl,
            type: 'POST',
            data: paramsTemp,
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            headers: this.header,
            dataType: this.dataType,
            async: false,
        });

        return ajaxTemp;
    }
}

/* Makes a Ajax call: Get
 header: null or custom header
 params: Example: {'Firstname':''}
 dataType: html, json
 */
function GetDataAjaxCall(url, params, header, dataType) {
    var headerTemp;
    var dataTypes = "json";
    var urlTemp = url;
    if (header != null && header != undefined) {
        headerTemp = header;
    }
    if (dataType != null && dataType != undefined) {
        dataTypes = dataType;
    }
    if (url.startsWith("/")) {
        urlTemp = baseUrl + url;
    }
    else {
        if (baseUrl != "" && baseUrl != undefined && baseUrl != null &&
            baseUrl.indexOf("https") == -1 && baseUrl.indexOf("http") == -1) {
            urlTemp = baseUrl + "/" + url;
        }
    }
    var ajaxTemp = $.ajax({
        url: urlTemp,
        type: 'GET',
        data: params,
        headers: headerTemp,
        dataType: dataTypes,
        async: false,
    });

    return ajaxTemp;
}

/* Makes a Ajax call: Post 
 header: null or custom header
 params: Use serializeObject function for serializing the form
 dataType: json
 */
function PostDataAjaxCall(url, params, header, dataType) {
    var headerTemp;
    var dataTypes = "json";
    var urlTemp = url;

    if (header != null && header != undefined) {
        headerTemp = header;
    }
    if (dataType != null && dataType != undefined) {
        dataTypes = dataType;
    }
    params = addRequestVerificationToken(params);

    if (url.startsWith("/")) {
        urlTemp = baseUrl + url;
    }
    else {
        if (baseUrl != "" && baseUrl != undefined && baseUrl != null &&
            baseUrl.indexOf("https") == -1 && baseUrl.indexOf("http") == -1) {
            urlTemp = baseUrl + "/" + url;
        }
    }

    var ajaxTemp = $.ajax({
        url: urlTemp,
        type: 'POST',
        data: params,
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        headers: headerTemp,
        dataType: dataTypes,
        async: false,
    });
    return ajaxTemp;
}

/* Makes a Ajax Async call: Get
 header: null or custom header
 params: Example: {'Firstname':''}
 dataType: html, json 
 */
function GetDataAjaxAsyncCall(url, params, header, dataType) {
    var headerTemp;
    var dataTypes = "json";
    var urlTemp = url;
    if (header != null && header != undefined) {
        headerTemp = header;
    }
    if (dataType != null && dataType != undefined) {
        dataTypes = dataType;
    }
    if (url.startsWith("/")) {
        urlTemp = baseUrl + url;
    }
    else {
        if (baseUrl != "" && baseUrl != undefined && baseUrl != null &&
            baseUrl.indexOf("https") == -1 && baseUrl.indexOf("http") == -1) {
            urlTemp = baseUrl + "/" + url;
        }
    }
    var ajaxTemp = $.ajax({
        url: urlTemp,
        type: 'GET',
        data: params,
        headers: headerTemp,
        dataType: dataTypes,
        async: true,
    });

    return ajaxTemp;
}

/* Adds the Antiforgery Verification token to the header */
function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

/*------------------------------------------------------------------*/
/* Serializes the elements inside the form to object with properties*/
$.fn.serializeObject = function () {
    var o = {};
    var disabled = this.find(':input:disabled').removeAttr('disabled');
    var a = this.serializeArray();
    disabled.attr('disabled', 'disabled');
    $.each(a, function () {
        if (o[this.name] !== undefined) {

        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
}

/* Supporting function: converts the query to dictionary elements */
$.extend({
    toDictionary: function (query) {
        var parms = {};
        var items = query.split("&"); // split
        for (var i = 0; i < items.length; i++) {
            var values = items[i].split("=");
            var key = decodeURIComponent(values.shift());

            if (key.indexOf(']') > -1 && key.indexOf('.') > -1) {
                key = key.substring(key.indexOf('.') + 1, key.length);
            }

            if (parms[key] !== undefined) {

            }
            else {
                var value = values.join("=");
                parms[key] = decodeURIComponent(value);
            }
        }
        return (parms);
    }
});

/* Serialize data in the table
 * id: should start with #
 * classname: should start with .
 creates an dictionary for each row and assigns it to the array
 Sets the property(propertyName) of object with the array
 Note: Used to send table data during a post event, 
 on the server side this property can be List<Model> 
 */
$.fn.serializeTable = function (tableId, className, obj, propertyName) {
    var o = [];
    var ele;
    if (tableId != null && tableId != undefined) {
        ele = tableId;
    }
    else {
        ele = className;
    }

    $(this).find(ele).find('tr').each(function () {
        var elements = $(this).find('input, textarea, select')
        if (elements.size() > 0) {
            var serialized = elements.serialize();
            var item = $.toDictionary(serialized);
            o.push(item);
        }
    });
    obj[propertyName] = o;
    return obj;
};

/*------------------------------------------------------------------*/
var isPopupDisplayed = false;
/* Shows the Message */
function ShowMessage(messageObject, parentContainerid, showSuccessMsg, hideServerMessage) {
    if (messageObject == null || messageObject == undefined) {
        console.log("Message Object is " + messageObject);
        return false;
    }
    if (hideServerMessage == null || hideServerMessage == undefined) {
        hideServerMessage = true;
    }
    if (showSuccessMsg == null || showSuccessMsg == undefined) {
        showSuccessMsg = false;
    }
    if (parentContainerid == null || parentContainerid == undefined) {
        //  parentContainerid = masterLayoutId;
        var eleParent = GetTopmostFormPopup();
        if ($(eleParent).length > 0)
            parentContainerid = "#" + $(eleParent).find(".ui-dialog-content")[0].id;
    }

    var messageCode = messageObject.MessageCode;
    var msgText = messageObject.MessageText;
    var messageTypeCode = messageObject.MessageTypeCode;
    var messageModels = messageObject.MessageModels;
    if (messageTypeCode == null || messageTypeCode == undefined) {
        messageTypeCode = "e";
    }

    if (messageTypeCode.toLowerCase() == "s") {
        if (showSuccessMsg) {
            ShowClientMessage(parentContainerid, messageObject, hideServerMessage);
        }
        else {
            HideClientMessageSections(parentContainerid, hideServerMessage);
        }
        return true;
    }
    else {
        ShowClientMessage(parentContainerid, messageObject, hideServerMessage);
        if (messageTypeCode.toLowerCase() == "e")
            return false;
        else
            return true;
    }

    //if (messageCode != null && messageCode != undefined && messageCode != "") {
    //if (messageTypeCode.toLowerCase() == "e") {
    //    ShowMessageError(parentContainerid, msgText, messageModels, hideServerMessage);
    //    return false;
    //}
    //else if (messageTypeCode.toLowerCase() == "w") {
    //    ShowMessageWarning(parentContainerid, msgText, messageModels, hideServerMessage);
    //    return true;
    //}
    //else if (messageTypeCode.toLowerCase() == "i") {
    //    ShowMessageInformation(parentContainerid, msgText, messageModels, hideServerMessage);
    //    return true;
    //}
    //else if (messageTypeCode.toLowerCase() == "s") {
    //    if (showSuccessMsg) {
    //        ShowMessageSuccess(parentContainerid, msgText, messageModels, hideServerMessage);
    //    }
    //    else {
    //        HideMessageSections(parentContainerid, hideServerMessage);
    //    }
    //    return true;
    //}
    //else {
    //    ShowMessageError(parentContainerid, msgText, messageModels, hideServerMessage);
    //    return false;
    //}
    //}
    return false;
}

/* Shows the Client message */
function ShowClientMessage(parentContainerid, messageObject, hideServerMessage) {
    //HideClientMessageSections(parentContainerid, hideServerMessage);
    var divClientMessage;
    var diMsgText;
    var spanEle;
    var spanHeading;
    var spanChildEle;
    var pChildMessages;
    var parentId = parentContainerid.replace("#", "");

    //divClientMessage = $(parentContainerid).find("#divClientMessage");
    //diMsgText = $(parentContainerid).find("#divClientMessageText");
    //spanHeading = $(parentContainerid).find("#spanHeading").clone();
    //pChildMessages = $(parentContainerid).find("#pChildMessages").clone();
    //spanChildEle = pChildMessages.find("#childMessages").clone();


    divClientMessage = $("#divClientMessage").clone();
    diMsgText = $("#divClientMessageText").clone();
    spanHeading = $("#spanHeading").clone();
    pChildMessages = $("#pChildMessages").clone();
    spanChildEle = pChildMessages.find("#childMessages").clone();

    divClientMessage[0].id = parentId + divClientMessage[0].id;
    diMsgText[0].id = parentId + diMsgText[0].id;
    spanHeading[0].id = parentId + spanHeading[0].id;
    pChildMessages[0].id = parentId + pChildMessages[0].id;

    if ($("#" + divClientMessage[0].id).length > 0) {
        $("#" + divClientMessage[0].id).remove();
    }

    divClientMessage.show();
    spanHeading.show();

    spanHeading.html(messageObject.MessageDescription);
    diMsgText.addClass(messageObject.MessageTypeIconClass);
    diMsgText.append(spanHeading);

    if (messageObject.MessageModels != null && messageObject.MessageModels != undefined) {
        for (var i = 0; i < messageObject.MessageModels.length; i++) {
            spanEle = $(spanChildEle).clone();
            spanEle[0].id = parentId + spanEle[0].id + i;
            spanEle.html(messageObject.MessageModels[i].MessageDescription);
            spanEle.show();
            pChildMessages.append(spanEle);
        }
        pChildMessages.show();
        diMsgText.append(pChildMessages);
    }
    divClientMessage.append(diMsgText);

    $(parentContainerid).prepend(divClientMessage);
}

/* Hides all the message section */
function HideClientMessageSections(parentContainerid, hideServerMessage) {
    if ($(parentContainerid + "divClientMessage").length > 0) {
        $(parentContainerid + "divClientMessage").remove();
    }

    //$(parentContainerid).find("#divClientMessage").hide();
    //$(parentContainerid).find("#divClientMessageText").removeClass();
    //$(parentContainerid).find("#divClientMessageText").empty();
    //if (hideServerMessage) {
    //    if ($(parentContainerid).length > 0) {
    //        $(parentContainerid).find("#divMessageServer").hide();
    //        $(parentContainerid).find("#divMessageServer").empty();

    //    }
    //}
}

///* Shows the Success message */
//function ShowMessageSuccess(parentContainerid, msgText, messageModels, hideServerMessage) {
//    HideMessageSections(parentContainerid, hideServerMessage);

//    $(parentContainerid).find("#divSuccessMessage").show();
//    $(parentContainerid).find("#divSuccessMessageText").html(msgText);
//}

///* Shows the Error message */
//function ShowMessageError(parentContainerid, msgText, messageModels, hideServerMessage) {
//    HideMessageSections(parentContainerid, hideServerMessage);

//    $(parentContainerid).find("#divErrorMessage").show();
//    $(parentContainerid).find("#divErrorMessageText").html(msgText);
//}

///* Shows the Warning message */
//function ShowMessageWarning(parentContainerid, msgText, messageModels, hideServerMessage) {
//    HideMessageSections(parentContainerid, hideServerMessage);

//    $(parentContainerid).find("#divWarningMessage").show();
//    $(parentContainerid).find("#divWarningMessageText").html(msgText);
//}

///* Shows the Information message */
//function ShowMessageInformation(parentContainerid, msgText, messageModels, hideServerMessage) {
//    HideMessageSections(parentContainerid, hideServerMessage);

//    $(parentContainerid).find("#divInformationMessage").show();
//    $(parentContainerid).find("#divInformationMessageText").html(msgText);
//}

///* Hides all the message section */
//function HideMessageSections(parentContainerid, hideServerMessage) {
//    $(parentContainerid).find("#divErrorMessage").hide();
//    $(parentContainerid).find("#divErrorMessageText").empty();
//    $(parentContainerid).find("#divWarningMessage").hide();
//    $(parentContainerid).find("#divWarningMessageText").empty();
//    $(parentContainerid).find("#divInformationMessage").hide();
//    $(parentContainerid).find("#divInformationMessageText").empty();
//    $(parentContainerid).find("#divSuccessMessage").hide();
//    $(parentContainerid).find("#divSuccessMessageText").empty();
//    if (hideServerMessage) {
//        $(parentContainerid).find("#divMessageServer").hide();
//        $(parentContainerid).find("#divMessageServer").empty();
//    }
//}

/*------------------------------------------------------------------*/
/* Global error */
window.onerror = function (msg, url, line, col, error) {

    // You can view the information in an alert to see things working like this:
    console.log("Error: ", msg, url, line, col, error);
    return true;
};
/*------------------------------------------------------------------*/
/* Gets the document offset width from body reduced by (v) amount */
function GetDocumentOffsetWidth(v) {
    if (v == null || v == undefined)
        v = 0;
    return document.body.offsetWidth - v;
}

/* Gets the document offset Height from body reduced by (v) amount */
function GetDocumentOffsetHeight(v) {
    if (v == null || v == undefined)
        v = 0;
    return document.body.offsetHeight - v;
}

/* Gets the document client width from body reduced by (v) amount */
function GetDocumentClientWidth(v) {
    if (v == null || v == undefined)
        v = 0;
    return document.body.clientWidth - v;
}

/* Gets the document client height from body reduced by (v) amount*/
function GetDocumentClientHeight(v) {
    if (v == null || v == undefined)
        v = 0;
    return document.body.clientHeight - v;
}

/*------------------------------------------------------------------*/
/* Set the form in unbostrusive mode*/
function SetFormUnobstrusiveMode(selector) {
    $.validator.unobtrusive.parse(selector);
}


/* Set the form in unbostrusive mode*/
function SetFormUnobstrusiveModeById(ctrlId, selector) {
    $.validator.unobtrusive.parse(GetChildElementByIdSelector(ctrlId,selector));
}

/* Set the form in unbostrusive mode by element*/
function SetFormUnobstrusiveModeByElement(element) {
    $.validator.unobtrusive.parseElement(element);
}

/* Set the form in unbostrusive mode*/
function ValidateFormUnobstrusiveModeByClass(selector) {
    return $(selector).valid();
}

/* Set the form in unbostrusive mode*/
function ValidateFormUnobstrusiveModeByClassById(ctrlId, selector) {
    return GetChildElementByIdSelector(ctrlId, selector).valid();
}
/*------------------------------------------------------------------*/
function TrimEnd(obj) {
    if (obj != null && obj != undefined) {
        return obj.toString().trimEnd();
    }
    return obj;
}
/*------------------------------------------------------------------*/
function ShowAlertMessage(message) {
    return Swal.fire({
        icon: 'error',
        title: message
    });
}

/*
 isConfirmed	The "Confirm" button was clicked, the value will contain the result
isDenied	The "Deny" button was clicked, the value will be false.
isDismissed	The "Cancel" button was clicked, the dismiss will be Swal.DismissReason.cancel
 */

function ShowConfirmationMessage(message, confirmButtonText, cancelButtonText) {
    var confirmBtnText = 'OK';
    var cancelBtnText = 'Cancel';
    if (confirmButtonText != null && confirmButtonText != "" && confirmButtonText != undefined) {
        confirmBtnText = confirmButtonText;
    }

    if (cancelButtonText != null && cancelButtonText != "" && cancelButtonText != undefined) {
        cancelBtnText = cancelButtonText;
    }
    return Swal.fire({
        icon: 'warning',
        title: message,
        showCancelButton: true,
        confirmButtonText: confirmBtnText,
        cancelButtonText: cancelBtnText,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33'
    });
}

function ShowAjaxErrorMessage(messageObject) {
    var msgDesc = "";
    if (messageObject.MessageModels != null && messageObject.MessageModels != undefined) {
        $(messageObject.MessageModels).each(function () {
            msgDesc = $(this)[0].MessageText + ' - ' + $(this)[0].MessageDescription;
        });
    }
    return Swal.fire({
        icon: 'error',
        title: messageObject.MessageText,
        text: msgDesc
    });
}

function ShowAjaxMessage(messageObject) {
    var msgDesc = "";
    if (messageObject.MessageTypeCode.toLowerCase() == "s") {
        return true;
    }
    if (messageObject.MessageModels != null && messageObject.MessageModels != undefined) {
        $(messageObject.MessageModels).each(function () {
            msgDesc = $(this)[0].MessageText + ' - ' + $(this)[0].MessageDescription;
        });
    }
    return Swal.fire({
        icon: 'error',
        title: messageObject.MessageText,
        text: msgDesc
    });
}

/*------------------------------------------------------------------*/
/*
 * Makes the html table- scrollable - horizontally, vertically
 * id: should start with #
 * classname: should start with .
 */
function ScrollableDataTable(id, classname, width, scrollY, isScrollX) {
    //parent div.container {    
    //    width: 80 %;
    //} 
    if (width == null || width == undefined) {
        width = '100%';
    }
    if (id != "" || id != null && id != undefined) {
        $(id).DataTable({
            "scrollY": scrollY,
            "scrollX": isScrollX,
            "width": width,
            "paging": false,
            "ordering": false,
            "info": false,
            "bAutoWidth": true,
            "bFilter": false
        });
    }
    else {
        $(classname).DataTable({
            "scrollY": scrollY,
            "scrollX": isScrollX,
            "width": width,
            "paging": false,
            "ordering": false,
            "info": false,
            "bAutoWidth": true,
            "bFilter": false
        });
    }
}
/*------------------------------------------------------------------*/
/*Shows the form in the popup
 * height: should be in px,
 * width: should be in px,
 * maxwidth: should be in px
 * No need to code to show multiple popups on each other , jquery dialog already handles it 
 *    var buttons = {
                "Ok": function () {
                    $(this).dialog("close");
                    $(departmentPopupContainerId).html('');
                },
                "Close": function () {
                    $(this).dialog("close");
                    $(departmentPopupContainerId).html('');
                }
            };
 */
function ShowFormPopup(popupContainerId, classname, height, width, maxwidth, title, buttons) {
    if (height == null || height == undefined) {
        height = "auto";
    }
    if (width == null || width == undefined) {
        width = "auto";
    }
    if (buttons == null || buttons == undefined) {
        buttons = {};
    }
    return $(popupContainerId).dialog({
        autoOpen: true,
        resizable: false,
        height: height,
        width: width,
        modal: true,
        title: title,
        moveToTop: true,
        create: function (event, ui) {
            if (maxwidth != null && maxwidth != undefined) {
                $(this).css("maxWidth", maxwidth);
            }
        },
        open: function () {
            $(".ui-dialog-titlebar-close").hide();
        },
        buttons: buttons
    });
}

function GetChildElementByIdSelector(ctrId, selector) {
    var ctrl = $(ctrId);
    if (ctrl != null && ctrl != undefined) {
        return $(ctrl).find(selector)
    }
    return $(selector);
}

/* Gets the topmost popup
 *
 */
function GetTopmostFormPopup() {
    var uidialogs = $(".ui-dialog");
    var index = 0;
    var zindex = 0;
    if (uidialogs.length > 0) {
        uidialogs.each(function (i) {
            if (i == 0) {
                zindex = $(this).css("z-index");
            }
            if ($(this).css("display") != "none") {
                if ($(this).css("z-index") > zindex) {
                    index = i;
                }
            }
        });
        return uidialogs[index];
    }
    return null;
}
/*
 * Dummy function - does not anything, use/pass this if their is no callback function
 */
function DummyFunction() {

}