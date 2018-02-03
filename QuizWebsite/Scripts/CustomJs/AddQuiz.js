var quizId;
$(function () {
    $('.summernote').summernote({
        minHeight: '150px', toolbar: [
        ['style', ['style']],
        ['font', ['bold', 'italic', 'underline', 'clear']],
        ['fontname', ['fontname']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
        ['table', ['table']],
        ['insert', ['link']],
        ['view', ['fullscreen']],
        ['help', ['help']]
        ],
    });
    APICall("LoadAllCategories", "SuccessLoadQuizCategory", "FailureLoadQuizCategory", "GET");
    quizId = getUrlData()["id"];
    if (quizId == undefined)
        quizId = 0;
    
});
function SuccessLoadQuizCategory(resp)
{
    if (resp.length > 0)
    {
        for (var i = 0; i < resp.length; i++)
        {
            $("#ddlQuizType").append($("<option></option>").attr({ "value": resp[i].Id}).text(resp[i].Category));
        }
    }
    if (quizId > 0)
        APICall("LoadQuizById/" + quizId, "SuccessLoadQuizById", "FailureLoadQuizById", "GET");
}
function SuccessLoadQuizById(resp)
{
    if (resp)
    {
        $("#txtTitle").val(resp.Name);
        $("#chkReattempt").attr("checked", resp.AllowReAttempt);
        $("#chkBasedOnTime").attr("checked", resp.Time);
        $("#chkActive").attr("checked", resp.IsDeleted);
        $("#txtDescription").summernote("code", resp.Description);
        $("#ddlQuizType").val(resp.Type);
        $("#txtUrl").val(resp.QuizUrl);
        $("#txtPassMarks").val(resp.PassMarks);
        $("#txtQuizNotes").summernote("code", resp.QuizNotes);
        $("#chkRequireLogin").attr("checked", resp.RequiresLogin)
        if (resp.Time)
        {
            $("#ddlHours").val(resp.Hours);
            $("#ddlMinutes").val(resp.Minutes);
            $("#divSelectTime").show("slow");
        }
        if (resp.AllowReAttempt)
        {
            $("#ddlDays").val(resp.ReAttemptDuration);
            $("#divSelectDays").show("slow");
        }
    }
}
function AddQuiz()
{
    if (!ValidateForm("frmAddQuiz"))
        return;
    var objQuiz = new Object();
    objQuiz.ID = quizId;
    objQuiz.Name = $("#txtTitle").val();
    objQuiz.AllowReAttempt = $("#chkReattempt").is(":checked");
    objQuiz.Time = $("#chkBasedOnTime").is(":checked");
    objQuiz.IsActive = $("#chkActive").is(":checked");
    objQuiz.RequiresLogin = $("#chkRequireLogin").is(":checked");
    if ($("#txtDescription").val() != "")
        objQuiz.Description = $("#txtDescription").val();
    if ($("#ddlQuizType").val() != "0")
        objQuiz.Type = $("#ddlQuizType").val();
    if ($("#txtUrl").val() != "")
        objQuiz.QuizUrl = $("#txtUrl").val();
    if ($("#txtPassMarks").val() != "")
        objQuiz.PassMarks = $("#txtPassMarks").val();
    if ($("#ddlDays").val() != "0")
        objQuiz.ReAttemptDuration = $("#ddlDays").val();
    if ($("#chkBasedOnTime").is(":checked"))
    {
        if ($("#ddlHours").val() != "0")
            objQuiz.Hours = parseInt($("#ddlHours").val());
        if ($("#ddlMinutes").val() != "0")
            objQuiz.Minutes = parseInt($("#ddlMinutes").val());
        
    }
    if ($("#txtQuizNotes").val() != "")
        objQuiz.QuizNotes = $("#txtQuizNotes").val();
    objQuiz.CreatedBy = 1;
    APICall("SaveQuiz", "SuccessAddQuiz", "FailureAddQuiz", "POST", JSON.stringify({ objQuiz: objQuiz }));
}
function SuccessAddQuiz(resp)
{
    if (resp.Success) {
        ShowSuccessToastMessage(resp.Message);
    }
    else {
        $(".formMessage").addClass("alert-danger").show().find("span.message").text(resp.Message);
    }
}
function FailureAddQuiz(resp) {

}
function ShowHideDays(chkBox)
{
    if ($(chkBox).is(":checked"))
        $("#divSelectDays").show("slow");
    else
        $("#divSelectDays").hide("slow");
}
function ShowHideTimer(chkBox) {
    if ($(chkBox).is(":checked"))
        $("#divSelectTime").show("slow");
    else
        $("#divSelectTime").hide("slow");
}
function AddQuestion() {


    var question = $("#txtQuestion").val();
    if (question) {
        $.ajax({
            type: "POST",
            url: "Default.aspx/AddQuestion",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ Pquestion: question }),
            success: function (resp) {
                BuildQuestionList(resp.d);
            }

        });
        ;
    }
}
function BuildQuestionList(list) {
    if (list.length > 0) {
        $("#tdNoQuestion").hide();
        $("#tblQuestions").html("");
    }
    for (var i = 0; i < list.length; i++) {
        var html = '<tr scope="row">' +
        '<td>' + (i + 1) + '</td><td>' + list[i].Question + '</td><td><span class="glyphicon glyphicon-edit" title="Edit Question"></span></td><td><span class="glyphicon glyphicon-plus" title="Add Options" data-toggle="modal" data-target="#divQuestionOption"></span></td><td><span class="glyphicon glyphicon-trash" title="Delete Question"></span></td></tr>';
        $("#tblQuestions").append(html);
    }
}
function AddQuestionOption() {
    var question = $("#txtoption").val();
    if (question) {
        $.ajax({
            type: "POST",
            url: "Default.aspx/AddQuestionOption",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ Option: question }),
            success: function (resp) {
                BuildQuestionOptionsList(resp.d);
            }

        });

    }

}
function BuildQuestionOptionsList(list) {
    if (list.length > 0) {
        $("#trNoOption").hide();

    }
    for (var i = 0; i < list.length; i++) {
        var html = '<tr scope="row">' +
        '<td>' + list[i].Id + '</td><td>' + list[i].Option + '</td><td><span class="glyphicon glyphicon-edit" title="Edit Question"></span></td><td><span class="glyphicon glyphicon-plus" title="Add Options" data-toggle="modal" data-target="#divQuestionOption"></span></td><td><span class="glyphicon glyphicon-trash" title="Delete Question"></span></td></tr>';
        $("#tblQuestionOptions").append(html);
    }
}