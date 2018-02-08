var quizActionsUrl = "Quiz/";
$(function () {
    
    APICall(quizActionsUrl+"LoadAllQuiz/1", "SuccessLoadAllQuiz", "FailureAddQuiz", "GET");
});
function SuccessLoadAllQuiz(resp)
{
    var columns = [{ "data": "Name", "bSortable": true }, { "data": "Type", "bSortable": true }, {
        mRender: function (data, type, row) {
            return row.Time == false ? "No" : "Yes";
        }, "bSortable": true
    }, {
        mRender: function (data, type, row) {
            return row.AllowReAttempt == false ? "No" : "Yes";
        }, "bSortable": true
    }, { "data": "PassMarks", "bSortable": true },
    {
        mRender: function (data, type, row) {
            return "<a href='Quiz/QuizQuestions?Id=" + row.ID + "'><span class='glyphicon glyphicon-list-alt icons' title='Questions List' data-toggle='tooltip'></span></a>";
        }, "bSortable": false
    },
    {
        mRender: function (data, type, row) {
            return "<a href='Quiz/AddQuiz?Id=" + row.ID + "'><span class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></a>";
        }, "bSortable": false
    },
    {
        mRender: function (data, type, row) {
            return "<span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip' onclick='ConfirmDeleteQuiz(" + row.ID + ")'></span>";
        }, "bSortable": false
        
    }];
    BindDataTable("tblQuizList", columns, resp);
}

