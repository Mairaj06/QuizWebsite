﻿var quizID = 0;
var questionID = 0;
$(function () {
    quizID = getUrlData()["id"];
    if (quizID == undefined)
        quizID = 0;
    if (quizID > 0)
        LoadQuizQuestions();
    $("#divAddQuestion").on("shown.bs.modal", function () {
        ResetFormFeilds("frmAddQuestion");
    });
    $("#divQuestionOptions").on("shown.bs.modal", function () {
        $("#optionsHeader").hide();
        $(".questionOption").remove();
    });
});
function LoadQuizQuestions()
{
    APICall("LoadQuizQuestions/" + quizID, "SuccessLoadQuizQuestions", "FailureLoadQuizQuestions", "GET");
}

function SuccessLoadQuizQuestions(resp)
{
    if (resp)
    {
        $("#panelHeading").html("Questions List for " + resp.m_Item1.Name);
        if (resp.m_Item2.length > 0)
        {
            RemoveDataTable("tblQuestions");
            $("#tblQuestions tbody").html("");
            $("#tdNoQuestion").hide();
            
            for (var i = 0; i < resp.m_Item2.length; i++)
            {
                var checked = "";
                if (resp.m_Item2[i].IsActive)
                    checked = "checked";
                var row = "<tr class='question'><td>" + (i + 1) + "</td><td>" + resp.m_Item2[i].QuestionText + "</td>" +
                    "<td><span onclick='ShowOptionsPopUp(" + resp.m_Item2[i].QuestionId + ")' class='glyphicon glyphicon-list icons' title='Options List' data-toggle='tooltip'></span></td>" +
                    "<td><span onclick='EditQuestion(" + resp.m_Item2[i].QuestionId + "," + resp.m_Item2[i].IsActive + ",\"" + resp.m_Item2[i].QuestionText + "\")' class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></td>" +
                "<td><span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip' onclick='ConfirmDeleteQuestion(" + resp.m_Item2[i].QuestionId + "," + resp.m_Item2[i].QuizId + ")'></span></td>" +
                "</tr>";
                $("#tblQuestions tbody").append(row);
            }
            var columns = [{ "bSortable": true }, { "bSortable": true }, { "bSortable": false }, { "bSortable": false }, { "bSortable": false }];
            BindStaticDataTable("tblQuestions", columns);
            $("#divAddQuestion").modal("hide");
        }
    }
}

function SaveQuestion()
{
    if (!ValidateForm("frmAddQuestion"))
        return;
    var questionText = $("#txtQuestion").val();
    var question = new Object();
    question.QuizId = quizID;
    question.QuestionId = questionID;
    question.QuestionText = questionText;
    question.IsActive = $("#chkActive").is(":checked");
    question.CreatedBy = 1;

    APICall("AddQuestion", "SuccessSaveQuestion", "FailureSaveQuestion", "POST", JSON.stringify({ Question: question }));

}
function SuccessSaveQuestion(resp)
{
    if (resp.Success) {
        ShowSuccessToastMessage(resp.ErrorMessage);
        //var qNo = $("tr.question").length;
        //var row = "<tr class='question'><td>" + (parseInt(qNo) + 1) + "</td><td>" + resp.QuestionText + "</td>" +
        //            "<td><span onclick='ShowOptionsPopUp(" + resp.QuestionId + ")' class='glyphicon glyphicon-list icons' title='Options List' data-toggle='tooltip'></span></td>" +
        //            "<td><span onclick='EditQuestion(" + resp.QuestionId + "," + resp.IsActive + ",\"" + resp.QuestionText + "\")' class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></td>" +
        //        "<td><span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip'></span></td>" +
        //        "</tr>";

        //$("#tblQuestions").append(row);
        LoadQuizQuestions();
    }
    else {
        ShowInformationDiv(".formMessage", "alert-danger", "message", resp.ErrorMessage);
    }
}
function AddOption() {
    //divAddOptions
    var optionNum = parseInt($(".questionOption").length) + 1;
    var html = '<div class="row questionOption paddingTop20">' +
                '<div class="col-lg-9 col-md-9 col-sm-9">' +
                        '<input type="text" class="form-control" maxlength="50" />' +
                    '</div>' +
                    '<div class="col-lg-2 col-md-2 col-sm-2">' +
    
    '<div class="material-switch">' +
    '<input id="chkCorrectOption' + optionNum + '" name="chkCorrectOption' + optionNum + '" type="checkbox" class="chkCorrect" />' +
    '<label for="chkCorrectOption' + optionNum + '" class="label-success"></label>' +
    '</div>' +
    '</div>' +
    '<div class="col-lg-1 col-md-1 col-sm-1">' +
    '<span class="glyphicon glyphicon-trash icons" title="Delete" data-toggle="tooltip" onclick="DeleteOption(this)"></span>' +
    '</div>' +
    '</div>';
    $("#optionsHeader").show();
    $("#divAddOptions").append(html);
    
}
function ShowOptionsPopUp(pquestionID)
{
    questionID = pquestionID;
    APICall("LoadQuestionOptions/" + pquestionID, "SuccessLoadQuestionsOptions", "FailureLoadQuestionsOptions", "GET");
    $("#divQuestionOptions").modal("show");
}
function SuccessLoadQuestionsOptions(resp)
{
    if (resp.length > 0)
    {
        MakeOptions(resp);
    }
}
function SaveQuestionOptions()
{
    if ($(".questionOption").length < 2)
    {
        ShowErrorToastMessage("Please enter at least two options.");
        //ShowInformationDiv(".formMessageOption", "alert-danger", "messageOption", "");
        return;
    }
    if ($("input.chkCorrect:checked").length < 1)
    {
        ShowErrorToastMessage("Please select at least one option as correct option.");
        return;
    }
    var lstOptions = [];
    $(".questionOption").each(function(){
        var option = new Object();
        option.OptionText = $(this).find("input.form-control").val();
        option.CorrectOption = $(this).find("input.chkCorrect").is(":checked");
        option.IsActive = $(this).find("input.chkActive").is(":checked");
        option.QuestionID = questionID;
        option.Id = $(this).attr("data-optionid") == undefined ? 0 : $(this).attr("data-optionid");
        lstOptions.push(option);
    });
    
    
    APICall("SaveQuestionOptions", "SuccessSaveQuestionOptions", "FailureSaveQuestionOptions", "POST", JSON.stringify({ Options: lstOptions }));
}
function SuccessSaveQuestionOptions(resp) {
    if (resp.Success) {
        LoadQuizQuestions();
        ShowSuccessToastMessage(resp.Message);
        $("#divQuestionOptions").modal("hide");
    }
    else {
        ShowSuccessToastMessage(resp.Message);
        $("#divQuestionOptions").modal("hide");
    }
}
function MakeOptions(arrOptions) {
    //divAddOptions
    var html = "";
    for (var i = 0; i < arrOptions.length; i++) {
        var correctOption;  
        if (arrOptions[i].CorrectOption)
            correctOption = "checked =' checked'";
        else
            correctOption = "";
        var optionText = arrOptions[i].OptionText;
        var isActive = arrOptions[i].IsActive ? "checked = 'checked'" : "";
        
        html += '<div class="row questionOption paddingTop20" data-optionid="' + arrOptions[i].Id + '">' +
                    '<div class="col-lg-9 col-md-9 col-sm-9">' +
                            '<input type="text" class="form-control" maxlength="50" value="' + optionText + '" />' +
                        '</div>' +
                        '<div class="col-lg-2 col-md-2 col-sm-2">' +

        '<div class="material-switch">' +
        '<input id="chkCorrectOption' + i + '" name="chkCorrectOption' + i + '" ' + correctOption + ' type="checkbox" class="chkCorrect" />' +
        '<label for="chkCorrectOption' + i + '" class="label-success"></label>' +
        '</div>' +
        '</div>' +
        '<div class="col-lg-1 col-md-1 col-sm-1">' +
        '<span class="glyphicon glyphicon-trash icons" title="Delete" data-toggle="tooltip" onclick="ConfirmDeleteQuestionOption('+arrOptions[i].Id+','+arrOptions[i].QuestionID+')"></span>' +
        '</div>' +
        '</div>';
        
    }
    $(".questionOption").remove();
    $("#divAddOptions").append(html);
    $("#optionsHeader").show();
}
function ShowAddQuestionPopUp()
{
    $("#divAddQuestion").modal("show");
}
function EditQuestion(pQuestionId,isActive,questionText)
{
    $("#divAddQuestion").modal("show");
    questionID = pQuestionId;
    $("#txtQuestion").val(questionText);
    $("#chkActive").attr("checked", isActive);
    
}
function ConfirmDeleteQuestion(questionId,quizId)
{
    $("#divDeleteQuestion").modal("show");
    $("#btnDeleteQuestion").attr({ "data-quizID": quizId, "data-questionID": questionId });
}
function DeleteQuestion()
{
    var questionId = $("#btnDeleteQuestion").attr("data-questionID");
    var quizId = $("#btnDeleteQuestion").attr("data-quizID");

    APICall("DeleteQuizQuestions/" + quizId + "/" + questionId, "SuccessDeleteQuizQuestions", "FailureDeleteQuizQuestions", "GET");
}
function SuccessDeleteQuizQuestions(resp) {
    if (resp != null) {
        $("#divDeleteQuestion").modal("hide");
        ShowSuccessToastMessage("Question deleted successfully.");
        SuccessLoadQuizQuestions(resp);
    }
}
function ConfirmDeleteQuestionOption(optionId,questionId) {
    $("#divDeleteQuestionOption").modal("show");
    $("#btnDeleteQuestionOption").attr({ "data-optionid": optionId, "data-questionID": questionId });
}
function DeleteQuestionOption()
{
    var questionId = $("#btnDeleteQuestionOption").attr("data-questionID");
    var optionId = $("#btnDeleteQuestionOption").attr("data-optionid");
    APICall("DeleteQuestionOption/" + questionId + "/" + optionId, "SuccessDeleteQuestionOption", "FailureDeleteQuestionOption", "GET");
    
}
function SuccessDeleteQuestionOption(resp)
{
    if(resp && resp!="null")
    {
        $("#divDeleteQuestionOption").modal("hide");
        ShowSuccessToastMessage("Option deleted successfully.");
        MakeOptions(resp);
    }
}
function DeleteOption(obj)
{
    $(obj).closest(".questionOption").remove();
    if ($(".questionOption").length == 0)
        $("#optionsHeader").hide();
}