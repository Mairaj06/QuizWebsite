var urlVar = "http://localhost/QuizWebSite/";
var currentPage = 1;
var itemsOnPage = 20;
var dataTable = null;
var ajaxCallCounter = 0;
$(document).ready(function () {
    try {
        $(document).ajaxSend(function (event, request, settings) {
            ajaxCallCounter++;
            BlockUI('Processing request please wait...');
        });
        $(document).ajaxSuccess(function (event, request, settings) {
            ajaxCallCounter--;
            if (ajaxCallCounter == 0)
                UnBlockUI();
        });
        $(document).ajaxError(function (event, jqxhr, settings, exception) {
            ajaxCallCounter--;
            if (jqxhr.status == 401)
                window.location.reload(true);
            else if (jqxhr.status == 403)
                ShowErrorToastMessage("Your token has expired. Please re-login.");
            else {
                UnBlockUI();
                ShowErrorToastMessage("An error occured processing the request");

            }
        });
        
        $("form").find(":input[required]").keyup(function () {
            $(this).css("border-color", "");
        });

        $('[data-toogle="tooltip"]').tooltip();

        $('.input-group').datetimepicker({
            format: 'DD/MM/YYYY',
            showClear: true
        });
        
        
    } catch (e) {
        //alert(e.message);
    }

    
});

function ShowSuccessToastMessage(message) {
    toastr.remove();
    toastr.success(message, '');
}
function ShowErrorToastMessage(message) {
    try {
        toastr.remove();
        toastr.error(message, '');
    } catch (e) {

    }
    
}
function ShowWarningToastMessage(message) {
    toastr.remove();
    toastr.warning(message, '');
}

function BlockUI(pMessage) {
    try {
        $.blockUI({
            //message: '<img src="../../application/assets/layouts/layout/img/loading-spinner-blue.gif" />',
            message: '<h1>Processing please wait</h1>',
            css: {
                backgroundColor: '#fff',

                border: '0'
            }
        });
    }
    catch (ex) {

    }
}
function UnBlockUI() {
    try {
        $.unblockUI();
    }
    catch (ex) {

    }

}

function APICall(method, SuccessMethod, FailureMethod, Type, Data) {
    $.ajax({
        cache: true,
        type: Type,
        url: urlVar+method,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: Data,
        success: window[SuccessMethod],
        error: window[FailureMethod]
    });
}
function getUrlData() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0].toLowerCase());
        vars[hash[0].toLowerCase()] = hash[1];
    }
    return vars;
}
function FormatJSONDate(jsonDate) {

    if (jsonDate != null) {
        var date = new Date(parseInt(jsonDate.substr(6)));
        date = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
        return date;
    }
    else
        return "";
}

function ValidateForm(frmId) {
    var validated = true;
    var formElements = $("#" + frmId).find(":input[required]");

    for (var i = 0; i < formElements.length; i++) {
        var element = $(formElements[i]);
        if ($(element).attr("type") == "text") {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                $(element).focus();
                validated = false;

            }
        }
        if ($(element).attr("type") == "email") {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                $(element).focus();
                validated = false;

            }
            else {
                var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                var matched = reg.test($(element).val());
                if (!matched) {

                    $(element).css("border-color", "red");
                    $(element).focus();
                    validated = false;
                }
            }
        }
        if ($(element).attr("type") == "number") {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                $(element).focus();
                validated = false;

            }
        }
        if ($(element).is("textarea")) {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                $(element).focus();
                validated = false;

            }
        }
        if ($(element).attr("type") == "password") {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                $(element).focus();
                validated = false;

            }
        }
        if ($(element).is("select")) {

            if (($(element).val() == -1) || ($(element).val() == 0) || ($(element).val() == "") || ($(element).val() == null))  // Add OR condition by Zeeshan
            {
                $(element).closest('.select-wrapper').css("border-color", "red");
                $(element).focus();
                validated = false;
            }
        }
        if ($(element).attr("type") == "file") {
            if ($(element).val() == "") {
                //ShowErrorToastMessage($(element).data("message"));
                $(element).css("border-color", "red");
                validated = false;
                return;

            }
            
        }

    }
    return validated;

}

function ApplyPagination(totalRecord, methodName) {


    if (totalRecord > itemsOnPage) {
        $("#divPagination").pagination({
            items: totalRecord,
            itemsOnPage: itemsOnPage,
            cssStyle: 'light-theme',
            listStyle: 'pagination',
            onPageClick: function (pageNumber, event) { LoadDataByPageNumber(pageNumber, event, methodName) }

        });
        
        $("#divPagination").pagination('selectPage', currentPage);
    }
    if (parseInt(totalRecord) < itemsOnPage)
        $("#divPagination").hide();
    else
        $("#divPagination").show();
}
function LoadDataByPageNumber(currentPageNumber, event, methodName) {
    if (currentPage == currentPageNumber)
        return;
    currentPage = currentPageNumber;
    window[methodName](currentPage, itemsOnPage);
    return currentPage;
}
function BindDataTable(tblId, columns, data, sortColumnIndex, sortOrder) {
    $('#' + tblId).show();
    $('#' + tblId).dataTable().fnDestroy();
    dataTable = $('#' + tblId).DataTable({
        "columns": columns,
        "bPaginate": false,
        "data": data,
        "bFilter": false,
        "bInfo":false,

        aaSorting: [[sortColumnIndex == undefined ? 0 : sortColumnIndex, sortOrder == undefined ? "asc" : sortOrder]]
    });
    $('[data-toggle="tooltip"]').tooltip();
    
}
function BindStaticDataTable(tblId, columns) {
    $('#' + tblId).show();
    $('#' + tblId).dataTable().fnDestroy();
    dataTable = $('#' + tblId).DataTable({
        "columns": columns,
        //"bPaginate": false,
        //"bFilter": false,
        "bInfo": false

        
    });
    $('[data-toggle="tooltip"]').tooltip();
}

function RemoveDataTable(tblId) {
    $('#' + tblId).dataTable().fnDestroy();

}


function CheckFileExtension(file) {
    try {
        var ext = $(file).val().match(/\.([^\.]+)$/)[1];
        switch (ext.toString().toLowerCase()) {
            case 'jpg':
            case 'png':
            case 'jpeg':
            case 'gif':
                return true;
                break;
            default:
                ShowErrorToastMessage("Only jpg,jpeg,gif and png files are allowed.");
                $(file).val("");
                return false;
        }
    }
    catch (ex) {
    }
}


function StopNegativeValuesInInputTypeNumber(id) {
    var ele = document.getElementById(id);
    ele.onkeypress = function (e) {
        if (isNaN(this.value + "" + String.fromCharCode(e.charCode)))
            return false;
    }
    ele.onpaste = function (e) {
        e.preventDefault();
    }
}
function EncryptQueryStringValue(value)
{
    return btoa(value);
}
function DecryptQueryStringValue(value) {
    

    try {

        return atob(value);
    }
    catch(e)
    {
        return value;
    }
    
}
function CheckMaxLengthForNumberTextboxes() {
    $("input[type=number]").keyup(function () {
        var length = $(this).attr("maxlength");
        if (length != undefined) {
            if ($(this).val().length > length) {
                $(this).val($(this).val().substring(0, length));
            }
        }
    });
}
function ShowInformationDiv(selector,classToAdd,spanClass,message)
{
    $(selector).addClass(classToAdd).show().find("span." + spanClass).text(message);
}
function ResetFormFeilds(frmId)
{
    $("#" + frmId).find(":input").val("");
}